using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Persistance;
using ShoppingList.Persistance.Context;
using ShoppingList.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPersistanceServices();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddJsonOptions(opt => {
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShoppingListDbContext>(opt => 
{
  opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));  
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandle<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseHttpsRedirection();


app.MapControllers();
app.Run();
