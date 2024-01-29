using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Base;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.DbProvider.Tools.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Implementations
{
    public class GenericRepository<T> : BaseDbComponent, IGenericRepository<T>
    {
        public IEnumerable<T> GetAll()
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source = {DbFile}"))
            {
                connection.Open();
                string tableName = typeof(T).Name;
                string query = $"SELECT * FROM {tableName} WHERE IsDeleted = 0;"; 

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
            using (SqliteConnection connection = new SqliteConnection($"Data Source = {DbFile}"))
            {
                connection.Open();
                string tableName = typeof(T).Name;
                string query = $"SELECT * FROM {tableName} WHERE {tableName}Id = @Id AND IsDeleted = 0;"; 

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
        public virtual bool DeleteById(string id)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source = {DbFile}"))
            {
                connection.Open();
                string tableName = typeof(T).Name;
                string query = $"UPDATE {tableName} SET IsDeleted = 1 WHERE {tableName}Id = @Id;";

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception)
                    {
                        return false;
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
