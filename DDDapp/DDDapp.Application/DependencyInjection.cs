using DDDapp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DDDapp.Application;

public static class DependencyInjection{
    public static IServiceCollection AddApplication(this IServiceCollection services){
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}