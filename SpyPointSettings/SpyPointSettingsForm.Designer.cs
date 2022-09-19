namespace SpyPointSettings
{
    partial class SpyPointSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataListView = new BrightIdeasSoftware.DataListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonRefreshCameraDisplay = new System.Windows.Forms.Button();
            this.pictureBoxEveningStatus = new System.Windows.Forms.PictureBox();
            this.pictureBoxMorningStatus = new System.Windows.Forms.PictureBox();
            this.buttonEveningTrigger = new System.Windows.Forms.Button();
            this.buttonMorningTrigger = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxMinOffset = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxShootTimeEve = new System.Windows.Forms.TextBox();
            this.textBoxShootTimeMorn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxBeforeSunset = new System.Windows.Forms.TextBox();
            this.textBoxAfterSunrise = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSunset = new System.Windows.Forms.TextBox();
            this.textBoxSunrise = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.comboBoxDayDelay = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxDayNumShots = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxNightNumShots = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxNightDelay = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEveningStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMorningStatus)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataListView
            // 
            this.dataListView.CellEditUseWholeCell = false;
            this.dataListView.DataSource = null;
            this.dataListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataListView.HideSelection = false;
            this.dataListView.Location = new System.Drawing.Point(3, 136);
            this.dataListView.Name = "dataListView";
            this.dataListView.Size = new System.Drawing.Size(1277, 427);
            this.dataListView.TabIndex = 0;
            this.dataListView.UseCompatibleStateImageBehavior = false;
            this.dataListView.View = System.Windows.Forms.View.Details;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1283, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // buttonRefreshCameraDisplay
            // 
            this.buttonRefreshCameraDisplay.Location = new System.Drawing.Point(244, 79);
            this.buttonRefreshCameraDisplay.Name = "buttonRefreshCameraDisplay";
            this.buttonRefreshCameraDisplay.Size = new System.Drawing.Size(104, 23);
            this.buttonRefreshCameraDisplay.TabIndex = 19;
            this.buttonRefreshCameraDisplay.Text = "Refresh Cams";
            this.buttonRefreshCameraDisplay.UseVisualStyleBackColor = true;
            this.buttonRefreshCameraDisplay.Click += new System.EventHandler(this.buttonRefreshCameraDisplay_Click);
            // 
            // pictureBoxEveningStatus
            // 
            this.pictureBoxEveningStatus.Location = new System.Drawing.Point(158, 79);
            this.pictureBoxEveningStatus.Name = "pictureBoxEveningStatus";
            this.pictureBoxEveningStatus.Size = new System.Drawing.Size(24, 23);
            this.pictureBoxEveningStatus.TabIndex = 18;
            this.pictureBoxEveningStatus.TabStop = false;
            // 
            // pictureBoxMorningStatus
            // 
            this.pictureBoxMorningStatus.Location = new System.Drawing.Point(60, 79);
            this.pictureBoxMorningStatus.Name = "pictureBoxMorningStatus";
            this.pictureBoxMorningStatus.Size = new System.Drawing.Size(24, 23);
            this.pictureBoxMorningStatus.TabIndex = 17;
            this.pictureBoxMorningStatus.TabStop = false;
            // 
            // buttonEveningTrigger
            // 
            this.buttonEveningTrigger.Location = new System.Drawing.Point(126, 50);
            this.buttonEveningTrigger.Name = "buttonEveningTrigger";
            this.buttonEveningTrigger.Size = new System.Drawing.Size(98, 23);
            this.buttonEveningTrigger.TabIndex = 16;
            this.buttonEveningTrigger.Text = "EveningTrigger";
            this.buttonEveningTrigger.UseVisualStyleBackColor = true;
            this.buttonEveningTrigger.Click += new System.EventHandler(this.buttonEveningTrigger_Click);
            // 
            // buttonMorningTrigger
            // 
            this.buttonMorningTrigger.Location = new System.Drawing.Point(20, 50);
            this.buttonMorningTrigger.Name = "buttonMorningTrigger";
            this.buttonMorningTrigger.Size = new System.Drawing.Size(98, 23);
            this.buttonMorningTrigger.TabIndex = 15;
            this.buttonMorningTrigger.Text = "MorningTrigger";
            this.buttonMorningTrigger.UseVisualStyleBackColor = true;
            this.buttonMorningTrigger.Click += new System.EventHandler(this.buttonMorningTrigger_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Settings Offset (min)";
            // 
            // textBoxMinOffset
            // 
            this.textBoxMinOffset.Location = new System.Drawing.Point(124, 10);
            this.textBoxMinOffset.Name = "textBoxMinOffset";
            this.textBoxMinOffset.Size = new System.Drawing.Size(100, 20);
            this.textBoxMinOffset.TabIndex = 13;
            this.textBoxMinOffset.Text = "60";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(684, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Shoot Eve";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(684, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Shoot Morn";
            // 
            // textBoxShootTimeEve
            // 
            this.textBoxShootTimeEve.Location = new System.Drawing.Point(747, 50);
            this.textBoxShootTimeEve.Name = "textBoxShootTimeEve";
            this.textBoxShootTimeEve.Size = new System.Drawing.Size(100, 20);
            this.textBoxShootTimeEve.TabIndex = 10;
            // 
            // textBoxShootTimeMorn
            // 
            this.textBoxShootTimeMorn.Location = new System.Drawing.Point(747, 24);
            this.textBoxShootTimeMorn.Name = "textBoxShootTimeMorn";
            this.textBoxShootTimeMorn.Size = new System.Drawing.Size(100, 20);
            this.textBoxShootTimeMorn.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(468, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Before Sunset";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(475, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "After Sunrise";
            // 
            // textBoxBeforeSunset
            // 
            this.textBoxBeforeSunset.Location = new System.Drawing.Point(548, 50);
            this.textBoxBeforeSunset.Name = "textBoxBeforeSunset";
            this.textBoxBeforeSunset.Size = new System.Drawing.Size(100, 20);
            this.textBoxBeforeSunset.TabIndex = 6;
            // 
            // textBoxAfterSunrise
            // 
            this.textBoxAfterSunrise.Location = new System.Drawing.Point(548, 24);
            this.textBoxAfterSunrise.Name = "textBoxAfterSunrise";
            this.textBoxAfterSunrise.Size = new System.Drawing.Size(100, 20);
            this.textBoxAfterSunrise.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(308, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sunset";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sunrise";
            // 
            // textBoxSunset
            // 
            this.textBoxSunset.Location = new System.Drawing.Point(354, 50);
            this.textBoxSunset.Name = "textBoxSunset";
            this.textBoxSunset.Size = new System.Drawing.Size(100, 20);
            this.textBoxSunset.TabIndex = 2;
            // 
            // textBoxSunrise
            // 
            this.textBoxSunrise.Location = new System.Drawing.Point(354, 24);
            this.textBoxSunrise.Name = "textBoxSunrise";
            this.textBoxSunrise.Size = new System.Drawing.Size(100, 20);
            this.textBoxSunrise.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataListView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxLog, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.58674F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.41325F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 143F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1283, 710);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.buttonRefreshCameraDisplay);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.pictureBoxEveningStatus);
            this.panel1.Controls.Add(this.textBoxSunrise);
            this.panel1.Controls.Add(this.pictureBoxMorningStatus);
            this.panel1.Controls.Add(this.textBoxSunset);
            this.panel1.Controls.Add(this.buttonEveningTrigger);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonMorningTrigger);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxAfterSunrise);
            this.panel1.Controls.Add(this.textBoxMinOffset);
            this.panel1.Controls.Add(this.textBoxBeforeSunset);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxShootTimeEve);
            this.panel1.Controls.Add(this.textBoxShootTimeMorn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1277, 127);
            this.panel1.TabIndex = 0;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLog.Location = new System.Drawing.Point(3, 569);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(1277, 138);
            this.richTextBoxLog.TabIndex = 1;
            this.richTextBoxLog.Text = "";
            // 
            // comboBoxDayDelay
            // 
            this.comboBoxDayDelay.FormattingEnabled = true;
            this.comboBoxDayDelay.Location = new System.Drawing.Point(61, 29);
            this.comboBoxDayDelay.Name = "comboBoxDayDelay";
            this.comboBoxDayDelay.Size = new System.Drawing.Size(74, 21);
            this.comboBoxDayDelay.TabIndex = 20;
            this.comboBoxDayDelay.SelectedIndexChanged += new System.EventHandler(this.comboBoxDayDelay_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Delay";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Shots";
            // 
            // comboBoxDayNumShots
            // 
            this.comboBoxDayNumShots.FormattingEnabled = true;
            this.comboBoxDayNumShots.Location = new System.Drawing.Point(61, 56);
            this.comboBoxDayNumShots.Name = "comboBoxDayNumShots";
            this.comboBoxDayNumShots.Size = new System.Drawing.Size(74, 21);
            this.comboBoxDayNumShots.TabIndex = 22;
            this.comboBoxDayNumShots.SelectedIndexChanged += new System.EventHandler(this.comboBoxDayNumShots_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Shots";
            // 
            // comboBoxNightNumShots
            // 
            this.comboBoxNightNumShots.FormattingEnabled = true;
            this.comboBoxNightNumShots.Location = new System.Drawing.Point(63, 57);
            this.comboBoxNightNumShots.Name = "comboBoxNightNumShots";
            this.comboBoxNightNumShots.Size = new System.Drawing.Size(74, 21);
            this.comboBoxNightNumShots.TabIndex = 26;
            this.comboBoxNightNumShots.SelectedIndexChanged += new System.EventHandler(this.comboBoxNightNumShots_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Delay";
            // 
            // comboBoxNightDelay
            // 
            this.comboBoxNightDelay.FormattingEnabled = true;
            this.comboBoxNightDelay.Location = new System.Drawing.Point(63, 30);
            this.comboBoxNightDelay.Name = "comboBoxNightDelay";
            this.comboBoxNightDelay.Size = new System.Drawing.Size(74, 21);
            this.comboBoxNightDelay.TabIndex = 24;
            this.comboBoxNightDelay.SelectedIndexChanged += new System.EventHandler(this.comboBoxNightDelay_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxDayNumShots);
            this.groupBox1.Controls.Add(this.comboBoxDayDelay);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(876, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 100);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Day";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxNightNumShots);
            this.groupBox2.Controls.Add(this.comboBoxNightDelay);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(1054, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 100);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Night";
            // 
            // SpyPointSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 734);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SpyPointSettingsForm";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.SpyPointSettingsForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataListView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEveningStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMorningStatus)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.DataListView dataListView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxBeforeSunset;
        private System.Windows.Forms.TextBox textBoxAfterSunrise;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSunset;
        private System.Windows.Forms.TextBox textBoxSunrise;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxShootTimeEve;
        private System.Windows.Forms.TextBox textBoxShootTimeMorn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxMinOffset;
        private System.Windows.Forms.PictureBox pictureBoxEveningStatus;
        private System.Windows.Forms.PictureBox pictureBoxMorningStatus;
        private System.Windows.Forms.Button buttonEveningTrigger;
        private System.Windows.Forms.Button buttonMorningTrigger;
        private System.Windows.Forms.Button buttonRefreshCameraDisplay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxDayDelay;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxDayNumShots;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxNightNumShots;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxNightDelay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

