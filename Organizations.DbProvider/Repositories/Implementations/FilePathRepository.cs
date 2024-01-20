using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.DbProvider.Tools.Implementations;
using Organizations.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Implementations
{
    public class FilePathRepository : GenericRepository<FilePath>, IFilePathRepository
    {
        public void AddFile(FilePath file)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source = {DbFile}"))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO FilePath (Path, TimeWhenRead) VALUES (@Path, @TimeWhenRead)";
                    command.Parameters.AddWithValue("@Path", file.Path);
                    command.Parameters.AddWithValue("@TimeWhenRead", file.TimeWhenRead);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
