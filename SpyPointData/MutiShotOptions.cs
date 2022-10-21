using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyPointSettings
{
    public class MutiShotOptions
    {
        public multishot_micro multishot_micro;

        public MutiShotOptions(multishot_micro m)
        {
            multishot_micro = m;
        }
        public string GetJson(string cameraModel)
        {
            if (cameraModel.ToLower().StartsWith("flex"))
            {
                //return "{\"config\":" + "{\"multiShot\":\"" + multishot_micro.ToString().Replace("_", "") + "\"}}";
                return "{\"multiShot\":" + multishot_micro.ToString().Replace("_", "") + "}";

            }
            else
            {
                //{"multiShot":"1min"}
                return "{\"multiShot\":" + multishot_micro.ToString().Replace("_", "") + "}";
            }
        }
    }
    public enum multishot_micro
    {
        _1,
        _2,
    }
}
