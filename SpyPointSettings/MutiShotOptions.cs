using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyPointData
{
    public class MutiShotOptions
    {
        public multishot_micro multishot_micro;

        public MutiShotOptions(multishot_micro m)
        {
            multishot_micro = m;
        }
        public string GetJson()
        {
            //{"delay":"1min"}
            return "{\"delay\":\"" + multishot_micro.ToString().Replace("_", "") + "\"}";
        }
    }
    public enum multishot_micro
    {
        _1,
        _2,
    }
}
