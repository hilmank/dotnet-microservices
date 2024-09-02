using System.Reflection;
using System.Text;
using Common.Logging;
using Infrastructure.Common.UserPreferences;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using UserApp.API.Services;
using UserApp.Application.Handlers;
using UserApp.Application.Mapper;
using UserApp.Application.Settings;
using UserApp.Core.Repositories;
using UserApp.Infrastructure.Extensions;
using UserApp.Application.Middlewares;
using UserApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Serilog configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Register Mediatr
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetUsersQueryHandler).Assembly
};
builder.Services.AddLocalization();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddGrpc();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserPreferences>();

var databaseSettings = new DatabaseSettings();
builder.Configuration.Bind(DatabaseSettings.SectioName, databaseSettings);

var directorySettings = new DirectorySettings();
builder.Configuration.Bind(DirectorySettings.SectioName, directorySettings);
DirectorySettings.PathFileUser = $"{DirectorySettings.BasePath}/{DirectorySettings.FileUser}";
if (!Directory.Exists(DirectorySettings.PathFileUser))
    Directory.CreateDirectory(DirectorySettings.PathFileUser);
DirectorySettings.UrlFileUser = $"{DirectorySettings.BaseUrl}/{DirectorySettings.PathUrl}/{DirectorySettings.FileUser}";

DirectorySettings.PathFileApp = $"{DirectorySettings.BasePath}/{DirectorySettings.FileApp}";
if (!Directory.Exists(DirectorySettings.PathFileApp))
    Directory.CreateDirectory(DirectorySettings.PathFileApp);
DirectorySettings.UrlFileApp = $"{DirectorySettings.BaseUrl}/{DirectorySettings.PathUrl}/{DirectorySettings.FileApp}";

var jwtSettings = new JwtSettings();
builder.Configuration.Bind(JwtSettings.SectioName, jwtSettings);

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<UserProfile>());

//Autorization
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JwtSettings.AppIssuer,
            ValidAudience = JwtSettings.AppAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.AppSecret))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

//Migrate Database
app.MigrateDatabase<Program>();
//Localization
app.UseRequestLocalization(["id-ID", "en-US"]);
app.UseMiddleware<UserPreferencesMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

//Authorization
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<UserService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with grpc endpoints must be made through a grpc client");
    });
});

app.Run();