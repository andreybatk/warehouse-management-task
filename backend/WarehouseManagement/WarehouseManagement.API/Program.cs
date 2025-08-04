using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.API.Helpers;
using WarehouseManagement.API.Middlewares;
using WarehouseManagement.BusinessLogic;
using WarehouseManagement.DataAccess;
using WarehouseManagement.DataAccess.DiContainer;
using WarehouseManagement.Domain;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString(
    "DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddSwaggerGen(c =>
{
    var xmlApi = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    if (File.Exists(xmlApi))
        c.IncludeXmlComments(xmlApi, includeControllerXmlComments: true);

    var businessLogicAssembly = typeof(BusinessLogicAssemblyMarker).Assembly;
    var xmlBusinessLogic = Path.Combine(AppContext.BaseDirectory, $"{businessLogicAssembly.GetName().Name}.xml");
    if (File.Exists(xmlBusinessLogic))
        c.IncludeXmlComments(xmlBusinessLogic);

    var domainAssembly = typeof(DomainAssemblyMarker).Assembly;
    var xmlDomain = Path.Combine(AppContext.BaseDirectory, $"{domainAssembly.GetName().Name}.xml");
    if (File.Exists(xmlDomain))
        c.IncludeXmlComments(xmlDomain);

    c.SchemaFilter<EnumSchemaFilter>();
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<BusinessLogicAssemblyMarker>();
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(BusinessLogicAssemblyMarker).Assembly);

builder.Services.AddHealthChecks();

builder.Services.AddRepositories();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();
