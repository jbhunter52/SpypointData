using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyPointData
{
    public class FilterCriteria
    {
        public bool New;
        public bool Nothing;
        public bool Buck;
        public bool Deer;
        public bool Age0;
        public bool Age1;
        public bool Age2;
        public bool Age3;
        public bool Age4;
        public bool Date;
        public DateTime MinDate;
        public DateTime MaxDate;
        public FilterCriteria()
        {
            New = false;
            Buck = false;
            Deer = false;
            Age0 = false;
            Age1 = false;
            Age2 = false;
            Age3 = false;
            Age4 = false;
            MinDate = new DateTime(2017, 1, 1,0,0,0);
            MaxDate = DateTime.Today;
        }
    }
}
