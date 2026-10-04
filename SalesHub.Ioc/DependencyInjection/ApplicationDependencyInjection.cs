using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesHub.Application.Interface;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Interface.Services.Sale;
using SalesHub.Application.Mapping;
using SalesHub.Application.Services;
using SalesHub.Application.Validators.Sale;
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
                cfg.AddProfile<SaleProfile>();
                cfg.AddProfile<SaleDetailProfile>();

            });

            // FluentValidation
            services.AddValidatorsFromAssemblyContaining<CreateSaleValidator>();


            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ISaleDetailService, SaleDetailService>();

            services.AddScoped<ISaleNumberGenerator, SaleNumberGenerator>();
            services.AddScoped<IDiscountCalculator, PercentageDiscountCalculator>();
            services.AddScoped<ITaxCalculator, ItbisTaxCalculator>();

            return services;
        }
    }
}
