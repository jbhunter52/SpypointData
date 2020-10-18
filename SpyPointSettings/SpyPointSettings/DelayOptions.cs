using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyPointData
{
    public class DelayOptions
    {
        public delay delay;

        public DelayOptions(delay d)
        {
            delay = d;
        }
        public string GetJson()
        {
            //{"delay":"1min"}
            return "{\"delay\":\"" + delay.ToString().Replace("_","") + "\"}";
        }

    }
    public enum delay
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

    

