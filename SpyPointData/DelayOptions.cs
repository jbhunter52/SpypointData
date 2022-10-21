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
                int delaySecs = 0;
                if (delay_micro == delay_micro._instant) delaySecs = 0;
                else if (delay_micro == delay_micro._10s) delaySecs = 10;
                else if (delay_micro == delay_micro._1min) delaySecs = 1 * 60;
                else if (delay_micro == delay_micro._3min) delaySecs = 3 * 60;
                else if (delay_micro == delay_micro._5min) delaySecs = 5 * 60;
                else if (delay_micro == delay_micro._10min) delaySecs = 10 * 60;
                else if (delay_micro == delay_micro._15min) delaySecs = 15 * 60;
                else if (delay_micro == delay_micro._30min) delaySecs = 30 * 60;

                //return "{\"config\":" + "{\"motionDelay\":\"" + delay_micro.ToString().Replace("_", "") + "\"}}";
                return "{\"motionDelay\":" + delaySecs.ToString() + "}";

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
