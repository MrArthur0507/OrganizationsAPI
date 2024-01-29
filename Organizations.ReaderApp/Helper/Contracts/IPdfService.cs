using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.ReaderApp.Helper.Contracts
{
    public interface IPdfService
    {
        public void GenerateAndSavePdf(string filePath);
    }
}
