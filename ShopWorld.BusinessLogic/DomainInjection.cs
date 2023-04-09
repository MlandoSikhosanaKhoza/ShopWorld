using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShopWorld.DataAccessLayer;
using System.Text;

namespace ShopWorld.BusinessLogic
{
    public class DomainInjection
    {
        public static void InjectBusinessLogic(IServiceCollection Services)
        {
            //Services.AddScoped<IAuthorizationLogic, AuthorizationLogic>();
            Services.AddScoped<IEmployeeLogic, EmployeeLogic>();
            Services.AddScoped<ICustomerLogic, CustomerLogic>();
            Services.AddScoped<IItemLogic, ItemLogic>();
            Services.AddScoped<IOrderLogic, OrderLogic>();
            Services.AddScoped<IOrderItemLogic, OrderItemLogic>();
            
            Services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void InjectCors(IServiceCollection Services)
        {
            Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());
            });
        }

        public static void InjectJwtTokens(IServiceCollection Services,string ValidAudience,string ValidIssuer,string Secret)
        {
            Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = ValidAudience,
                    ValidIssuer = ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret))
                };
            });
        }

        //public static void InjectJsonOptions(IServiceCollection Services)
        //{
        //    Services.AddControllers(options =>
        //    {
        //        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        //    }).AddJsonOptions(options =>
        //    {
        //        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        //        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //    });
        //}
    }
}