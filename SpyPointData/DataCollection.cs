using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ExifLib;
using Cyotek.Windows.Forms;

namespace SpyPointData
{
    [Serializable]
    public class DataCollection
    {
        public List<SPConnection> Connections;
        public ManualPics ManualPics;
        public delegate void ProgressUpdate(string s);
        public event ProgressUpdate OnProgressUpdate;
        public BuckData BuckData;
        public FilterCriteria Filter;
        public string Dir;
        public DataCollection()
        {
            Connections = new List<SPConnection>();
            ManualPics = new ManualPics();
            Filter = new FilterCriteria();
            
        }

        public void FixErrors()
        {
            //Fix all null Photo ids
            foreach (var conn in Connections)
            {
                foreach (var cp in conn.CameraPictures)
                {
                    foreach (var p in cp.Value.photos)
                    {
                        if (p.id != null)
                            continue;

                        string dir = "";
                        if (p.HaveCardPic)
                        {
                            dir = Path.GetDirectoryName(p.CardPicFilename);
                            string randomSeq = Guid.NewGuid().ToString();
                            string newName = Path.Combine(dir, randomSeq + ".jpg");
                            File.Copy(p.CardPicFilename, newName);
                            File.Delete(p.CardPicFilename);
                            p.CardPicFilename = newName;
                            p.FileName = newName;
                            p.id = randomSeq;
                        }
                    }
                }
            }
        }
        public void ClearNew()
        {
            foreach (var conn in Connections)
            {
                foreach (var cp in conn.CameraPictures)
                {
                    foreach (var p in cp.Value.photos)
                    {
                        p.New = false;
                    }
                }
            }
        }
        public CameraInfo FindCameraId(string id)
        {
            foreach (var conn in Connections)
            {
                foreach (var ci in conn.CameraInfoList)
                {
                    if (ci.Key.Equals(id))
                        return (CameraInfo)ci.Value;
                }
            }
            return null;
        }
        public List<Photo> GetFilteredPhotos()
        {
            List<Photo> photos = new List<Photo>();
            foreach (var conn in Connections)
            {
                photos.AddRange(conn.GetFilteredPhotos(Filter, BuckData));
            }
            return photos;
        }
        public void RegisterEvents()
        {
            foreach (var connection in Connections)
            {
                connection.OnProgressUpdate += connection_OnProgressUpdate;
            }
        }
        public SPConnection Add(LoginInfo login)
        {
            SPConnection connection = new SPConnection(login);
            connection.OnProgressUpdate += connection_OnProgressUpdate;
            Connections.Add(connection);
            return connection;
        }

        void connection_OnProgressUpdate(string s)
        {
            if (OnProgressUpdate != null)
                OnProgressUpdate(s);
        }
        public void Load(string file)
        {
            string json = File.ReadAllText(file);
            DataCollection data = JsonConvert.DeserializeObject<DataCollection>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            this.Connections = data.Connections;
            this.BuckData = data.BuckData;
            this.ManualPics = data.ManualPics;
            this.Dir = Path.GetDirectoryName(file);
        }
        public void Save(string file)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.Write(json);
            }
        }

        public Photo FindPhoto(string tag)
        {
            Photo p;
            foreach (SPConnection connection in Connections)
            {
                p = connection.FindPhoto(tag);
                if (p != null)
                    return p;
            }

            p = ManualPics.FindPhoto(tag);
            if (p != null)
                return p;

            return null;
        }
        public string GetFileSize(string file)
        {
            FileInfo FileVol = new FileInfo(file);

            if (FileVol.Exists == false)
                return "0";

            string fileLength = FileVol.Length.ToString();
            string length = string.Empty;
            if (FileVol.Length >= (1 << 10))
              length= string.Format("{0} Kb", FileVol.Length >> 10);
            return length;
        }
        public void DeletePic(Photo p)
        {
            List<Photo> list = new List<Photo> { p };
            DeletePics(list);
        }
        public void DeletePics(List<Photo> list)
        {
            foreach (var p in list)
            {
                foreach (var conn in Connections)
                {
                    foreach (var kvp in conn.CameraPictures)
                    {
                        CameraPics pics = kvp.Value;

                        if (pics.photos.Contains(p))
                        {
                            if (File.Exists(p.CardPicFilename))
                                File.Delete(p.CardPicFilename);
                            if (File.Exists(p.FileName))
                                File.Delete(p.FileName);
                            pics.photos.Remove(p);
                        }
                    }
                }
            }
        }
        public void AddCardPic(string filename, string camid)
        {
            using (ExifReader reader = new ExifReader(filename))
            {
                DateTime date;
                if (reader.GetTagValue<DateTime>(ExifTags.DateTimeOriginal, out date))
                {
                    //string dateString = date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

                    foreach (SPConnection account in Connections)
                    {
                        foreach (var kvp in account.CameraPictures)
                        {
                            CameraPics pics = kvp.Value;
                            if (!pics.cameraId.Equals(camid))
                                continue;

                            bool added = false;
                            foreach (Photo p in pics.photos)
                            {
                                if (p.originDate.Equals(date))
                                {
                                    if (p.FileName == null)
                                        p.FileName = p.CardPicFilename;
                                    string newName = Path.Combine(Path.GetDirectoryName(p.FileName), Path.GetFileNameWithoutExtension(p.FileName) + "_camPic.jpg");
                                    added = true;
                                    if (File.Exists(newName))
                                        File.Delete(newName);

                                    if (!File.Exists(newName))
                                    {
                                        File.Copy(filename, newName);
                                        p.HaveCardPic = true;
                                        p.CardPicFilename = newName;
                                    }
                                }
                            }
                            if (added == false)
                            {
                                //Add new Photo object for photo
                                Photo p = new Photo();
                                string dir = Path.Combine(this.Dir, camid);
                                string randomSeq = Guid.NewGuid().ToString();
                                string newName = Path.Combine(dir, randomSeq + "_camPic.jpg");
                                File.Copy(filename, newName);
                                p.Latitude = pics.Latitude;
                                p.Longitude = pics.Longitude;
                                CameraInfo ci = this.FindCameraId(camid);
                                p.CameraName = ci.config.name;
                                p.id = randomSeq;
                                p.HaveCardPic = true;
                                p.originDate = (new FileInfo(filename)).LastWriteTime;
                                p.CardPicFilename = newName;
                                p.FileName = newName;
                                pics.photos.Add(p);
                            }
                        }
                    }
                }
            }
        }
        public Chart Histogram(List<Photo> photos, int numBins, Chart c, HistogramType htype)
        {
            //List of picture nodes is nodes
            List<double> data = new List<double>();
            foreach (Photo p in photos)
            {
                if (htype == HistogramType.Date)
                {
                    data.Add(p.originDate.ToOADate());
                }
                else if (htype == HistogramType.Time)
                {

                    DateTime dt = p.originDate;

                    DateTime time = new DateTime(2020, 1, 1, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
                    data.Add(time.ToOADate());
                }
            }
            data.Sort();

            if (data.Count == 0)
                return c;

            if (htype == HistogramType.Time)
            {
                double initial = Math.Floor(data[0]);
                for (int i = 0; i < data.Count; i++)
                {
                    data[i] = data[i] - initial;
                    data[i] = data[i] * 24;
                }
            }

            //Make Bins
            double[] intervals = MakeIntervals(data.ToArray(), numBins, htype);
            int[] binCount = new int[numBins];
            //Binning

            for (int i = 0; i < data.Count; ++i)
            {
                double x = data[i];
                int bin = Bin(x, intervals);
                binCount[bin]++;
            }

            //Chart c = new Chart();
            c.ChartAreas.Clear();
            c.Series.Clear();
            var ca = new ChartArea();
            if (htype == HistogramType.Date)
            {
                ca.AxisX.LabelStyle.Format = "MM/dd/yyyy";
                ca.AxisX.IntervalType = DateTimeIntervalType.Weeks;
            }

            if (htype == HistogramType.Time)
            {
                ca.AxisX.IntervalType = DateTimeIntervalType.Number;
                ca.AxisX.Interval = 1;
                ca.AxisX.MajorGrid.Interval = 1;
                ca.AxisX.Minimum = 0;
                ca.AxisX.Maximum = 24;
            }
            ca.AxisX.LabelStyle.Angle = -90;

            c.ChartAreas.Add(ca);
            var s = new Series();
            if (htype == HistogramType.Date)
                s.XValueType = ChartValueType.DateTime;
            if (htype == HistogramType.Time)
                s.XValueType = ChartValueType.Single;
            s.ChartType = SeriesChartType.Column;
            for (int i = 0; i < binCount.Length; i++)
            {

                if (htype == HistogramType.Date)
                {
                    int ind = i * 2;
                    double x = (intervals[ind + 1] + intervals[ind]) / 2;
                    DateTime x_dt= DateTime.FromOADate(x);
                    DataPoint dp = new DataPoint();
                    var y = binCount[i];
                    dp.SetValueXY(x_dt, y);
                    dp.ToolTip = x_dt.ToShortDateString();
                    s.Points.Add(dp);
                }
                if (htype == HistogramType.Time)
                {
                    int ind = i * 2;
                    double x = (intervals[ind + 1] + intervals[ind]) / 2;
                    DataPoint dp = new DataPoint();
                    dp.SetValueXY(x, binCount[i]);
                    dp.ToolTip = x.ToString("#.#");
                    s.Points.Add(dp);
                }
            }

            c.Series.Add(s);
            return c;
        }
        static int Bin(double x, double[] intervals)
        {
            for (int i = 0; i < intervals.Length - 1; i += 2)
            {
                if (x >= intervals[i] && x < intervals[i + 1])
                    return i / 2;
            }
            return -1; // error
        }

        static double[] MakeIntervals(double[] data, int numBins, HistogramType hType)
        {
            double min;
            double max;
            double width;
            if (hType == HistogramType.Date)
            {
                max = data[0]; // find min & max
                min = data[0];
                for (int i = 0; i < data.Length; ++i)
                {
                    if (data[i] < min) min = data[i];
                    if (data[i] > max) max = data[i];
                }
                width = (max - min) / numBins; // compute width
            }
            else // HistogramType.Time
            {
                min = 0;
                max = 24;

                width = (max - min) / numBins; // compute width
            }

            double[] intervals = new double[numBins * 2]; // intervals
            intervals[0] = min;
            intervals[1] = min + width;
            for (int i = 2; i < intervals.Length - 1; i += 2)
            {
                intervals[i] = intervals[i - 1];
                intervals[i + 1] = intervals[i] + width;
            }
            intervals[0] -= .00001; // outliers
            intervals[intervals.Length - 1] += .00001;

            return intervals;
        }


        public string Debug()
        {
            string result = "";

            result += "Parsing Connections";

            result += String.Format("\n\nFound {0} connections\n", this.Connections.Count);
            int i = 0;
            foreach (var conn in this.Connections)
            {
                result += String.Format("\nConnection {0}, {1}",i, conn.uuid);
                result += conn.ToString();
                i++;
            }


            return result;
        }
    }

    public enum HistogramType
    {
        Date,
        Time
    };
    public enum OrganizeMethod
    {
        Camera,
        Location
    };
}
