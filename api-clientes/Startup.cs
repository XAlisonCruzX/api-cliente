using api_clientes.Database;
using api_clientes.Repositories;
using api_clientes.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace api_clientes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            

            services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(op => op.AddDefaultPolicy(
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                ));

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = false;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = false;
                    options.SuppressMapClientErrors = false;
                    options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                        "https://httpstatuses.com/404";
                });


            //INTERFACE
            services.AddSingleton<IConexao>(sp => new Conexao(connectionString));
            services.AddScoped<IEnderecoRepository>(sp => new EnderecoRepository(sp.GetService<IConexao>()));
            services.AddScoped<ITelefoneRepository>(sp => new TelefoneRepository(sp.GetService<IConexao>()));
            services.AddScoped<IClienteRepository>(sp => new ClienteRepository(sp.GetService<IConexao>()));
            services.AddScoped<IRdSocialRepository>(sp => new RdSocialRepository(sp.GetService<IConexao>()));

            //SERVICE
            services.AddScoped(sp => new EnderecoService(sp.GetService<IEnderecoRepository>()));
            services.AddScoped(sp => new ClienteService(sp.GetService<IClienteRepository>(), sp.GetService<EnderecoService>(), sp.GetService<TelefoneService>(), sp.GetService<RdSocialService>()));
            services.AddScoped(sp => new RdSocialService(sp.GetService<IRdSocialRepository>()));
            services.AddScoped(sp => new TelefoneService(sp.GetService<ITelefoneRepository>()));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
