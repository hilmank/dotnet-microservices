using System.Reflection;
using Student.Application.GrpcService;
using Student.Application.Handlers;
using Student.Application.Mapper;
using Student.Application.Settings;
using Student.Core.Repositories;
using Student.Infrastructure.Repositories;
using UserApp.Application.Protos;
#pragma warning disable CS8604 // Possible null reference argument.

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<CourseProfile>());

var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetCoursesQueryHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<UserGrpcService>();
builder.Services.AddGrpcClient<UserProtoService.UserProtoServiceClient>
    (cfg => cfg.Address = new Uri(builder.Configuration["GrpcSettings:UserUrl"]));

var databaseSettings = new DatabaseSettings();
builder.Configuration.Bind(DatabaseSettings.SectioName, databaseSettings);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = null;
    o.JsonSerializerOptions.DictionaryKeyPolicy = null;
    o.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.UseHttpMethodOverride();
app.UseRouting();
/*
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
*/
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
