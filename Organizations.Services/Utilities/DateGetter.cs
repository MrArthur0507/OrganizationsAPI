using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Utilities
{
    public class DateGetter : IDateGetter
    {
        public DateTime GetDate()
        {
            return DateTime.UtcNow;
        }
    }
}
