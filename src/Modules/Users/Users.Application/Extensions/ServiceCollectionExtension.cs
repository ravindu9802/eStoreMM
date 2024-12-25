using System.Reflection;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Behaviours;

namespace Users.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);


        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly);
        });

        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            // Setup message connsumers
            //config.AddConsumer<AddUserEventConsumer>();
            config.AddConsumers(typeof(ServiceCollectionExtension).Assembly);

            // Setup message transporter
            config.UsingInMemory((context, config) => config.ConfigureEndpoints(context));

            // Setup RabbitMQ
            //config.UsingRabbitMq((context, cnf) =>
            //{
            //    cnf.Host(configuration["RabbitMQ:Host"], h =>
            //    {
            //        h.Username(configuration["RabbitMQ:Username"]!);
            //        h.Password(configuration["RabbitMQ:Password"]!);
            //    });

            //    cnf.ConfigureEndpoints(context);
            //});
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));

        return services;
    }
}