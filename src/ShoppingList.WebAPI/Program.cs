using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application;
using ShoppingList.Application.Exceptions;
using ShoppingList.Persistance;
using ShoppingList.Persistance.Context;
using FluentValidation;
using ShoppingList.Application.Features.Products.Commands.CreateProduct;

var builder = WebApplication.CreateBuilder(args);


builder.Services.PersistanceContextConfiguration(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureApplicationRegistration();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


// builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
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