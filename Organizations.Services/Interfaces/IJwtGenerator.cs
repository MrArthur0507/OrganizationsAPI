using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Interfaces
{
    public interface IJwtGenerator
    {
        public string GenerateToken(string username);
    }
}
