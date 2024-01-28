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
    public class CountryQuery : BaseDbComponent, ICountryQuery
    {
        public List<CountryOrganization> GetOrganizationsForCountries()
        {
         
            List<CountryOrganization> countryOrganizations = new List<CountryOrganization>();
            using (SqliteConnection connection = new SqliteConnection($"Data Source = {DbFile}"))
            {
                connection.Open();
                string query =
                "SELECT c.Name AS CountryName, COUNT(o.OrganizationId) AS OrganizationCount " +
                "FROM Country c " +
                "INNER JOIN Organization o ON o.CountryId = c.CountryId " +
                "GROUP BY c.Name";

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CountryOrganization countryOrganization = new CountryOrganization
                            {
                                CountryName = reader["CountryName"].ToString(),
                                OrganizationCount = Convert.ToInt32(reader["OrganizationCount"])
                            };

                            countryOrganizations.Add(countryOrganization);
                        }
                    }
                }
            }

            return countryOrganizations;
        }
    }
}
