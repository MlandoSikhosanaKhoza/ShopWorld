using Microsoft.Extensions.Configuration;
using ShopWorld.DAL;
using ShopWorld.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.BusinessLogic
{
    public class AuthorizationLogic:IAuthorizationLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;
        public AuthorizationLogic(IUnitOfWork unitOfWork, IConfiguration configuration, ICustomerRepository customerRepository)
        {
            _unitOfWork         = unitOfWork;
            _configuration      = configuration;
            _customerRepository = customerRepository;
        }

        public LoginResult Login(string MobileNumber)
        {
            LoginResult result  = new LoginResult();
            List<Claim> claims  = new List<Claim>();
            DateTime expiration = DateTime.UtcNow.AddDays(7);
            
            Customer? customer = _customerRepository.GetCustomerByMobileNumber(MobileNumber);
            
            if (customer == null)
            {
                result.IsAuthorized = false;
            }
            else
            {
                result.IsAuthorized = true;
                
                #region Claims
                claims.Add(new Claim("CustomerId", $"{customer.CustomerId}"));
                claims.Add(new Claim(ClaimTypes.Name, $"{customer.Name} {customer.Surname}"));
                claims.Add(new Claim(ClaimTypes.Role, "Customer"));
                #endregion Claims

                result.JwtToken = JwtTokenWriter.WriteTokenAsString(
                    _configuration["JWT:Secret"]??"",
                    _configuration["JWT:ValidIssuer"]??"",
                    _configuration["JWT:ValidAudience"]??"",
                    expiration,claims);
            }
            return result;  
        }

        public LoginResult LoginAsAdmin()
        {
            List<Claim> claims  = new List<Claim>();
            LoginResult result  = new LoginResult();
            DateTime expiration = DateTime.UtcNow.AddDays(7);
            result.IsAuthorized = true;

            #region Claims
            claims.Add(new Claim("CustomerId", "0"));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            #endregion Claims

            result.JwtToken = JwtTokenWriter.WriteTokenAsString(
                _configuration["JWT:Secret"]??"",
                _configuration["JWT:ValidIssuer"]??"",
                _configuration["JWT:ValidAudience"]??"",
                expiration, claims);
            return result;
        }
    }
}
