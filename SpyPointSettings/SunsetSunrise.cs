using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace SpyPointSettings
{
    public class SunsetSunrise
    {
        public string Latitude = "37.987525510757038";
        public string Longitude = "-89.485037326812744";
        public string Response = "";

        public DateTime Sunrise;
        public DateTime Sunset;
        public DateTime AfterSunrise;
        public DateTime BeforeSunset;
        public DateTime ShootTimeMorn;
        public DateTime ShootTimeEve;
        public string format = "M/d/yyyy h:m:s tt";

        public SunriseSunsetResult Result;
        public SunsetSunrise(int minOffset)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string url = String.Format("https://api.sunrise-sunset.org/json?lat={0}&lng={1}&date={2}",Latitude, Longitude,date);

            string response = "";
            using (var client = new WebClient()) // WebClient class inherits IDisposable
            {
                response = client.DownloadString(url);

                // parse the 'authenticity_token' and cookie is auto handled by the cookieContainer
                //string token = Regex.Match(response, "authenticity_token.+value=\"(.+)\"").Groups[1].Value;
                //string encodedToken = System.Web.HttpUtility.UrlEncode(token);
                //string postData = "{\"username\":\"" + username + "\",\"password\":\"" + pass + "\"}";

                //client.Headers.Add("Referer", "https://webapp.spypoint.com/login");
                //client.Method = "POST";
                //response = client.UploadString(new Uri("https://restapi.spypoint.com/api/v3/user/login"), postData);
            }
            Response = response;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            Result = JsonConvert.DeserializeObject<SunriseSunsetResult>(response, settings);

            UpdateTimes(minOffset);
        }

        private void UpdateTimes(int minOffset)
        {
            var dt = DateTime.Now;
            Sunrise = DateTime.ParseExact(DateTime.Now.ToShortDateString() + " " + Result.results.sunrise, format, System.Globalization.CultureInfo.InvariantCulture).ToLocalTime();
            Sunset = DateTime.ParseExact(DateTime.Now.ToShortDateString() + " " + Result.results.sunset, format, System.Globalization.CultureInfo.InvariantCulture).ToLocalTime();
            Sunrise = new DateTime(dt.Year, dt.Month, dt.Day, Sunrise.Hour, Sunrise.Minute, Sunrise.Second);
            Sunset = new DateTime(dt.Year, dt.Month, dt.Day, Sunset.Hour, Sunset.Minute, Sunset.Second);

            AfterSunrise = Sunrise.AddMinutes(minOffset);
            BeforeSunset = Sunset.Subtract(new TimeSpan(0,minOffset,0));

            ShootTimeMorn = Sunrise.Subtract(new TimeSpan(0, 30, 0));
            ShootTimeEve = Sunset.Add(new TimeSpan(0, 30, 0));
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Results
        {
            public string sunrise { get; set; }
            public string sunset { get; set; }
            public string solar_noon { get; set; }
            public string day_length { get; set; }
            public string civil_twilight_begin { get; set; }
            public string civil_twilight_end { get; set; }
            public string nautical_twilight_begin { get; set; }
            public string nautical_twilight_end { get; set; }
            public string astronomical_twilight_begin { get; set; }
            public string astronomical_twilight_end { get; set; }
        }

        public class SunriseSunsetResult
        {
            public Results results { get; set; }
            public string status { get; set; }
        }
    }
}
