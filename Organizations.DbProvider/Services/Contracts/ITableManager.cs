using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Services.Contracts
{
    public interface ITableManager
    {
        public void CreateTables(string dbPath);
    }
}
