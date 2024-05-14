using BloGoes.Data;
using BloGoes.Data.Repository;
using BloGoes.Data.Repository.Interfaces;
using BloGoes.Services;
using BloGoes.Services.JwtServices;
using BloGoes.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;

namespace BloGoes.IOC
{
    public static class Register
    {
        public static void RegisterDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            //Entity FrameWork
            services.AddDbContext<ApplicationCommandContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbCommandContext")));
            //IF USE CQRS 
            services.AddDbContext<ApplicationQuerieContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbQuerieContext")));

            #region DependencyInjection

            ////Services
            services.AddScoped<IBlogServices, BlogServices>();
            services.AddScoped<IBlogUserServices, BlogUserServices>();
            services.AddSingleton<IWebSocketServer, WebSocketServer>();

            services.AddScoped<IJwtUtils, JwtUtils>();
            
            ////Repository//
            services.AddSingleton(typeof(IEntityRepository<>), typeof(EntityBaseRepository<>));

            ////Entity
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogUserRepository, BlogUserRepository>();

            #endregion
        }
    }
}
