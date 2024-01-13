using CsvHelper;
using Organizations.ReaderApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.ReaderApp.Services.Implementations
{
    public class Reader<T> : IReader<T>
    {
        private readonly IReaderTracker _readerTracker;
        public Reader(IReaderTracker readerTracker) {
            _readerTracker = readerTracker;
        }
        public IList<T> Read()
        {
            List<T> records = new List<T>();
            while (_readerTracker.HasNext())
            {
                
                string path = _readerTracker.Next();
                if (path != "-1")
                {
                    using (var reader = new StreamReader(path))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        records.AddRange(csv.GetRecords<T>().ToHashSet());

                    }
                }
            }
           
            return records;
        }
    }
}
