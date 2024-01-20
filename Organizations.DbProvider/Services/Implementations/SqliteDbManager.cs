using Organizations.DbProvider.Base;
using Organizations.DbProvider.Services.Contracts;
using Organizations.DbProvider.Tools.Contracts;
using Organizations.DbProvider.Tools.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Services.Implementations
{
    public class SqliteDbManager : BaseDbComponent, IDbManager
    {
        

        private readonly ILogger _logger;
        private readonly ITableManager _tableManager;
        public SqliteDbManager(ILogger logger, ITableManager tableManager)
        {
            _logger = logger;
            _tableManager = tableManager;
        }

        public void LoadDb()
        {
            if (File.Exists(DbFile))
            {
                _logger.Log("Db exists");
            } else
            {
                _logger.Log("Starting to create db");
                File.Create(DbFile).Close();
                _tableManager.CreateTables(DbFile);
            }
        }

    }
}
