using Application.Services;
using Application.Services.Interfaces;
using Application.Usecases;
using Application.Usecases.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CrossCutting.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var httpClientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };

        services.AddHttpClient("ServiceLayer", client =>
            {
                client.BaseAddress = new Uri(configuration["ServiceUri:ServiceLayer"]);
            })
            .ConfigurePrimaryHttpMessageHandler(() => httpClientHandler);
        
        services.AddScoped<ITokenManagementService, TokenManagementService>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IHanaAuthenticateService, HanaAuthenticateService>();
        services.AddScoped<IGetEmployeeByEmailUsecase, GetEmployeeByEmailUsecase>();
        services.AddScoped<IHanaAuthenticateUsecase, HanaAuthenticateUsecase>();
        
        return services;
    }
}