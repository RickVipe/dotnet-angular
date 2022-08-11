using DDDapp.Application.Persistence;
using DDDapp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DDDapp.Infrastructure;

public static class DependencyInjection{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services){
        services.AddScoped<IPersUser, PersUser>();
        services.AddDbContext<UsersAPIDbContext>(options => options.UseInMemoryDatabase("UsersDb"));
        return services;
    }

}