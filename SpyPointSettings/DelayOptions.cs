using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyPointData
{
    public class DelayOptions
    {
        public delay_micro delay_micro;

        public DelayOptions(delay_micro d)
        {
            delay_micro = d;
        }
        public string GetJson(string cameraModel)
        {
            if (cameraModel.ToLower().StartsWith("flex"))
            {
                return "{\"delay\":\"" + delay_micro.ToString().Replace("_", "") + "\"}";
            }
            else
            {
                return "{\"delay\":\"" + delay_micro.ToString().Replace("_", "") + "\"}";
            }
        }

    }
    public enum delay_micro
    {
        _instant,
        _10s,
        _1min,
        _3min,
        _5min,
        _10min,
        _15min,
        _30min,
    }
}

    

