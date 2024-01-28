using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Models.DTO
{
    public class LoginResult
    {
        public bool status { get; set; }

        public Account Account { get; set; }
    }
}
