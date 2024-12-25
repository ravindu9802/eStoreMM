using eStore.Authentication;
using eStore.Extensions;
using MassTransit.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Orders.Infrastructure.Extensions;
using Products.Application.Extensions;
using Products.Infrastructure.Extensions;
using Serilog;
using Users.Application.Extensions;
using Users.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

// User module service registration 
builder.Services
    .AddUserApplication(builder.Configuration)
    .AddUserInfrastructure(builder.Configuration);

// Products service registration 
builder.Services
    .AddProductsApplication()
    .AddProductsInfrastructure(builder.Configuration);

// Orders service registration 
builder.Services
    .AddOrdersInfrastructure(builder.Configuration);

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

//builder.Services
//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(o =>
//    {
//        o.TokenValidationParameters = new()
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration.GetSection("JwtConfig:Issuer").Value,
//            ValidAudience = builder.Configuration.GetSection("JwtConfig:Audience").Value,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtConfig:SecretKey").Value!))
//        };
//    });

builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

// Open telemetry for distributed tracing.
builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("eShop"))
    .WithTracing(tracing =>
    {
        tracing
            .AddHttpClientInstrumentation()
            .AddAspNetCoreInstrumentation()
            .AddSource(DiagnosticHeaders.DefaultListenerName);

        tracing.AddOtlpExporter();
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "Swagger API"); });
    app.ApplyMigrations();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors(builder.Configuration.GetSection("CorsPolicy:PolicyName").Value!);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();