using Autofac;
using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories;
using Logix.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.DI
{
    public class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //builder.RegisterType<MainRepositoryManager>().As<IMainRepositoryManager>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(QueryRepository<>)).As(typeof(IQueryRepository<>));
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<CurrentData>().As<ICurrentData>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.Load("Logix.Infrastructure"))
                   .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Manager"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
