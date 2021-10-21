using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SpyPointData
{
    class Settings
    {
        public Settings()
        {

        }
        public void Load(string file)
        {
            string json = File.ReadAllText(file);
            DataCollection data = JsonConvert.DeserializeObject<DataCollection>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
        public void Save(string file)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.Write(json);
            }
        }
    }


}
