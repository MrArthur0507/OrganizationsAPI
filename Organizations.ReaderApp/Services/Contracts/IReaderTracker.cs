using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.ReaderApp.Services.Contracts
{
    public interface IReaderTracker
    {
        public string Next();


        public bool HasNext();
        
    }
}
