using Application.Services;
using Application.Services.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CrossCutting.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("ServiceLayer", client =>
        {
            client.BaseAddress = new Uri(configuration["ServiceUri:ServiceLayer"]);
        });
        
        services.AddScoped<ITokenManagementService, TokenManagementService>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IHanaAuthenticateService, HanaAuthenticateService>();

        return services;
    }
}