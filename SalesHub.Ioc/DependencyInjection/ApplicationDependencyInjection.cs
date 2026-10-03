using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesHub.Application.Interface;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Mapping;
using SalesHub.Application.Services;
using SalesHub.Infrastructure.Persistence.Context;
using SalesHub.Infrastructure.Persistence.Repositories;
using SalesHub.Infrastructure.Persistence.UnitOfWork;
using SalesHub.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Ioc.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(
          this IServiceCollection services)
        {

            // ApplicationDependencyInjection
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
                cfg.AddProfile<CategoryProfile>();
                cfg.AddProfile<CustomerProfile>();
                cfg.AddProfile<UserProfile>();

            });

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
