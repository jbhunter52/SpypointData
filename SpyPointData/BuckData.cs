using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SpyPointData
{
    public class BuckData
    {
        public List<BuckID> IDs { get; set; }
        public string DataFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpyPoint", "BuckID.json");

        public BuckData()
        {
            IDs = new List<BuckID>();
        }

        public void Load()
        {
            if (!File.Exists(DataFile))
            {
                Save();
            }

            string json = File.ReadAllText(DataFile);
            DataCollection data = JsonConvert.DeserializeObject<DataCollection>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
        public void Save()
        {
            string json = JsonConvert.SerializeObject(this);


            using (StreamWriter sw = new StreamWriter(DataFile))
            {
                sw.Write(json);
            }
        }

    }


    public class BuckID
    {
        public List<Photo> Photos { get; set; }
        public string Name { get; set; }

        public BuckID(string name)
        {
            Name = name;
            Photos = new List<Photo>();
        }
    }


}
