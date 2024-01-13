using Organizations.DbProvider.Services.Contracts;
using Organizations.DbProvider.Tools.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Services.Implementations
{
    public class SqliteDbManager : IDbManager
    {
        private readonly string dbName = "mydb.db";

        private readonly ILogger _logger;
        private readonly ITableManager _tableManager;

        public SqliteDbManager(ILogger logger, ITableManager tableManager)
        {
            _logger = logger;
            _tableManager = tableManager;
        }

        public void LoadDb()
        {
            if (File.Exists(dbName))
            {
                _logger.Log("Db exists");
            } else
            {
                _logger.Log("Starting to create db");
                File.Create(dbName).Close();
                _tableManager.CreateTables(dbName);
            }
        }

    }
}
