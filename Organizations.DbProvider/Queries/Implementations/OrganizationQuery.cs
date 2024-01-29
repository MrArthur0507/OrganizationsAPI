using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Base;
using Organizations.DbProvider.Queries.Contracts;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Queries.Implementations
{
    public class OrganizationQuery : BaseDbComponent, IOrganizationQuery
    {
        public List<Organization> GetTopOrganizationsByEmployees(int topCount)
        {
            List<Organization> organizations = new List<Organization>();

            using (SqliteConnection connection = new SqliteConnection($"Data Source={DbFile}"))
            {
                connection.Open();

                string query = $"SELECT * FROM Organization WHERE IsDeleted = 0 ORDER BY NumberOfEmployees DESC LIMIT @top";

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@top", topCount);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Organization organization = MapOrganizations(reader);
                            organizations.Add(organization);
                        }
                    }
                }
            }

            return organizations;
        }

        private Organization MapOrganizations(SqliteDataReader reader)
        {
            return new Organization
            {
                OrganizationId = reader.GetString(reader.GetOrdinal("OrganizationId")),
                Index = reader.GetString(reader.GetOrdinal("Index")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Website = reader.GetString(reader.GetOrdinal("Website")),
                CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Founded = reader.GetInt32(reader.GetOrdinal("Founded")),
                IndustryId = reader.GetInt32(reader.GetOrdinal("IndustryId")),
                NumberOfEmployees = reader.GetInt32(reader.GetOrdinal("NumberOfEmployees")),
            };
        }
    }
}
