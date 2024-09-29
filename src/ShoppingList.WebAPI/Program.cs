using System.Text.Json.Serialization;
using ShoppingList.Application;
using ShoppingList.Application.Exceptions;
using ShoppingList.Infrastructure;
using ShoppingList.Persistance;

var builder = WebApplication.CreateBuilder(args);


builder.Services.PersistanceContextConfiguration(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureApplicationRegistration();
builder.Services.AddInfrastructureServices();
builder.Services.ConfigureIdentity(builder.Configuration);


builder.Services.AddControllers()
    .AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandlingMiddleware();

app.UseHttpsRedirection();


app.MapControllers();
app.Run();