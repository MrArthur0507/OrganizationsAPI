using Organizations.ReaderApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.ReaderApp.Services.Implementations
{
    public class FileLocator : IFileLocator
    {
        private string path;
        public FileLocator(string path) { 
            this.path = path;
        }

        public Queue<string> LocateCSVFiles()
        {
            string[] files = Directory.GetFiles(path);
            Queue<string> paths = new Queue<string>();

            foreach (var item in files)
            {
                if (item.EndsWith(".csv"))
                {
                    paths.Enqueue(item);
                }
            }
            return paths;
        }
    }
}
