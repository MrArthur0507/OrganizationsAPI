using Organizations.DbProvider.Repositories.Contracts;
using Organizations.Models.Models;
using Organizations.ReaderApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.ReaderApp.Services.Implementations
{
    public class ReaderTracker : IReaderTracker
    {
        private readonly IFileLocator _fileLocator;
        private readonly IFilePathRepository _filePathRepository;
        public Dictionary<string, bool> ReadFiles = new Dictionary<string, bool>();
        private Queue<string> paths;
        public ReaderTracker(IFileLocator locator, IFilePathRepository filePathRepository) { 
            _fileLocator = locator;
            paths = _fileLocator.LocateCSVFiles();
            _filePathRepository = filePathRepository;
            ReadFiles = GetReadFiles(_filePathRepository.GetAll());
            
        }
        public string Next()
        {
            string path = paths.Dequeue();
            if (!CheckIfFileIsRead(path))
            {
                MarkFileAsRead(path);
                return path;
            } 
            return "-1";
        }

        public bool HasNext()
        {
            if (paths.Count == 0)
            {
                return false;
            }
            return true;
        }
        private bool CheckIfFileIsRead(string fileName)
        {
            ReadFiles.TryGetValue(fileName, out bool result);
            if (!result)
            {
                return false;
            }
            return true;
        }

        private void MarkFileAsRead(string fileName)
        {
            _filePathRepository.AddFile(new FilePath() { Path = fileName, TimeWhenRead = DateTime.Now });
        }

        private Dictionary<string, bool> GetReadFiles(IEnumerable<FilePath> files)
        {
            Dictionary<string, bool> readFiles = new Dictionary<string, bool>();
            foreach (var item in files)
            {
                readFiles.Add(item.Path, true);
            }
            return readFiles;
        }
    }
}
