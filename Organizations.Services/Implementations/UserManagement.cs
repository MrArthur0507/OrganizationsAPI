using Organizations.DbProvider.Repositories.Contracts;
using Organizations.Models.Models;
using Organizations.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Implementations
{
    public class UserManagement : IUserManagement
    {
        
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;

        public UserManagement(IPasswordHasher passwordHasher, IAccountRepository accountRepository)
        {
            
            _passwordHasher = passwordHasher;
            _accountRepository = accountRepository;
        }

        public string RegisterUser(string username, string password)
        {
            
            byte[] salt = _passwordHasher.GenerateSalt();

            byte[] hashedPassword = _passwordHasher.HashPassword(password, salt);

            Account account = _accountRepository.GetAccountByUsername(username);

            if (account == null)
            {
                _accountRepository.AddAccount(new Account() { Username = username, AccountId = 1, Salt = salt, HashedPassword = hashedPassword });
                return "Registered successfuly!";
            }
            return "Username taken";

        }

        public bool LoginUser(string username, string password)
        {
            Account account = _accountRepository.GetAccountByUsername(username);
            if (account != null)
            {
                
                byte[] hashedPassword = _passwordHasher.HashPassword(password, account.Salt);

               
                return _passwordHasher.CompareByteArrays(hashedPassword, account.HashedPassword);
            }

            return false;
        }
    }
}
