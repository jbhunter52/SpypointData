using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

namespace SpyPointSettings
{
    public class SPSSettings
    {
        public delay DayDelay;
        public multishot DayMultiShot;
        public delay NightDelay;
        public multishot NightMultiShot;

        public SPSSettings()
        {
            DayDelay = delay._30min;
            DayMultiShot = multishot._1;
            NightDelay = delay._instant;
            NightMultiShot = multishot._2;
        }

        public void Load(string file)
        {
            if (!File.Exists(file))
            {
                var newSettings = new SPSSettings();
                DayDelay = newSettings.DayDelay;
                DayMultiShot = newSettings.DayMultiShot;
                NightDelay = newSettings.NightDelay;
                NightMultiShot = newSettings.NightMultiShot;
                return;
            }
            string json = File.ReadAllText(file);
            SPSSettings spss = JsonConvert.DeserializeObject<SPSSettings>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            DayDelay = spss.DayDelay;
            DayMultiShot = spss.DayMultiShot;
            NightDelay = spss.NightDelay;
            NightMultiShot = spss.NightMultiShot;

        }
        public void Save(string file)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            string dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.Write(json);
            }
        }
    }
}
