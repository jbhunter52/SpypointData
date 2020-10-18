using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyPointSettings
{
    public class MutiShotOptions
    {
        public multishot multishot;

        public MutiShotOptions(multishot m)
        {
            multishot = m;
        }
        public string GetJson()
        {
            //{"delay":"1min"}
            return "{\"multiShot\":" + multishot.ToString().Replace("_", "") + "}";
        }
    }
    public enum multishot
    {
        _1,
        _2,
    }
}
