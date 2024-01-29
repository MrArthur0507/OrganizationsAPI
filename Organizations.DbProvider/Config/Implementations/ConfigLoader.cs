using Newtonsoft.Json;
using Organizations.DbProvider.Config.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Config.Implementations
{
    public class ConfigLoader : IConfigLoader
    {
        private readonly string filePath = "C:\\Users\\Bozhidar\\Desktop\\webapi2\\OrganizationsAPI\\Config\\config.json";
        public ITableCreationConfig LoadConfig()
        {
            string jsonFile = GetJsonContentFromFile(filePath);
            TableCreationConfig tableCreationConfig = JsonConvert.DeserializeObject<TableCreationConfig>(jsonFile);

            return tableCreationConfig;
        }

        private string GetJsonContentFromFile(string file)
        {
            string response;
            if(!File.Exists(file))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    TableCreationConfig config = new TableCreationConfig() { OrganizationCreation = "test", IndustryCreation = "test", CountryCreation = "test" };
                    writer.Write(JsonConvert.SerializeObject(config));
                }
            }
            

            using (StreamReader sr = new StreamReader(file))
            {
                response = sr.ReadToEnd();
            }


            return response;
        }

        
    }
}
