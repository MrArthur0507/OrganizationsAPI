using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Tools.Implementations
{
    public struct DbSetting
    {
        public DbSetting()
        {
            DbFile = "C:\\Users\\mrart\\source\\repos\\OrganizationsManager\\Data\\mydb.db";
        }

        public readonly string DbFile;

        
    }
}
