using Organizations.Models.DTO;
using Organizations.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserManagement _userManagement;
        private readonly IJwtGenerator _jwtGenerator;

        public AccountService(IUserManagement userManagement, IJwtGenerator jwtGenerator)
        {
            _userManagement = userManagement;
            _jwtGenerator = jwtGenerator;   
        }

        public string RegisterUser(string username, string password)
        {
            return _userManagement.RegisterUser(username, password);
        }

        public string LoginUser(string username, string password)
        {
            LoginResult lg = _userManagement.LoginUser(username, password);
            if (lg.status)
            {
                string jwt = _jwtGenerator.GenerateToken(lg.Account);
                return jwt;
            }
            return null;
        }
    }
}
