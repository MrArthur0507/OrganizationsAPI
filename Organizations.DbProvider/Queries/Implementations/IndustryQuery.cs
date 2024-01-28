using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Base;
using Organizations.DbProvider.Queries.Contracts;
using Organizations.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Queries.Implementations
{
    public class IndustryQuery : BaseDbComponent, IIndustryQuery
    {
        public List<IndustryEmployee> GetTotalEmployeesForIndustry()
        {
            string connectionString = $"Data Source= {DbFile}";
            List<IndustryEmployee> industryEmployees = new List<IndustryEmployee>();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = 
                "SELECT i.Name AS IndustryName, SUM(o.NumberOfEmployees) AS TotalEmployees " +
                "FROM Industry i " +
                "INNER JOIN Organization o ON o.IndustryId = i.IndustryId " +
                "GROUP BY i.Name";

                using (SqliteCommand command = connection.CreateCommand())
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IndustryEmployee industrySummary = new IndustryEmployee
                            {
                                IndustryName = reader["IndustryName"].ToString(),
                                TotalEmployees = Convert.ToInt32(reader["TotalEmployees"])
                            };

                            industryEmployees.Add(industrySummary);
                        }
                    }
                }
            }

            return industryEmployees;
        }
    }
}
