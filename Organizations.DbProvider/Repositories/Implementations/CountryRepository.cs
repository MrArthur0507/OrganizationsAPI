using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Implementations
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public int AddCountry(Country country)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source = mydb.db"))
            {
                connection.Open();
                int existringCountry = GetCountryIdByName(country.Name);

            if (existringCountry != -1) {
                return existringCountry;
            } else
            {
                string query = "INSERT INTO Country (Name) VALUES (@Name);";
                
                    
                    using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@Name", country.Name);
                        command.ExecuteNonQuery();
                        return GetCountryIdByName(country.Name);
                    }
                }
            }
        }

        public void AddCountries(HashSet<Country> countries)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source = mydb.db"))
            {
                connection.Open();

                foreach (var country in countries)
                {
                    int existringCountry = GetCountryIdByName(country.Name);

                    if (existringCountry != -1)
                    {
                        continue;
                    }
                    else
                    {
                        string query = "INSERT INTO Country (Name) VALUES (@Name);";
                        using (SqliteCommand command = connection.CreateCommand())
                        {
                            command.CommandText = query;
                            command.Parameters.AddWithValue("@Name", country.Name);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                
            }
        }

        public int GetCountryIdByName(string name)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source = mydb.db"))
            {
                string query = "SELECT CountryId FROM Country WHERE Name = @Name;";
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@Name", name);

                    object response = command.ExecuteScalar();

                    if (response != null && response != DBNull.Value)
                    {
                        return Convert.ToInt32(response);
                    }
                    else
                    {

                        return -1;
                    }
                }
            }

        }
    }
}
