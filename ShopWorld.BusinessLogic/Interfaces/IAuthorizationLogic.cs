using ShopWorld.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.BusinessLogic
{
    public interface IAuthorizationLogic
    {
        public LoginResult Login(string MobileNumber);
    }
}
