using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.DbProvider.Tools.Implementations;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Implementations
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {

        public bool AddAccount(Account account)
        {
            Account accountFromDb = GetAccountByUsername(account.Username);
            if (accountFromDb == null)
            {
                using (SqliteConnection connection = new SqliteConnection($"Data Source = {DbFile}"))
                {
                    connection.Open();

                    using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO Account (Username, Salt, HashedPassword) VALUES (@Username, @Salt, @HashedPassword)";
                        command.Parameters.AddWithValue("@Username", account.Username);
                        command.Parameters.AddWithValue("@Salt", account.Salt);
                        command.Parameters.AddWithValue("@HashedPassword", account.HashedPassword);

                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }

                    
                }
            }
            return false;
        }


        public Account GetAccountByUsername(string username)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source = {DbFile}"))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Account WHERE Username = @Username";
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Account
                            {
                                AccountId = reader.GetInt32(reader.GetOrdinal("AccountId")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                Salt = (byte[])reader["Salt"],
                                HashedPassword = (byte[])reader["HashedPassword"],
                                IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                            };
                        }
                    }
                }

                connection.Close();
            }

            return null;
        }
    }
}
