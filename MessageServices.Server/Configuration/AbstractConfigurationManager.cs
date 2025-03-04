using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.Server.Configuration
{
    public abstract class AbstractConfigurationManager
    {
        protected IServiceCollection _services;
        protected IConfiguration _configuration;
        public AbstractConfigurationManager(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

    }
}
