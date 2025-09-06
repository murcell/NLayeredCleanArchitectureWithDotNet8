using App.Bus;
using App.Persistence.Extensions;
using App.Services.Extensions;
using CleanApp.API.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithFiltersExt();
builder.Services.AddExceptionHandlerExt();
builder.Services.AddCachingExt();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerExt();
// burayý temiz turmak yazmak yerine extensions olarak yazdýk 
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddBusExt(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
