using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;
using SpyPointData;

namespace SpyPointData
{
    public class SPSSettings
    {
        public SPSCameraSettings Micro;
        public SPSCameraSettings Flex;

        public SPSSettings(string file)
        {
            Load(file);
        }

        public delay_micro GetDelay(string model, DayNight dayNight)
        {
            var settings = GetSettings(model);
            if (dayNight == DayNight.Day)
                return settings.DayDelay;
            else
                return settings.NightDelay;
        }

        public multishot_micro GetMultiShot(string model, DayNight dayNight)
        {
            var settings = GetSettings(model);
            if (dayNight == DayNight.Day)
                return settings.DayMultiShot;
            else
                return settings.NightMultiShot;
        }

        public void SetDelay(string model, DayNight dayNight, delay_micro value)
        {
            var settings = GetSettings(model);
            if (dayNight == DayNight.Day)
                settings.DayDelay = value;
            else
                settings.NightDelay = value;
        }
        public void SetMultishot(string model, DayNight dayNight, multishot_micro value)
        {
            var settings = GetSettings(model);
            if (dayNight == DayNight.Day)
                settings.DayMultiShot = value;
            else
                settings.NightMultiShot = value;
        }

        public SPSCameraSettings GetSettings(string model)
        {
            model = model.ToLower();
            SPSCameraSettings settings = new SPSCameraSettings(model);
            if (model.Contains("micro"))
            {
                settings = Micro;
            }
            else if (model.Contains("flex"))
            {
                settings = Flex;
            }
            else if (model.Contains("lm2"))
            {
                settings = Micro;
            }
            return settings;
        }

        public void Load(string file)
        {
            if (!File.Exists(file))
            {
                Micro = new SPSCameraSettings("micro");
                Flex = new SPSCameraSettings("flex");
                return;
            }
            string json = File.ReadAllText(file);
            SPSSettings spss = JsonConvert.DeserializeObject<SPSSettings>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            Micro = spss.Micro;
            Flex = spss.Flex;
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

    public class SPSCameraSettings
    {
        string Model;
        public delay_micro DayDelay;
        public multishot_micro DayMultiShot;
        public delay_micro NightDelay;
        public multishot_micro NightMultiShot;

        public SPSCameraSettings(string model)
        {
            Model = model;
            DayDelay = delay_micro._30min;
            DayMultiShot = multishot_micro._1;
            NightDelay = delay_micro._1min;
            NightMultiShot = multishot_micro._2;
        }
    }

    public enum DayNight
    {
        Day,
        Night
    }
}
