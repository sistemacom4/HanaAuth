using System.Text;
using Application.Services.Interfaces;
using Application.Usecases;
using Application.Usecases.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


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
        
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddSingleton<ITokenManagementService, TokenManagementService>();
        services.AddScoped<IHanaAuthenticateService, HanaAuthenticateService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IGetEmployeeByEmailUsecase, GetEmployeeByEmailUsecase>();
        services.AddScoped<IHanaAuthenticateUsecase, HanaAuthenticateUsecase>();
        services.AddScoped<IAuthenticateUserUsecase, AuthenticateUserUsecase>();
        services.AddScoped<ICheckHanaSessionValidUsecase, CheckHanaSessionValidUsecase>();
        
        return services;
    }

    public static IServiceCollection AddAuthenticationJwt(this IServiceCollection services,
        IConfiguration configuration)
    {
        var secretKey = configuration["JWT:SecretKey"]
                        ?? throw new ArgumentException("Invalid Secret Key");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secretKey))
            };
        });

        return services;
    }
}