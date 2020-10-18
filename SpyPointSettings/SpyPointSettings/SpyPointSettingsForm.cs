using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using SpyPointData;
using System.Globalization;

namespace SpyPointSettings
{
    public partial class SpyPointSettingsForm : Form
    {
        private Timer timer;
        private Timer fastTimer;
        private DataCollection Data;
        private string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpyPoint", "Data.json");

        bool MorningChange = false;
        bool EveningChange = false;

        SunsetSunrise CurrentSunsetSunrise;

        DateTime PrevDateTime;

        public SpyPointSettingsForm()
        {
            InitializeComponent();

            Data = new DataCollection();

            if (File.Exists(file))
            {
                Data.Load(file);
            }

            RefreshCameraInfo();

            int minOffset = int.Parse(textBoxMinOffset.Text);
            RefreshSunriseSunset(minOffset);


            PrevDateTime = DateTime.Now;
            if (PrevDateTime > CurrentSunsetSunrise.AfterSunrise && PrevDateTime < CurrentSunsetSunrise.BeforeSunset)
            {
                TriggerMorningChange();
            }
            else
                TriggerEveningChange();

            timer = new Timer();
            timer.Interval = 1000 * 60; //every minute
            timer.Tick += Timer_Tick;
            timer.Start();

            fastTimer = new Timer();
            fastTimer.Interval = 100;
            fastTimer.Tick += FastTimer_Tick;
            fastTimer.Start();
        }

        private void FastTimer_Tick(object sender, EventArgs e)
        {
            ColorPicturebox(pictureBoxMorningStatus, MorningChange);
            ColorPicturebox(pictureBoxEveningStatus, EveningChange);
        }
        private void ColorPicturebox(PictureBox pb, bool status)
        {
            Color col;
            if (status == true)
                col = Color.Green;
            else
                col = Color.Red;

            pb.BackColor = col;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {

            if (!PrevDateTime.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
            {
                //New Date
                MorningChange = false;
                EveningChange = false;
                PrevDateTime = DateTime.Now;
                int minOffset = int.Parse(textBoxMinOffset.Text);
                RefreshSunriseSunset(minOffset);
            }

            if (DateTime.Now > CurrentSunsetSunrise.AfterSunrise && MorningChange == false)
            {
                //Morning setting trigger
                TriggerMorningChange();
            }
            if (DateTime.Now > CurrentSunsetSunrise.BeforeSunset && EveningChange == false)
            {
                //Morning setting trigger
                TriggerEveningChange();
            }
        }

        public void TriggerMorningChange()
        {
            MorningChange = true;

            foreach (SPConnection conn in Data.Connections)
            {
                if (conn.UserLogin.Username == "jbhunter52@yahoo.com")
                {
                    conn.Login();
                    foreach (KeyValuePair<string, CameraInfo> kvp in conn.CameraInfoList)
                    {
                        CameraInfo ci = kvp.Value;
                        conn.SetCameraSettings(ci, new DelayOptions(delay._15min), new MutiShotOptions(multishot._1));
                    }
                }
            }
        }
        public void TriggerEveningChange()
        {
            EveningChange = true;

            foreach (SPConnection conn in Data.Connections)
            {
                if (conn.UserLogin.Username == "jbhunter52@yahoo.com" || conn.UserLogin.Username == "M17BEHRMAN@GMAIL.COM")
                {
                    conn.Login();
                    foreach (KeyValuePair<string,CameraInfo> kvp in conn.CameraInfoList)
                    {
                        CameraInfo ci = kvp.Value;
                        conn.SetCameraSettings(ci, new DelayOptions(delay._10s), new MutiShotOptions(multishot._2));
                    }
                }
            }
        }
        public void RefreshSunriseSunset(int minOffset)
        {
            var s = new SunsetSunrise(minOffset);

            textBoxSunrise.Text = s.Sunrise.ToString(s.format);
            textBoxSunset.Text = s.Sunset.ToString(s.format);

            textBoxAfterSunrise.Text = s.AfterSunrise.ToString(s.format);
            textBoxBeforeSunset.Text = s.BeforeSunset.ToString(s.format);

            textBoxShootTimeMorn.Text = s.ShootTimeMorn.ToString(s.format);
            textBoxShootTimeEve.Text = s.ShootTimeEve.ToString(s.format);
            CurrentSunsetSunrise = s;
        }
        public void RefreshCameraInfo()
        {
            foreach (var spc in Data.Connections)
            {
                //SPConnection SP = Data.Add(spc.UserLogin);
                //SP.Login();

                //SP.GetCameraInfo();
                spc.Login();
                spc.GetCameraInfo();
            }
            RefreshTables();
        }
        public void RefreshTables()
        {
            CameraDetailsForm cdf = new CameraDetailsForm(Data);
            DataTable dt = cdf.GetDataTable();
            dataListView.DataSource = dt;
        }

        private void buttonMorningTrigger_Click(object sender, EventArgs e)
        {
            TriggerMorningChange();
        }

        private void buttonEveningTrigger_Click(object sender, EventArgs e)
        {
            TriggerEveningChange();
        }

        private void buttonRefreshCameraDisplay_Click(object sender, EventArgs e)
        {
            RefreshCameraInfo();
        }
    }
}
