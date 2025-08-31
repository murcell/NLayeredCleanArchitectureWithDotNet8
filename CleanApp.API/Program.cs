using App.Persistence.Extensions;
using App.Services.Extensions;
using CleanApp.API.ExceptionHandler;
using CleanApp.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
	options.Filters.Add<FluentValidationFilter>();
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// burayý temiz turmak yazmak yerine extensions olarak yazdýk 
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddExceptionHandler<CriticalExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped(typeof(NotFoundFilter<,>));

var app = builder.Build();

app.UseExceptionHandler(x => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
