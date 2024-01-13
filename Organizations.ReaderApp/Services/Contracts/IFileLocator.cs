using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.ReaderApp.Services.Contracts
{
    public interface IFileLocator
    {

        public Queue<string> LocateCSVFiles();
        
    }
}
