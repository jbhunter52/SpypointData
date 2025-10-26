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
using NLog;

namespace SpyPointSettings
{
    public partial class SpyPointSettingsForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Timer timer;
        private Timer mediumTimer;
        private Timer fastTimer;
        private DataCollection Data;
        private string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpyPoint", "Data.json");
        private string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpyPoint", "SpyPointSettings.json");
        private SPSSettings Settings;
        bool MorningChange = false;
        bool EveningChange = false;
        List<string> updateUsernames = new List<string>() { "jbhunter52@yahoo.com" };

        SunsetSunrise CurrentSunsetSunrise;

        DateTime PrevDateTime;

        public SpyPointSettingsForm()
        {
            InitializeComponent();


            // Create a file target and set the filename
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log.txt" };

            // Create a rule to map loggers to the target
            // This rule sends all logs at the Debug level and higher to the file target
            var config = new NLog.Config.LoggingConfiguration();
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply the configuration
            LogManager.Configuration = config;
            Log("Application started");
            Settings = new SPSSettings(settingsFile);


            comboBoxDayDelay.SelectedIndexChanged -= comboBoxDayDelay_SelectedIndexChanged;
            comboBoxDayNumShots.SelectedIndexChanged -= comboBoxDayNumShots_SelectedIndexChanged;
            comboBoxNightDelay.SelectedIndexChanged -= comboBoxNightDelay_SelectedIndexChanged;
            comboBoxNightNumShots.SelectedIndexChanged -= comboBoxNightNumShots_SelectedIndexChanged;
            comboBoxOptionsCamType.SelectedIndexChanged -= ComboBoxOptionsCamType_SelectedIndexChanged;

            foreach (string s in Enum.GetNames(typeof(delay_micro)).ToArray()) comboBoxDayDelay.Items.Add(s);
            foreach (string s in Enum.GetNames(typeof(multishot_micro)).ToArray()) comboBoxDayNumShots.Items.Add(s);
            foreach (string s in Enum.GetNames(typeof(delay_micro)).ToArray()) comboBoxNightDelay.Items.Add(s);
            foreach (string s in Enum.GetNames(typeof(multishot_micro)).ToArray()) comboBoxNightNumShots.Items.Add(s);
            foreach (string s in Enum.GetNames(typeof(CamType)).ToArray()) comboBoxOptionsCamType.Items.Add(s);

            //comboBoxOptionsCamType.SelectedItem = CamType.Micro.ToString();

            comboBoxDayDelay.SelectedIndexChanged += comboBoxDayDelay_SelectedIndexChanged;
            comboBoxDayNumShots.SelectedIndexChanged += comboBoxDayNumShots_SelectedIndexChanged;
            comboBoxNightDelay.SelectedIndexChanged += comboBoxNightDelay_SelectedIndexChanged;
            comboBoxNightNumShots.SelectedIndexChanged += comboBoxNightNumShots_SelectedIndexChanged;
            comboBoxOptionsCamType.SelectedIndexChanged += ComboBoxOptionsCamType_SelectedIndexChanged;

            comboBoxOptionsCamType.SelectedItem = CamType.Micro.ToString();

            Data = new DataCollection();


            if (File.Exists(file))
            {
                Data.Load(file);
            }


        }

        private void ComboBoxOptionsCamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string model = ((ComboBox)sender).SelectedItem.ToString();

            comboBoxDayDelay.SelectedIndexChanged -= comboBoxDayDelay_SelectedIndexChanged;
            comboBoxDayNumShots.SelectedIndexChanged -= comboBoxDayNumShots_SelectedIndexChanged;
            comboBoxNightDelay.SelectedIndexChanged -= comboBoxNightDelay_SelectedIndexChanged;
            comboBoxNightNumShots.SelectedIndexChanged -= comboBoxNightNumShots_SelectedIndexChanged;

            comboBoxDayDelay.SelectedItem = Settings.GetDelay(model, DayNight.Day).ToString();
            comboBoxDayNumShots.SelectedItem = Settings.GetMultiShot(model, DayNight.Day).ToString();
            comboBoxNightDelay.SelectedItem = Settings.GetDelay(model, DayNight.Night).ToString();
            comboBoxNightNumShots.SelectedItem = Settings.GetMultiShot(model, DayNight.Night).ToString();

            comboBoxDayDelay.SelectedIndexChanged += comboBoxDayDelay_SelectedIndexChanged;
            comboBoxDayNumShots.SelectedIndexChanged += comboBoxDayNumShots_SelectedIndexChanged;
            comboBoxNightDelay.SelectedIndexChanged += comboBoxNightDelay_SelectedIndexChanged;
            comboBoxNightNumShots.SelectedIndexChanged += comboBoxNightNumShots_SelectedIndexChanged;
        }

        private void MediumTimer_Tick(object sender, EventArgs e)
        {
            RefreshCameraInfo();
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
                bool result = RefreshSunriseSunset(minOffset);
                if (result)
                    return;
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
                if (updateUsernames.Contains(conn.UserLogin.Username))
                {
                    conn.Login();
                    foreach (KeyValuePair<string, CameraInfo> kvp in conn.CameraInfoList)
                    {
                        CameraInfo ci = kvp.Value;
                        if (ci.ManualDisable)
                            continue;

                        try
                        {
                            conn.SetCameraSettings(ci, DayNight.Day, Settings);
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Error, String.Format("Exception with changing settings on user:{0} name:{1}", ci.user, ci.config.name));
                            logger.Log(LogLevel.Error, ex.Message);
                        }
                        var csettings = Settings.GetSettings(ci.status.model);
                        Log(String.Format("TriggerMorningChange() ended {0}, Delay={1}, MultiShot={2}",
                            ci.config.name, Settings.GetDelay(ci.status.model, DayNight.Day).ToString(),
                            Settings.GetMultiShot(ci.status.model, DayNight.Day).ToString()));
                    }
                }
            }
            RefreshCameraInfo();
        }
        public void TriggerEveningChange()
        {
            EveningChange = true;

            foreach (SPConnection conn in Data.Connections)
            {
                if (updateUsernames.Contains(conn.UserLogin.Username))
                {
                    conn.Login();
                    foreach (KeyValuePair<string,CameraInfo> kvp in conn.CameraInfoList)
                    {
                        CameraInfo ci = kvp.Value;
                        if (ci.ManualDisable)
                            continue;
                        try
                        {
                            conn.SetCameraSettings(ci, DayNight.Night, Settings);
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Error, String.Format("Exception with changing settings on user:{0} name:{1}", ci.user, ci.config.name));
                            logger.Log(LogLevel.Error, ex.Message);
                        }
                        Log(String.Format("TriggerEveningChange() ended {0}, Delay={1}, MultiShot={2}",
                            ci.config.name, Settings.GetDelay(ci.status.model, DayNight.Night).ToString(),
                            Settings.GetMultiShot(ci.status.model, DayNight.Night).ToString()));
                    }
                }
            }
            RefreshCameraInfo();
        }
        public bool RefreshSunriseSunset(int minOffset)
        {
            try
            {
                var s = new SunsetSunrise(minOffset);

                textBoxSunrise.Text = s.Sunrise.ToString(s.format);
                textBoxSunset.Text = s.Sunset.ToString(s.format);

                textBoxAfterSunrise.Text = s.AfterSunrise.ToString(s.format);
                textBoxBeforeSunset.Text = s.BeforeSunset.ToString(s.format);

                textBoxShootTimeMorn.Text = s.ShootTimeMorn.ToString(s.format);
                textBoxShootTimeEve.Text = s.ShootTimeEve.ToString(s.format);
                CurrentSunsetSunrise = s;

                Log("New Date");
                Log("Sunrise, " + textBoxSunrise.Text);
                Log("AfterSunrise, " + textBoxAfterSunrise.Text);
                Log("ShootTimeMorning, " + textBoxShootTimeMorn.Text);
                Log("Sunset, " + textBoxSunset.Text);
                Log("BeforeSunset, " + textBoxBeforeSunset.Text);
                Log("ShootTimeEvening, " + textBoxShootTimeEve.Text);
            }
            catch(Exception ex)
            {
                Log("Error getting sunrise/setset times");
                Log(ex.Message);
                return false;
            }
            return true;
        }
        public void RefreshCameraInfo()
        {
            foreach (var spc in Data.Connections)
            {
                //SPConnection SP = Data.Add(spc.UserLogin);
                //SP.Login();

                //SP.GetCameraInfo();
                try
                {
                    spc.Login();
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Error, String.Format("Exception logging into user:{0}",spc.UserLogin.Username));
                    logger.Log(LogLevel.Error, ex.Message);
                    logger.Log(LogLevel.Error, ex.InnerException.Message);
                }
                try
                {
                    spc.GetCameraInfo();
                }
                catch (Exception ex)
                {
                    string mess = String.Format(ex.Message + ", user:" + spc.UserLogin.Username);
                    Log(mess);
                    logger.Log(LogLevel.Error, mess);
                    logger.Log(LogLevel.Error, ex.Message);
                    logger.Log(LogLevel.Error, ex.InnerException.Message);
                }
                try
                {
                    spc.UpdateFirstPics();
                }
                catch (Exception ex)
                {
                    string mess = String.Format(ex.Message + ", user:" + spc.UserLogin.Username);
                    Log(mess);
                    logger.Log(LogLevel.Error, mess);
                    logger.Log(LogLevel.Error, ex.Message);
                    logger.Log(LogLevel.Error, ex.InnerException.Message);
                }

            }
            RefreshTables();
            Log("RefreshCameraInfo");
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

        private void Log(string mess)
        {
            logger.Debug(mess);

            string dt = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            string log = String.Format("{0}, {1}\n", dt, mess);
            richTextBoxLog.AppendText(log);
            richTextBoxLog.ScrollToCaret();
        }

        private void SpyPointSettingsForm_Shown(object sender, EventArgs e)
        {
            RefreshCameraInfo();

            int minOffset = int.Parse(textBoxMinOffset.Text);
            bool result = RefreshSunriseSunset(minOffset);


            PrevDateTime = DateTime.Now;
            if (PrevDateTime > CurrentSunsetSunrise.AfterSunrise && PrevDateTime < CurrentSunsetSunrise.BeforeSunset)
            {
                TriggerMorningChange();
            }
            else
            {
                MorningChange = true;
                TriggerEveningChange();
            }

            timer = new Timer();
            timer.Interval = 1000 * 60; //every minute
            timer.Tick += Timer_Tick;
            timer.Start();

            mediumTimer = new Timer();
            mediumTimer.Interval = 1000 * 60 * 30; //every 30 minutes
            mediumTimer.Tick += MediumTimer_Tick;
            mediumTimer.Start();

            fastTimer = new Timer();
            fastTimer.Interval = 1000; //Every 1000 ms
            fastTimer.Tick += FastTimer_Tick;
            fastTimer.Start();
            Log("Initialize Complete");
        }

        private void comboBoxDayDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var delay = (delay_micro)Enum.Parse(typeof(delay_micro), comboBoxDayDelay.SelectedItem.ToString());
            var daynight = DayNight.Day;
            var cam = (CamType)Enum.Parse(typeof(CamType), comboBoxOptionsCamType.SelectedItem.ToString());
            Settings.SetDelay(cam.ToString(), daynight, delay);
            Settings.Save(settingsFile);
        }

        private void comboBoxDayNumShots_SelectedIndexChanged(object sender, EventArgs e)
        {
            var multishot = (multishot_micro)Enum.Parse(typeof(multishot_micro), comboBoxDayNumShots.SelectedItem.ToString());
            var daynight = DayNight.Day;
            var cam = (CamType)Enum.Parse(typeof(CamType), comboBoxOptionsCamType.SelectedItem.ToString());
            Settings.SetMultishot(cam.ToString(), daynight, multishot);
            Settings.Save(settingsFile);
        }

        private void comboBoxNightDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var delay = (delay_micro)Enum.Parse(typeof(delay_micro), comboBoxNightDelay.SelectedItem.ToString());
            var daynight = DayNight.Night;
            var cam = (CamType)Enum.Parse(typeof(CamType), comboBoxOptionsCamType.SelectedItem.ToString());
            Settings.SetDelay(cam.ToString(), daynight, delay);
            Settings.Save(settingsFile);
        }

        private void comboBoxNightNumShots_SelectedIndexChanged(object sender, EventArgs e)
        {
            var multishot = (multishot_micro)Enum.Parse(typeof(multishot_micro), comboBoxNightNumShots.SelectedItem.ToString());
            var daynight = DayNight.Night;
            var cam = (CamType)Enum.Parse(typeof(CamType), comboBoxOptionsCamType.SelectedItem.ToString());
            Settings.SetMultishot(cam.ToString(), daynight, multishot);
            Settings.Save(settingsFile);
        }

        private void refreshDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            string backup = @"\\Amd\d\Spypoint\Data.json";

            if (File.Exists(backup))
            {
                var backup_info = new FileInfo(backup);
                var current = new FileInfo(file);

                if (backup_info.LastWriteTime > current.LastWriteTime)
                {
                    File.Copy(backup, file, true);

                    if (File.Exists(file))
                    {
                        Data.Load(file);
                    }
                }
            }
            RefreshCameraInfo();
        }
    }

    public enum CamType
    {
        Micro,
        Flex,
        LM2
    }
}
