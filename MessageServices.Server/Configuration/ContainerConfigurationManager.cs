using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using MessageService.Common.Extensions;
using MessageService.Common.Configuration;
using Microsoft.AspNetCore.Http;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;
using MessageService.IService;
using Microsoft.AspNetCore.Mvc;
using MessageService.Server.Configuration;


namespace MessageService.Server.Configuration
{
    public class ContainerConfigurationManager : AbstractConfigurationManager
    {
        //**********************************
        private ContainerBuilder _builder;
        //**********************************
        public ContainerConfigurationManager(IServiceCollection services, IConfiguration configuration) : base(services, configuration)
        {

            _services

                .AddRazorPages()
                //.AddWebApiConventions()
                //////////////////////////////////////////////////////////////////////////////
                //!!! IMPORTANT , For the Core DI (Dependency Injection) with Autofac: if we are using PROPERTY INJECTION, 
                // and AddControllersAsServices() HASN'T BEEN CALLED, Core framework WILL NOT USE AUTOFAC PROVIDER 
                // for the Controllers, so the controller properties WILL NOT BE INJECTED!
                .AddControllersAsServices()
                // And View components aswell
                .AddViewComponentsAsServices();
            ///////////////////////////////  TEMP DATA      //////////////////////////////////////////
            _services.AddMemoryCache();
            /////////////////////////////////////////////////////////////////////////////////////////


            ////////////////////////////////////////////////////////////////////////////////
            _builder = new ContainerBuilder();
            _builder.Populate(_services);
        }
        public IServiceProvider Register()
        {
            /*********************************************************************************/
            var serviceSection = _configuration.GetSection("Services");
            //services
            if (serviceSection.Exists())
            {

                var serviceList = serviceSection.Get<List<ServiceConfig>>();
                var activeServiceList = from contextConfig in serviceList where contextConfig.Active select contextConfig;
                _builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
                foreach (var serviceConfig in activeServiceList)
                {

                    ///// Regester UnitOfWork 
                    var sourceUnitType = serviceConfig.SourceUnitType.LoadTypeByName();
                    var destUnitType = serviceConfig.DestUnitType.LoadTypeByName();
                    //!!!!! IMPORTANT - Core DI mechanism does not have 'instance per request' option, so the way
                    // (not only one) to share  "Unit of Work"/(dbcontext) between different Services (using DI) with Autofac is:
                    // 1. REGISTER UNIT OF WORK BY CALLING: .InstancePerLifetimeScope()
                    // 2. REGISTER ALL SERVICES BY CALLING: .InstancePerDependency() (DEFAULT)                             

                    _builder.RegisterType(destUnitType)
                       .As(sourceUnitType)
                       .WithParameter(new TypedParameter(typeof(string), serviceConfig.Connection))
                       .InstancePerLifetimeScope();
                       //.


                    // Get Assemly of Services

                    var serviceAssembly = Assembly.Load(serviceConfig.Assembly);

                    // Register Services
                    _builder.RegisterAssemblyTypes(serviceAssembly)
                   // Register all Data Services
                   .Where(t => typeof(IDataService).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                   .AsImplementedInterfaces()
                   .InstancePerDependency();
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    _builder.RegisterAssemblyTypes(serviceAssembly)
                  // Register all Common Services
                  .Where(t => typeof(MessageService.IService.Common.ICommonService).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                  .AsImplementedInterfaces()
                  .SingleInstance();

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 


                }

            }

            // Register Controllers  using .PropertiesAutowired() method , so all properties
            // which have PUBLIC SETTER and
            // whose types implement IService Interface (and were registered above),
            // will be automatically injected by Autofac IoC

            var assembly = AppDomain.CurrentDomain.GetAssemblies().ToList();
            (from a in assembly
             from t in a.GetTypes()
             where t.IsSubclassOf(typeof(ControllerBase)) && !t.IsAbstract && t.Namespace.StartsWith("MessageService")
             select t)
           .ToList()
           .ForEach((type) =>
           {
               _builder.RegisterType(type).PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).AsSelf();
           });
            // Return the new instance of Service Provider (IServiceProvider, in our case - AutofacServiceProvider)
            // which will be used by Core Framework to resolve all registered dependences.
            // Method Build() of Autofac.ContainerBuilder returns the new container (IContainer) with all registrations that have been made

            return new AutofacServiceProvider(_builder.Build());
            /***************************************************************************************/

        }


    }
}
