using Autofac;
using BloGoes.Data.Repository;
using BloGoes.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloGoes.Data.AutoFac
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationCommandContext>().As<BaseContext>();
            builder.RegisterType<ApplicationQuerieContext>().As<BaseContext>();
            builder.Register(ctx =>
            {
                var allContext = new Dictionary<string, BaseContext>();
                allContext.Add("ApplicationCommandContext", ctx.Resolve<ApplicationQuerieContext>());
                allContext.Add("ApplicationQuerieContext", ctx.Resolve<ApplicationQuerieContext>());
                return new DbContextFactory(allContext);

            });
           
            builder.RegisterType<BlogRepository>().As<IBlogRepository>();

        }
    }
}
