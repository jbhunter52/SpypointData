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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBoxSunrise = new System.Windows.Forms.TextBox();
            this.textBoxSunset = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxBeforeSunset = new System.Windows.Forms.TextBox();
            this.textBoxAfterSunrise = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxShootTimeEve = new System.Windows.Forms.TextBox();
            this.textBoxShootTimeMorn = new System.Windows.Forms.TextBox();
            this.textBoxMinOffset = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonMorningTrigger = new System.Windows.Forms.Button();
            this.buttonEveningTrigger = new System.Windows.Forms.Button();
            this.pictureBoxMorningStatus = new System.Windows.Forms.PictureBox();
            this.pictureBoxEveningStatus = new System.Windows.Forms.PictureBox();
            this.buttonRefreshCameraDisplay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMorningStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEveningStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // dataListView
            // 
            this.dataListView.CellEditUseWholeCell = false;
            this.dataListView.DataSource = null;
            this.dataListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataListView.HideSelection = false;
            this.dataListView.Location = new System.Drawing.Point(0, 0);
            this.dataListView.Name = "dataListView";
            this.dataListView.Size = new System.Drawing.Size(955, 489);
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
            this.menuStrip1.Size = new System.Drawing.Size(955, 24);
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonRefreshCameraDisplay);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxEveningStatus);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxMorningStatus);
            this.splitContainer1.Panel1.Controls.Add(this.buttonEveningTrigger);
            this.splitContainer1.Panel1.Controls.Add(this.buttonMorningTrigger);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxMinOffset);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxShootTimeEve);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxShootTimeMorn);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxBeforeSunset);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxAfterSunrise);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxSunset);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxSunrise);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataListView);
            this.splitContainer1.Size = new System.Drawing.Size(955, 606);
            this.splitContainer1.SplitterDistance = 113;
            this.splitContainer1.TabIndex = 2;
            // 
            // textBoxSunrise
            // 
            this.textBoxSunrise.Location = new System.Drawing.Point(397, 26);
            this.textBoxSunrise.Name = "textBoxSunrise";
            this.textBoxSunrise.Size = new System.Drawing.Size(100, 20);
            this.textBoxSunrise.TabIndex = 1;
            // 
            // textBoxSunset
            // 
            this.textBoxSunset.Location = new System.Drawing.Point(397, 52);
            this.textBoxSunset.Name = "textBoxSunset";
            this.textBoxSunset.Size = new System.Drawing.Size(100, 20);
            this.textBoxSunset.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sunrise";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(351, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sunset";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(511, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Before Sunset";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(518, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "After Sunrise";
            // 
            // textBoxBeforeSunset
            // 
            this.textBoxBeforeSunset.Location = new System.Drawing.Point(591, 52);
            this.textBoxBeforeSunset.Name = "textBoxBeforeSunset";
            this.textBoxBeforeSunset.Size = new System.Drawing.Size(100, 20);
            this.textBoxBeforeSunset.TabIndex = 6;
            // 
            // textBoxAfterSunrise
            // 
            this.textBoxAfterSunrise.Location = new System.Drawing.Point(591, 26);
            this.textBoxAfterSunrise.Name = "textBoxAfterSunrise";
            this.textBoxAfterSunrise.Size = new System.Drawing.Size(100, 20);
            this.textBoxAfterSunrise.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(727, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Shoot Eve";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(727, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Shoot Morn";
            // 
            // textBoxShootTimeEve
            // 
            this.textBoxShootTimeEve.Location = new System.Drawing.Point(790, 52);
            this.textBoxShootTimeEve.Name = "textBoxShootTimeEve";
            this.textBoxShootTimeEve.Size = new System.Drawing.Size(100, 20);
            this.textBoxShootTimeEve.TabIndex = 10;
            // 
            // textBoxShootTimeMorn
            // 
            this.textBoxShootTimeMorn.Location = new System.Drawing.Point(790, 26);
            this.textBoxShootTimeMorn.Name = "textBoxShootTimeMorn";
            this.textBoxShootTimeMorn.Size = new System.Drawing.Size(100, 20);
            this.textBoxShootTimeMorn.TabIndex = 9;
            // 
            // textBoxMinOffset
            // 
            this.textBoxMinOffset.Location = new System.Drawing.Point(167, 12);
            this.textBoxMinOffset.Name = "textBoxMinOffset";
            this.textBoxMinOffset.Size = new System.Drawing.Size(100, 20);
            this.textBoxMinOffset.TabIndex = 13;
            this.textBoxMinOffset.Text = "60";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Settings Offset (min)";
            // 
            // buttonMorningTrigger
            // 
            this.buttonMorningTrigger.Location = new System.Drawing.Point(63, 52);
            this.buttonMorningTrigger.Name = "buttonMorningTrigger";
            this.buttonMorningTrigger.Size = new System.Drawing.Size(98, 23);
            this.buttonMorningTrigger.TabIndex = 15;
            this.buttonMorningTrigger.Text = "MorningTrigger";
            this.buttonMorningTrigger.UseVisualStyleBackColor = true;
            this.buttonMorningTrigger.Click += new System.EventHandler(this.buttonMorningTrigger_Click);
            // 
            // buttonEveningTrigger
            // 
            this.buttonEveningTrigger.Location = new System.Drawing.Point(169, 52);
            this.buttonEveningTrigger.Name = "buttonEveningTrigger";
            this.buttonEveningTrigger.Size = new System.Drawing.Size(98, 23);
            this.buttonEveningTrigger.TabIndex = 16;
            this.buttonEveningTrigger.Text = "EveningTrigger";
            this.buttonEveningTrigger.UseVisualStyleBackColor = true;
            this.buttonEveningTrigger.Click += new System.EventHandler(this.buttonEveningTrigger_Click);
            // 
            // pictureBoxMorningStatus
            // 
            this.pictureBoxMorningStatus.Location = new System.Drawing.Point(103, 81);
            this.pictureBoxMorningStatus.Name = "pictureBoxMorningStatus";
            this.pictureBoxMorningStatus.Size = new System.Drawing.Size(24, 23);
            this.pictureBoxMorningStatus.TabIndex = 17;
            this.pictureBoxMorningStatus.TabStop = false;
            // 
            // pictureBoxEveningStatus
            // 
            this.pictureBoxEveningStatus.Location = new System.Drawing.Point(201, 81);
            this.pictureBoxEveningStatus.Name = "pictureBoxEveningStatus";
            this.pictureBoxEveningStatus.Size = new System.Drawing.Size(24, 23);
            this.pictureBoxEveningStatus.TabIndex = 18;
            this.pictureBoxEveningStatus.TabStop = false;
            // 
            // buttonRefreshCameraDisplay
            // 
            this.buttonRefreshCameraDisplay.Location = new System.Drawing.Point(287, 81);
            this.buttonRefreshCameraDisplay.Name = "buttonRefreshCameraDisplay";
            this.buttonRefreshCameraDisplay.Size = new System.Drawing.Size(104, 23);
            this.buttonRefreshCameraDisplay.TabIndex = 19;
            this.buttonRefreshCameraDisplay.Text = "Refresh Cams";
            this.buttonRefreshCameraDisplay.UseVisualStyleBackColor = true;
            this.buttonRefreshCameraDisplay.Click += new System.EventHandler(this.buttonRefreshCameraDisplay_Click);
            // 
            // SpyPointSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 630);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SpyPointSettingsForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataListView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMorningStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEveningStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.DataListView dataListView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
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
    }
}

