using System.Reflection;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Behaviours;

namespace Products.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddProductsApplication(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly);
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));

        return services;
    }
}