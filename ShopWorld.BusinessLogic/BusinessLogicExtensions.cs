using ShopWorld.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.BusinessLogic
{
    public static class BusinessLogicExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            #region Business Logic
            services.AddScoped<IAuthorizationLogic, AuthorizationLogic>();
            services.AddScoped<IEmployeeLogic, EmployeeLogic>();
            services.AddScoped<ICustomerLogic, CustomerLogic>();
            services.AddScoped<IItemLogic, ItemLogic>();
            services.AddScoped<IOrderLogic, OrderLogic>();
            services.AddScoped<ISupplierLogic, SupplierLogic>();
            services.AddScoped<IOrderItemLogic, OrderItemLogic>();

            //Add Mapper
            services.AddAutoMapper(typeof(AutoMapper));
            #endregion Business Logic
            return services;
        }

        public static void AddCors(this IServiceCollection Services)
        {
            Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());
            });
        }

        public static void AddJwtToken(this IServiceCollection Services, string ValidAudience, string ValidIssuer, string Secret)
        {
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken                 = true;
                options.RequireHttpsMetadata      = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer   = true,
                    ValidateAudience = true,
                    ValidAudience    = ValidAudience,
                    ValidIssuer      = ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret))
                };
            });
        }

    }
}
