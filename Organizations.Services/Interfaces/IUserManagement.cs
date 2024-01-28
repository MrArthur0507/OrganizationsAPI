using Organizations.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Interfaces
{
    public interface IUserManagement
    {
        public string RegisterUser(string username, string password);

        public LoginResult LoginUser(string username, string password);

    }
}
