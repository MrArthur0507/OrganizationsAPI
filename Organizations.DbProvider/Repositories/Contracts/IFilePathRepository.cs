using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Contracts
{
    public interface IFilePathRepository : IGenericRepository<FilePath>
    {
        public void AddFile(FilePath file);
    }
}
