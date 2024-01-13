using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Interfaces
{
    public interface IPasswordHasher
    {
        public byte[] GenerateSalt();

        public byte[] HashPassword(string password, byte[] salt);

        public bool CompareByteArrays(byte[] array1, byte[] array2);

    }
}
