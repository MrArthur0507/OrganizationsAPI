using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T>
    {
        public IEnumerable<T> GetAll()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source = mydb.db"))
            {
                connection.Open();
                string tableName = typeof(T).Name;
                string query = $"SELECT * FROM {tableName};";

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        List<T> entities = new List<T>();
                        while (reader.Read())
                        {
                            T entity = MapDataReaderToEntity(reader);
                            entities.Add(entity);
                        }
                        return entities;
                    }
                }

            }
        }

        public T GetById(string id)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source = mydb.db"))
            {
                connection.Open();
                string tableName = typeof(T).Name;
                string query = $"SELECT * FROM {tableName} WHERE {tableName}Id = @Id;";

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapDataReaderToEntity(reader);
                        }
                        return default(T);
                    }
                }
            }
        }




        private T MapDataReaderToEntity(SqliteDataReader reader)
        {
            T entity = Activator.CreateInstance<T>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var property = typeof(T).GetProperty(reader.GetName(i));
                if (property != null)
                {
                    if (property.PropertyType == typeof(int))
                    {
                        if (!reader.IsDBNull(i))
                        {
                            property.SetValue(entity, reader.GetInt32(i));
                        }
                    }
                    else
                    {
                        property.SetValue(entity, Convert.ChangeType(reader.GetValue(i), property.PropertyType));
                    }
                }
            }

            return entity;
        }
    }
}
