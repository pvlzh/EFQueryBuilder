using Domain.Interfaces.CQRS.Command;
using Domain.Interfaces.CQRS.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
/// Регистрация зависимостей.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Зарегистрировать варианты использования.
    /// </summary>
    /// <param name="services"> Коллекция сервисов приложения.</param>
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        var currentAssembly = typeof(DependencyInjection).Assembly;
        
        services.Scan(opt =>
            opt.FromAssemblies(currentAssembly)
                .AddClasses(filter => filter.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        
        services.Scan(opt =>
            opt.FromAssemblies(currentAssembly)
                .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        
        return services;
    }
}