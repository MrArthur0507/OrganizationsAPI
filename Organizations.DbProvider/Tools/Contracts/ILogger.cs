using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Tools.Contracts
{
    public interface ILogger
    {
        public void Log(string message);
    }
}
