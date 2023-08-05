using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SpyPointData
{
    public class Signal
    {
        public int bar { get; set; }
        public int dBm { get; set; }
        public string type { get; set; }
        public int mcc { get; set; }
        public int mnc { get; set; }
        public Processed processed { get; set; } 
    }
    public class Processed
    {
        public int percentage { get; set; }
        public int bar { get; set; }
        public bool lowSignal { get; set; }
    }

    public class Memory
    {
        public int used { get; set; }
        public int size { get; set; }
    }

    public class Temperature
    {
        public int value { get; set; }
    }

    public class Capability
    {
        public bool survivalMode { get; set; }
        public bool hdRequest { get; set; }
    }

    public class Status
    {
        public string model { get; set; }
        public string version { get; set; }
        public int serial { get; set; }
        public DateTime lastUpdate { get; set; }
        public List<int> batteries { get; set; }
        public string batteryType { get; set; }
        public Signal signal { get; set; }
        public Memory memory { get; set; }
        public Temperature temperature { get; set; }
        public DateTime installDate { get; set; }
        public string modemFirmware { get; set; }
        public string sim { get; set; }
        public List<object> notifications { get; set; }
        public Capability capability { get; set; } 
    }

    public class Sensibility
    {
        public string level { get; set; }
        public int low { get; set; }
        public int medium { get; set; }
        public int high { get; set; }
    }

    public class TransmitTime
    {
        public int hour { get; set; }
        public int minute { get; set; }
    }

    public class Ssl
    {
        public bool use { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public int version { get; set; }
        public int cipher { get; set; }
    }

    public class Http
    {
        public string host { get; set; }
        public int port { get; set; }
        public Ssl ssl { get; set; }
    }

    public class Config
    {
        public string name { get; set; }
        public string batteryType { get; set; }
        public string captureMode { get; set; }
        public string dateFormat { get; set; }
        public string delay { get; set; }
        public int motionDelay { get; set; }
        public int multiShot { get; set; }
        public Sensibility sensibility { get; set; }
        public bool stamp { get; set; }
        public string quality { get; set; }
        public string temperatureUnit { get; set; }
        public int timeFormat { get; set; }
        public int transmitFreq { get; set; }
        public string transmitFormat { get; set; }
        public TransmitTime transmitTime { get; set; }
        public List<List<int>> schedule { get; set; }
        public bool transmitAuto { get; set; }
        public bool transmitUser { get; set; }
        public string triggerSpeed { get; set; }
        public int smallPicWidth { get; set; }
        public bool charger { get; set; }
        public bool debug { get; set; }
        public bool gps { get; set; }
        public string irIntensity { get; set; }
        public bool photoFirst { get; set; }
        public bool theftAlert { get; set; }
        public int timeLapse { get; set; }
        public int videoLength { get; set; }
        public bool capture { get; set; }
        public bool factory { get; set; }
        public object dateTime { get; set; }
        public Http http { get; set; }
    }
    public class Plan
    {
        public string name { get; set; }
        public string id { get; set; }
        public bool isActive { get; set; }
        public bool isFree { get; set; }
        public int photoCountPerMonth { get; set; }
        public int pricePerMonthIfPaidPerMonth { get; set; }
        public int pricePerMonthIfPaidAnnually { get; set; }
        public int pricePerYear { get; set; }
        public int rebateIfPaidAnnually { get; set; }
        public bool isUpgradable { get; set; }
        public bool isDowngradable { get; set; }
    }

    public class Subscription
    {
        public string id { get; set; }
        public string cameraId { get; set; }
        public string paymentStatus { get; set; }
        public bool isActive { get; set; }
        public Plan plan { get; set; }
        public string currency { get; set; }
        public string paymentFrequency { get; set; }
        public bool isFree { get; set; }
        public DateTime startDateBillingCycle { get; set; }
        public DateTime endDateBillingCycle { get; set; }
        public DateTime monthEndBillingCycle { get; set; }
        public int photoCount { get; set; }
        public bool isAutoRenew { get; set; }
    }
    public class CameraInfo
    {
        public string id { get; set; }
        public DateTime activationDate { get; set; } 
        public string user { get; set; }
        public Status status { get; set; }
        public Config config { get; set; }
        public DateTime hdSince { get; set; } 
        public bool isCellular { get; set; }
        public DateTime lastPhotoDate { get; set; }
        public List<Subscription> subscriptions { get; set; }
        public object dataMatrixKey { get; set; } 
        public bool ManualDisable { get; set; }


        public override string ToString()
        {
            return id;
        }
    }
}
