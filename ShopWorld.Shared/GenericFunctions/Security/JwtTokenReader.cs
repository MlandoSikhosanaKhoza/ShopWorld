﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.Shared
{
    public class JwtTokenReader
    {
        //Read a JWT Token
        public static string GetTokenValue(string JwtToken, string TokenField)
        {
            JwtSecurityToken token = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadJwtToken(JwtToken);
            string tokenValue = token.Claims?.Where(c => c.Type.Equals(TokenField)).FirstOrDefault()?.Value ?? "";
            return tokenValue;
        }
        //Read JWT Token from 
        public static JwtSecurityToken GetJwtToken(string JwtToken)
        {
            JwtSecurityToken token = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadJwtToken(JwtToken);
            return token;
        }
        //Read a JWT Token
        public static string? GetTokenValue(IHttpContextAccessor ContextAccessor, string TokenField)
        {
            JwtSecurityToken token = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadJwtToken(ContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ").Last());
            string tokenValue = token?.Claims.Where(c => c.Type.Equals(TokenField)).FirstOrDefault()?.Value??"";
            return tokenValue;
        }
    }
}
