using Autofac;
using Autofac.Integration.WebApi;
using Entities;
using Repository;
using RestWebAPI.Controllers;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace RestWebAPI.Infrastructure
{
    internal class DependencyConfigure
    {
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
       

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CustomerContext>()
                   .As<ICustomerContext>()
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>))
                  .As(typeof(IRepository<>))
                  .InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(WebApiApplication).Assembly).PropertiesAutowired();

            ////deal with your dependencies here
            //builder.RegisterType<EmployeeContext>().As<IEmployeeContext>().InstancePerDependency(); ;

            //builder.RegisterGeneric(typeof(RepositoryService<>)).As(typeof(IRepository<>));

            builder.RegisterType<CustomerService>().As<ICustomerService>();
            return builder.Build();
            
        }
    }
}