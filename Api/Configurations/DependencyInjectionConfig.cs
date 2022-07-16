using api_clientes.Database;
using api_clientes.Repositories;
using Core.Interface.Database;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddConnection()
                .AddServices()
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddConnection(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
            services.AddSingleton<IConexao>(sp => new Conexao(connectionString));           
            
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEnderecoRepository, EnderecoRepository>()
                .AddScoped<ITelefoneRepository, TelefoneRepository>()
                .AddScoped<IClienteRepository, ClienteRepository>()
                .AddScoped<IRdSocialRepository, RdSocialRepository>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEnderecoService, EnderecoService>()
                .AddScoped<IClienteService, ClienteService>()
                .AddScoped<IRdSocialRepository, RdSocialRepository>()
                .AddScoped<ITelefoneRepository, TelefoneRepository>();

            return services;
        }
    }
}
