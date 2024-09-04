using System.Reflection;
using Teacher.Application;
using Teacher.Core;
using Teacher.Infrastructure;
#pragma warning disable CS8604 // Possible null reference argument.

var builder = WebApplication.CreateBuilder(args);

var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetCoursesQueryHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();

var databaseSettings = new DatabaseSettings();
builder.Configuration.Bind(DatabaseSettings.SectionName, databaseSettings);

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

app.Run();
