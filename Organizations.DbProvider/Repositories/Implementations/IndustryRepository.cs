﻿using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Implementations
{
    public class IndustryRepository : GenericRepository<Industry>, IIndustryRepostiory
    {
        public int AddIndustry(Industry industry)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=mydb.db"))
            {
                connection.Open();
                int existingIndustry = GetIndustryIdByName(industry.Name, connection);

                if (existingIndustry != -1)
                {
                    return existingIndustry;
                }
                else
                {
                    string query = "INSERT INTO Industry (Name) VALUES (@Name);";

                    using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@Name", industry.Name);
                        command.ExecuteNonQuery();
                        return GetIndustryIdByName(industry.Name, connection);
                    }
                }
            }
        }

        public void AddIndustries(HashSet<Industry> industries)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=mydb.db"))
            {
                connection.Open();
                foreach (var industry in industries)
                {
                    int existingIndustry = GetIndustryIdByName(industry.Name, connection);

                    if (existingIndustry != -1)
                    {
                        continue;
                    }
                    else
                    {
                        string query = "INSERT INTO Industry (Name) VALUES (@Name);";
                        using (SqliteCommand command = connection.CreateCommand())
                        {
                            command.CommandText = query;
                            command.Parameters.AddWithValue("@Name", industry.Name);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public int GetIndustryIdByName(string name, SqliteConnection connection)
        {
            string query = "SELECT IndustryId FROM Industry WHERE Name = @Name;";
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