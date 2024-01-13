using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Config.Contracts
{
    public interface IConfigLoader
    {
        public ITableCreationConfig LoadConfig();
    }
}
