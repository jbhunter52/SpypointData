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

namespace SpyPointData
{
    [Serializable]
    public class DataCollection
    {
        public List<SPConnection> Connections;
        public delegate void ProgressUpdate(string s);
        public event ProgressUpdate OnProgressUpdate;
        public BuckData BuckData;
        public DataCollection()
        {
            Connections = new List<SPConnection>();
            
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
        }
        public void Save(string file)
        {
            string json = JsonConvert.SerializeObject(this);


            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.Write(json);
            }
        }
        public TreeNode[] GetNodes(FilterCriteria fc, OrganizeMethod method)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (SPConnection connection in Connections)
            {
                nodes.Add(connection.GetNodes(fc, method));
            }
            return nodes.ToArray();
        }
        public Photo FindPhoto(string tag)
        {
            foreach (SPConnection connection in Connections)
            {
                Photo p = connection.FindPhoto(tag);
                if (p != null)
                    return p;
            }
            return null;
        }
        public System.Drawing.Image GetPhotoFromFile(Photo p)
        {
            if (p.CardPicFilename != null)
            {
                return System.Drawing.Image.FromFile(p.CardPicFilename);
            }
            if (p.FileName != null)
            {
                return System.Drawing.Image.FromFile(p.FileName);
            }
            else
                return null;
        }
        public void AddCardPic(string filename)
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
                            foreach (Photo p in pics.photos)
                            {
                                if (p.originDate.Equals(date))
                                {
                                    string newName = Path.Combine(Path.GetDirectoryName(p.FileName), Path.GetFileNameWithoutExtension(p.FileName) + "_camPic.jpg");
                                    if (!File.Exists(newName))
                                    {
                                        File.Copy(filename, newName);
                                        p.HaveCardPic = true;
                                        p.CardPicFilename = newName;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public Chart Histogram(TreeNode[] nodes, int numBins, Chart c, HistogramType htype)
        {
            //List of picture nodes is nodes
            List<Photo> photos = new List<Photo>();
            List<double> data = new List<double>();
            foreach (var n in nodes)
            {
                Photo p = FindPhoto((string)n.Tag);
                //photos.Add(p);

                if (htype == HistogramType.Date)
                    data.Add(p.originDate.ToOADate());
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
                    DateTime dt = DateTime.FromOADate(intervals[i]);
                    DataPoint dp = new DataPoint();
                    dp.SetValueXY(dt, binCount[i]);
                    dp.ToolTip = dt.ToShortDateString();
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
