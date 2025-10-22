using Autofac;

namespace Logix.Application.DI
{
    public class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.Load("Logix.Application"))
                   .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Manager"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
            
        }
    }
}
