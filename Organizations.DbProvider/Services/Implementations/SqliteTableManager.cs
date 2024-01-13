using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Config.Contracts;
using Organizations.DbProvider.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Services.Implementations
{
    public class SqliteTableManager : ITableManager
    {
        private readonly IConfigLoader _configLoader;
        private readonly ITableCreationConfig _tableCreationConfig;

        public SqliteTableManager(IConfigLoader configLoader)
        {
            _configLoader = configLoader;
            _tableCreationConfig = _configLoader.LoadConfig();
        }


        public void CreateTables(string dbPath)
        {
            PropertyInfo[] properties = _tableCreationConfig.GetType().GetProperties();
            foreach (var property in properties)
            {
                using (SqliteConnection connection = new SqliteConnection($"Data Source = {dbPath}"))
                {
                    connection.Open();
                    using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = property.GetValue(_tableCreationConfig).ToString();
                        command.ExecuteNonQuery();
                    }

                }
            }
            
        }

    }
}
