using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using ShoppingList.Application;
using ShoppingList.Application.Exceptions;
using ShoppingList.Infrastructure;
using ShoppingList.Persistance;
using ShoppingList.WebAPI.LogConfig.ColumnWriters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpContextAccessor();

builder.Services.PersistanceContextConfiguration(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureApplicationRegistration();
builder.Services.AddInfrastructureServices();
builder.Services.ConfigureIdentity(builder.Configuration);



Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"),
        "Logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>()
        {
            { "message", new RenderedMessageColumnWriter() },
            { "message_template", new MessageTemplateColumnWriter() },
            { "level", new LevelColumnWriter(true, NpgsqlTypes.NpgsqlDbType.Varchar) },
            { "time_stamp", new TimestampColumnWriter()},
            { "exception", new ExceptionColumnWriter()},
            { "log_event", new LogEventSerializedColumnWriter()},
            { "UserName", new UsernameColumnWriter()}

        })
    .WriteTo.Seq(builder.Configuration["Seq:ServerUrl"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);


builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ShoppingList API",
        Version = "v1",
        Description = "Shopping API Swagger Client"
    });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.ConfigureExceptionHandlingMiddleware();

app.UseHttpLogging();

app.UseHttpsRedirection();


app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated == true ? context.User.Identity.Name : null;
    LogContext.PushProperty("UserName", username);

    await next();
});

app.MapControllers();
app.Run();