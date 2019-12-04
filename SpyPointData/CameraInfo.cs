using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyPointData
{
    public class Signal
    {
        public int bar { get; set; }
        public int dBm { get; set; }
        public string type { get; set; }
        public int mcc { get; set; }
        public int mnc { get; set; }
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

    public class CameraInfo
    {
        public string id { get; set; }
        public string user { get; set; }
        public Status status { get; set; }
        public Config config { get; set; }
        public bool isCellular { get; set; }
        public object lastPhoto { get; set; }
    }
}
