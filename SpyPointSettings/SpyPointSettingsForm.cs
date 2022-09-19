﻿using System;
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

        SunsetSunrise CurrentSunsetSunrise;

        DateTime PrevDateTime;

        public SpyPointSettingsForm()
        {
            InitializeComponent();
            Log("Application started");
            Settings = new SPSSettings();
            Settings.Load(settingsFile);

            comboBoxDayDelay.SelectedIndexChanged -= comboBoxDayDelay_SelectedIndexChanged;
            comboBoxDayNumShots.SelectedIndexChanged -= comboBoxDayNumShots_SelectedIndexChanged;
            comboBoxNightDelay.SelectedIndexChanged -= comboBoxNightDelay_SelectedIndexChanged;
            comboBoxNightNumShots.SelectedIndexChanged -= comboBoxNightNumShots_SelectedIndexChanged;

            comboBoxDayDelay.DataSource = Enum.GetNames(typeof(delay));
            comboBoxDayNumShots.DataSource = Enum.GetNames(typeof(multishot));
            comboBoxNightDelay.DataSource = Enum.GetNames(typeof(delay));
            comboBoxNightNumShots.DataSource = Enum.GetNames(typeof(multishot));

            comboBoxDayDelay.SelectedItem = Settings.DayDelay;
            comboBoxDayNumShots.SelectedItem = Settings.DayMultiShot;
            comboBoxNightDelay.SelectedItem = Settings.NightDelay;
            comboBoxNightNumShots.SelectedItem = Settings.NightMultiShot;

            comboBoxDayDelay.SelectedIndexChanged += comboBoxDayDelay_SelectedIndexChanged;
            comboBoxDayNumShots.SelectedIndexChanged += comboBoxDayNumShots_SelectedIndexChanged;
            comboBoxNightDelay.SelectedIndexChanged += comboBoxNightDelay_SelectedIndexChanged;
            comboBoxNightNumShots.SelectedIndexChanged += comboBoxNightNumShots_SelectedIndexChanged;

            Data = new DataCollection();


            if (File.Exists(file))
            {
                Data.Load(file);
            }


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
                if (conn.UserLogin.Username == "jbhunter52@yahoo.com" || conn.UserLogin.Username == "M17BEHRMAN@GMAIL.COM")
                {
                    conn.Login();
                    foreach (KeyValuePair<string, CameraInfo> kvp in conn.CameraInfoList)
                    {
                        CameraInfo ci = kvp.Value;
                        if (ci.ManualDisable)
                            continue;
                        Log("TriggerMorningChange(), started " + ci.config.name);
                        try
                        {
                            conn.SetCameraSettings(ci, new DelayOptions(Settings.DayDelay), new MutiShotOptions(Settings.NightMultiShot));
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Error, String.Format("Exception with changing settings on user:{0} name:{1}", ci.user, ci.config.name));
                            logger.Log(LogLevel.Error, ex.Message);
                        }
                        Log("TriggerMorningChange(), ended " + ci.config.name);
                    }
                }
            }
            Log("TriggerMorningChange");
            RefreshCameraInfo();
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
                        if (ci.ManualDisable)
                            continue;
                        Log("TriggerEveningChange(), started " + ci.config.name);
                        try
                        {
                            conn.SetCameraSettings(ci, new DelayOptions(Settings.NightDelay), new MutiShotOptions(Settings.NightMultiShot));
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Error, String.Format("Exception with changing settings on user:{0} name:{1}", ci.user, ci.config.name));
                            logger.Log(LogLevel.Error, ex.Message);
                        }
                        Log("TriggerEveningChange(), ended " + ci.config.name);
                    }
                }
            }
            Log("TriggerEveningChange");
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
                    logger.Log(LogLevel.Error, String.Format("Exception getting camera info on user:{0}", spc.UserLogin.Username));
                    logger.Log(LogLevel.Error, ex.Message);
                    logger.Log(LogLevel.Error, ex.InnerException.Message);
                }
                try
                {
                    spc.UpdateFirstPics();
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Error, String.Format("Exception updating pics on user:{0}", spc.UserLogin.Username));
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
            mediumTimer.Interval = 1000 * 60 * 30; //every 5 minutes
            mediumTimer.Tick += MediumTimer_Tick;
            mediumTimer.Start();

            fastTimer = new Timer();
            fastTimer.Interval = 100;
            fastTimer.Tick += FastTimer_Tick;
            fastTimer.Start();
            Log("Initialize Complete");
        }

        private void comboBoxDayDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.DayDelay = (delay)Enum.Parse(typeof(delay), comboBoxDayDelay.SelectedValue.ToString());
            Settings.Save(settingsFile);
        }

        private void comboBoxDayNumShots_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.DayMultiShot = (multishot)Enum.Parse(typeof(multishot), comboBoxDayNumShots.SelectedValue.ToString());
            Settings.Save(settingsFile);
        }

        private void comboBoxNightDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.NightDelay = (delay)Enum.Parse(typeof(delay), comboBoxNightDelay.SelectedValue.ToString());
            Settings.Save(settingsFile);
        }

        private void comboBoxNightNumShots_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.NightMultiShot = (multishot)Enum.Parse(typeof(multishot), comboBoxNightNumShots.SelectedValue.ToString());
            Settings.Save(settingsFile);
        }
    }
}
