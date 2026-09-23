using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Ioc.DependencyInjection
{
    public static class SalesHubDependencyInjection
    {
        public static IServiceCollection AddSalesHubServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            return services;
        }
    }
}
