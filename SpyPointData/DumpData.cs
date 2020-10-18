using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTracker;
using System.IO;

namespace SpyPointData
{
    public class DumpData
    {
        public DataCollection Data;
        public DarkSkyData Weather;

        public DumpData(DataCollection dc, DarkSkyData weather)
        {
            Data = dc;
            Weather = weather;
        }


        public void DumpPhotos(string file)
        {
            List<Photo> rows = GetAllPhotos();
            StreamWriter sw = new StreamWriter(file, false);

            sw.Write(GetHeaderPhoto());
            foreach (Photo p in rows)
            {
                sw.Write(GetRowPhoto(p));
            }
            sw.Close();
        }
        public List<Photo> GetAllPhotos()
        {
            List<Photo> photos = new List<Photo>();
            foreach (var spc in Data.Connections)
            {
                foreach (var kvp in spc.CameraPictures)
                {
                    CameraPics cp = kvp.Value;
                    foreach (var p in cp.photos)
                    {
                        photos.Add(p);
                    }
                }
            }

            return photos;
        }
        public string GetHeaderPhoto()
        {
            List<string> list = new List<string>();

            list.Add("ID");
            list.Add("Date");
            list.Add("Buck");
            list.Add("Deer");
            list.Add("BuckAge");
            list.Add("CameraName");
            list.Add("HaveLocation");
            list.Add("Latitude");
            list.Add("Longitude");
            list.Add("\n");
            return String.Join(",", list);
        }
        public string GetRowPhoto(Photo p)
        {
            List<string> list = new List<string>();

            list.Add(p.id);
            list.Add(p.originDate.ToString("MM/dd/yyyy H:mm:ss"));
            list.Add(p.Buck.ToString());
            list.Add(p.Deer.ToString());
            list.Add(p.BuckAge.ToString());
            list.Add(p.CameraName);
            list.Add(p.HaveLocation.ToString());
            list.Add(p.Latitude.ToString());
            list.Add(p.Longitude.ToString());
            list.Add("\n");
            return String.Join(",", list);
        }

        public void DumpWeather(string file)
        {
            StreamWriter sw = new StreamWriter(file, false);
            sw.Write(GetHeaderWeather());
            foreach (var dsd in Weather.Data)
            {
                foreach (var d in dsd.hourly.data)
                {
                    sw.Write(GetRowWeather(d));
                }
            }
            sw.Close();
        }
        public string GetHeaderWeather()
        {
            List<string> list = new List<string>();
            list.Add("time");
            list.Add("apparentTemperature");
            list.Add("cloudCover");
            list.Add("dewPoint");
            list.Add("humidity");
            list.Add("icon");
            list.Add("ozone");
            list.Add("precipIntensity");
            list.Add("precipProbability");
            list.Add("precipType");
            list.Add("pressure");
            list.Add("summary");
            list.Add("temperature");
            list.Add("uvIndex");
            list.Add("visibility");
            list.Add("windBearing");
            list.Add("windGust");
            list.Add("windSpeed");
            list.Add("\n");
            return String.Join(",", list);
        }
        public string GetRowWeather(Datum d)
        {
            List<string> list = new List<string>();
            list.Add(d.time.ToString());
            list.Add(d.apparentTemperature.ToString());
            list.Add(d.cloudCover.ToString());
            list.Add(d.dewPoint.ToString());
            list.Add(d.humidity.ToString());
            list.Add(d.icon);
            list.Add(d.ozone.ToString());
            list.Add(d.precipIntensity.ToString());
            list.Add(d.precipProbability.ToString());
            list.Add(d.precipType);
            list.Add(d.pressure.ToString());
            list.Add(d.summary);
            list.Add(d.temperature.ToString());
            list.Add(d.uvIndex.ToString());
            list.Add(d.visibility.ToString());
            list.Add(d.windBearing.ToString());
            list.Add(d.windGust.ToString());
            list.Add(d.windSpeed.ToString());
            list.Add("\n");
            return String.Join(",", list);
        }
    }

    public class Observation
    {
        public string Id;
        public DateTime Date;
        public bool Buck;
        public bool Deer;
        public int BuckAge;
        public string CameraName;
        public bool HaveLocation;
        public double Latitude;
        public double Longitude;
    }
}
