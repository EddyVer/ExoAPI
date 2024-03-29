﻿using System.Security.Claims;

namespace ExoAPI.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor) 
        { 
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserName()
        {
            string result = string.Empty;
            if(_httpContextAccessor.HttpContext != null ) 
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result; 
        }
    }
}
