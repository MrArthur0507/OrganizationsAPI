using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Models.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        public string Username { get; set; }

        public byte[] Salt { get; set; }

        public byte[] HashedPassword { get; set; }
    }
}
