using Microsoft.Extensions.Configuration;
using ShopWorld.DataAccessLayer;
using ShopWorld.Shared;
using ShopWorld.Shared.Entities;
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
        private readonly GenericRepository<Customer> CustomerRepository;
        private readonly IConfiguration _configuration;
        public AuthorizationLogic(IUnitOfWork unitOfWork,IConfiguration configuration) { 
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            CustomerRepository=unitOfWork.GetRepository<Customer>();
        }

        public LoginResult Login(string MobileNumber)
        {
            LoginResult result = new LoginResult();
            List<Claim> claims = new List<Claim>();
            DateTime expiration= DateTime.Now.AddDays(7);
            
            Customer? customer = CustomerRepository.Get(c=>c.Mobile==MobileNumber).FirstOrDefault();
            
            if (customer==null)
            {
                result.IsAuthorized = false;
            }
            else
            {
                result.IsAuthorized= true;
                
                #region Claims
                claims.Add(new Claim("CustomerId", $"{customer.CustomerId}"));
                claims.Add(new Claim(ClaimTypes.Name, $"{customer.Name} {customer.Surname}"));
                claims.Add(new Claim(ClaimTypes.Role, "Customer"));
                #endregion Claims

                result.JwtToken = JwtTokenWriter.WriteTokenAsString(
                    _configuration["JWT:Secret"],
                    _configuration["JWT:ValidIssuer"],
                    _configuration["JWT:ValidAudience"],
                    expiration,claims);
            }
            return result;  
        }

        public LoginResult LoginAsAdmin()
        {
            List<Claim> claims = new List<Claim>();
            LoginResult result = new LoginResult();
            DateTime expiration=DateTime.Now.AddDays(7);
            result.IsAuthorized = true;

            #region Claims
            claims.Add(new Claim("CustomerId", "0"));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            #endregion Claims

            result.JwtToken = JwtTokenWriter.WriteTokenAsString(
                _configuration["JWT:Secret"],
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                expiration, claims);
            return result;
        }
    }
}
