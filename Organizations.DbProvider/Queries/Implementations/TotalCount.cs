using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Base;
using Organizations.DbProvider.Queries.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Queries.Implementations
{
    public class TotalCount : BaseDbComponent, ITotalCount
    {
        private long GetTotal(string query)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source = {DbFile}"))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    long total = (long)command.ExecuteScalar();
                    return total;
                }
            }
        }

        public long GetTotalCountriesAddedToday()
        {
            return GetTotal("SELECT COUNT(CountryId) FROM Country WHERE Date(TimeAdded) = Date('now') AND IsDeleted = 0");
        }

        public long GetTotalIndustriesAddedToday()
        {
            return GetTotal("SELECT COUNT(IndustryId) FROM Industry WHERE Date(TimeAdded) = Date('now') AND IsDeleted = 0");
        }

        public long GetTotalOrganizationsAddedToday()
        {
            return GetTotal("SELECT COUNT(*) FROM Organization WHERE Date(TimeAdded) = Date('now') AND IsDeleted = 0");
        }
    }
}
