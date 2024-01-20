using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Contracts
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        public bool DeleteById(string id);
    }
}
