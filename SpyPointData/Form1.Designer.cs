﻿namespace SpyPointData
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCardPicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importManualPicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bucksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buckAgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAge0p5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAge1p5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAge2p5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAge3p5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAge4p5 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeCameraNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setLocationCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buckIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weatherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLocation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnHaveGPS = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDeer = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnBuck = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnBuckAge = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.comboBoxBuckIDs = new System.Windows.Forms.ComboBox();
            this.comboBoxChartType = new System.Windows.Forms.ComboBox();
            this.chartHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelCamName = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.imageBox1 = new Cyotek.Windows.Forms.ImageBox();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.olvColumnBuckName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.dataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1492, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.importToolStripMenuItem,
            this.mergeToolStripMenuItem,
            this.importCardPicsToolStripMenuItem,
            this.importManualPicsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.importToolStripMenuItem.Text = "Import From Server";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.mergeToolStripMenuItem.Text = "Merge from Server";
            this.mergeToolStripMenuItem.Click += new System.EventHandler(this.mergeToolStripMenuItem_Click);
            // 
            // importCardPicsToolStripMenuItem
            // 
            this.importCardPicsToolStripMenuItem.Name = "importCardPicsToolStripMenuItem";
            this.importCardPicsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.importCardPicsToolStripMenuItem.Text = "Import Card Pics";
            this.importCardPicsToolStripMenuItem.Click += new System.EventHandler(this.importCardPicsToolStripMenuItem_Click);
            // 
            // importManualPicsToolStripMenuItem
            // 
            this.importManualPicsToolStripMenuItem.Name = "importManualPicsToolStripMenuItem";
            this.importManualPicsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.importManualPicsToolStripMenuItem.Text = "Import Manual Pics";
            this.importManualPicsToolStripMenuItem.Click += new System.EventHandler(this.importManualPicsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deerToolStripMenuItem,
            this.bucksToolStripMenuItem,
            this.buckAgeToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // deerToolStripMenuItem
            // 
            this.deerToolStripMenuItem.CheckOnClick = true;
            this.deerToolStripMenuItem.Name = "deerToolStripMenuItem";
            this.deerToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deerToolStripMenuItem.Text = "Deer";
            this.deerToolStripMenuItem.Click += new System.EventHandler(this.deerToolStripMenuItem_Click);
            // 
            // bucksToolStripMenuItem
            // 
            this.bucksToolStripMenuItem.CheckOnClick = true;
            this.bucksToolStripMenuItem.Name = "bucksToolStripMenuItem";
            this.bucksToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.bucksToolStripMenuItem.Text = "Bucks";
            this.bucksToolStripMenuItem.Click += new System.EventHandler(this.bucksToolStripMenuItem_Click);
            // 
            // buckAgeToolStripMenuItem
            // 
            this.buckAgeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAge0p5,
            this.toolStripMenuItemAge1p5,
            this.toolStripMenuItemAge2p5,
            this.toolStripMenuItemAge3p5,
            this.toolStripMenuItemAge4p5});
            this.buckAgeToolStripMenuItem.Name = "buckAgeToolStripMenuItem";
            this.buckAgeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.buckAgeToolStripMenuItem.Text = "Buck Age";
            // 
            // toolStripMenuItemAge0p5
            // 
            this.toolStripMenuItemAge0p5.Checked = true;
            this.toolStripMenuItemAge0p5.CheckOnClick = true;
            this.toolStripMenuItemAge0p5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemAge0p5.Name = "toolStripMenuItemAge0p5";
            this.toolStripMenuItemAge0p5.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItemAge0p5.Text = "0.5";
            this.toolStripMenuItemAge0p5.Click += new System.EventHandler(this.bucksToolStripMenuItem_Click);
            // 
            // toolStripMenuItemAge1p5
            // 
            this.toolStripMenuItemAge1p5.Checked = true;
            this.toolStripMenuItemAge1p5.CheckOnClick = true;
            this.toolStripMenuItemAge1p5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemAge1p5.Name = "toolStripMenuItemAge1p5";
            this.toolStripMenuItemAge1p5.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItemAge1p5.Text = "1.5";
            this.toolStripMenuItemAge1p5.Click += new System.EventHandler(this.bucksToolStripMenuItem_Click);
            // 
            // toolStripMenuItemAge2p5
            // 
            this.toolStripMenuItemAge2p5.Checked = true;
            this.toolStripMenuItemAge2p5.CheckOnClick = true;
            this.toolStripMenuItemAge2p5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemAge2p5.Name = "toolStripMenuItemAge2p5";
            this.toolStripMenuItemAge2p5.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItemAge2p5.Text = "2.5";
            this.toolStripMenuItemAge2p5.Click += new System.EventHandler(this.bucksToolStripMenuItem_Click);
            // 
            // toolStripMenuItemAge3p5
            // 
            this.toolStripMenuItemAge3p5.Checked = true;
            this.toolStripMenuItemAge3p5.CheckOnClick = true;
            this.toolStripMenuItemAge3p5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemAge3p5.Name = "toolStripMenuItemAge3p5";
            this.toolStripMenuItemAge3p5.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItemAge3p5.Text = "3.5";
            this.toolStripMenuItemAge3p5.Click += new System.EventHandler(this.bucksToolStripMenuItem_Click);
            // 
            // toolStripMenuItemAge4p5
            // 
            this.toolStripMenuItemAge4p5.Checked = true;
            this.toolStripMenuItemAge4p5.CheckOnClick = true;
            this.toolStripMenuItemAge4p5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemAge4p5.Name = "toolStripMenuItemAge4p5";
            this.toolStripMenuItemAge4p5.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItemAge4p5.Text = "4.5";
            this.toolStripMenuItemAge4p5.Click += new System.EventHandler(this.bucksToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem,
            this.changeCameraNameToolStripMenuItem,
            this.setLocationCoordinatesToolStripMenuItem,
            this.buckIDToolStripMenuItem,
            this.enableMapToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraIdToolStripMenuItem,
            this.locationToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // cameraIdToolStripMenuItem
            // 
            this.cameraIdToolStripMenuItem.Checked = true;
            this.cameraIdToolStripMenuItem.CheckOnClick = true;
            this.cameraIdToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cameraIdToolStripMenuItem.Name = "cameraIdToolStripMenuItem";
            this.cameraIdToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.cameraIdToolStripMenuItem.Text = "CameraId";
            this.cameraIdToolStripMenuItem.Click += new System.EventHandler(this.cameraIdToolStripMenuItem_Click);
            // 
            // locationToolStripMenuItem
            // 
            this.locationToolStripMenuItem.CheckOnClick = true;
            this.locationToolStripMenuItem.Name = "locationToolStripMenuItem";
            this.locationToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.locationToolStripMenuItem.Text = "Location";
            this.locationToolStripMenuItem.Click += new System.EventHandler(this.locationToolStripMenuItem_Click);
            // 
            // changeCameraNameToolStripMenuItem
            // 
            this.changeCameraNameToolStripMenuItem.Name = "changeCameraNameToolStripMenuItem";
            this.changeCameraNameToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.changeCameraNameToolStripMenuItem.Text = "Change Camera Name";
            this.changeCameraNameToolStripMenuItem.Click += new System.EventHandler(this.changeCameraNameToolStripMenuItem_Click);
            // 
            // setLocationCoordinatesToolStripMenuItem
            // 
            this.setLocationCoordinatesToolStripMenuItem.Name = "setLocationCoordinatesToolStripMenuItem";
            this.setLocationCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.setLocationCoordinatesToolStripMenuItem.Text = "Set Location Coordinates";
            this.setLocationCoordinatesToolStripMenuItem.Click += new System.EventHandler(this.setLocationCoordinatesToolStripMenuItem_Click);
            // 
            // buckIDToolStripMenuItem
            // 
            this.buckIDToolStripMenuItem.Name = "buckIDToolStripMenuItem";
            this.buckIDToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.buckIDToolStripMenuItem.Text = "BuckID";
            this.buckIDToolStripMenuItem.Click += new System.EventHandler(this.buckIDToolStripMenuItem_Click);
            // 
            // enableMapToolStripMenuItem
            // 
            this.enableMapToolStripMenuItem.Checked = true;
            this.enableMapToolStripMenuItem.CheckOnClick = true;
            this.enableMapToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableMapToolStripMenuItem.Name = "enableMapToolStripMenuItem";
            this.enableMapToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.enableMapToolStripMenuItem.Text = "Enable Map";
            this.enableMapToolStripMenuItem.Click += new System.EventHandler(this.enableMapToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.weatherToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // weatherToolStripMenuItem
            // 
            this.weatherToolStripMenuItem.Name = "weatherToolStripMenuItem";
            this.weatherToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.weatherToolStripMenuItem.Text = "Weather";
            this.weatherToolStripMenuItem.Click += new System.EventHandler(this.weatherToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeListView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1492, 612);
            this.splitContainer1.SplitterDistance = 614;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.olvColumnName);
            this.treeListView1.AllColumns.Add(this.olvColumnLocation);
            this.treeListView1.AllColumns.Add(this.olvColumnHaveGPS);
            this.treeListView1.AllColumns.Add(this.olvColumnCount);
            this.treeListView1.AllColumns.Add(this.olvColumnDeer);
            this.treeListView1.AllColumns.Add(this.olvColumnBuck);
            this.treeListView1.AllColumns.Add(this.olvColumnBuckAge);
            this.treeListView1.AllColumns.Add(this.olvColumnBuckName);
            this.treeListView1.CellEditUseWholeCell = false;
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnName,
            this.olvColumnLocation,
            this.olvColumnHaveGPS,
            this.olvColumnCount,
            this.olvColumnDeer,
            this.olvColumnBuck,
            this.olvColumnBuckAge,
            this.olvColumnBuckName});
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView1.GridLines = true;
            this.treeListView1.HideSelection = false;
            this.treeListView1.Location = new System.Drawing.Point(0, 0);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.SelectedBackColor = System.Drawing.Color.DodgerBlue;
            this.treeListView1.ShowFilterMenuOnRightClick = false;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(614, 612);
            this.treeListView1.TabIndex = 0;
            this.treeListView1.TriggerCellOverEventsWhenOverHeader = false;
            this.treeListView1.UnfocusedSelectedBackColor = System.Drawing.Color.Gray;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.UseFiltering = true;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            this.treeListView1.SelectionChanged += new System.EventHandler(this.treeListView1_SelectionChanged);
            this.treeListView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeListView1_MouseDoubleClick);
            // 
            // olvColumnName
            // 
            this.olvColumnName.MinimumWidth = 50;
            this.olvColumnName.Text = "Name";
            this.olvColumnName.Width = 200;
            // 
            // olvColumnLocation
            // 
            this.olvColumnLocation.MinimumWidth = 100;
            this.olvColumnLocation.Text = "Location";
            this.olvColumnLocation.Width = 120;
            // 
            // olvColumnHaveGPS
            // 
            this.olvColumnHaveGPS.MinimumWidth = 35;
            this.olvColumnHaveGPS.Text = "GPS";
            this.olvColumnHaveGPS.Width = 35;
            // 
            // olvColumnCount
            // 
            this.olvColumnCount.MinimumWidth = 40;
            this.olvColumnCount.Text = "Count";
            this.olvColumnCount.Width = 40;
            // 
            // olvColumnDeer
            // 
            this.olvColumnDeer.MinimumWidth = 40;
            this.olvColumnDeer.Text = "Deer";
            this.olvColumnDeer.Width = 40;
            // 
            // olvColumnBuck
            // 
            this.olvColumnBuck.MinimumWidth = 40;
            this.olvColumnBuck.Text = "Buck";
            this.olvColumnBuck.Width = 40;
            // 
            // olvColumnBuckAge
            // 
            this.olvColumnBuckAge.MinimumWidth = 40;
            this.olvColumnBuckAge.Text = "Age";
            this.olvColumnBuckAge.Width = 40;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.comboBoxBuckIDs);
            this.splitContainer2.Panel1.Controls.Add(this.comboBoxChartType);
            this.splitContainer2.Panel1.Controls.Add(this.chartHistogram);
            this.splitContainer2.Panel1.Controls.Add(this.labelCamName);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(874, 612);
            this.splitContainer2.SplitterDistance = 199;
            this.splitContainer2.TabIndex = 1;
            // 
            // comboBoxBuckIDs
            // 
            this.comboBoxBuckIDs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuckIDs.FormattingEnabled = true;
            this.comboBoxBuckIDs.Location = new System.Drawing.Point(895, 74);
            this.comboBoxBuckIDs.Name = "comboBoxBuckIDs";
            this.comboBoxBuckIDs.Size = new System.Drawing.Size(172, 21);
            this.comboBoxBuckIDs.TabIndex = 8;
            this.comboBoxBuckIDs.SelectedIndexChanged += new System.EventHandler(this.comboBoxBuckIDs_SelectedIndexChanged);
            // 
            // comboBoxChartType
            // 
            this.comboBoxChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChartType.FormattingEnabled = true;
            this.comboBoxChartType.Location = new System.Drawing.Point(32, 88);
            this.comboBoxChartType.Name = "comboBoxChartType";
            this.comboBoxChartType.Size = new System.Drawing.Size(102, 21);
            this.comboBoxChartType.TabIndex = 4;
            // 
            // chartHistogram
            // 
            chartArea5.Name = "ChartArea1";
            this.chartHistogram.ChartAreas.Add(chartArea5);
            legend5.Enabled = false;
            legend5.Name = "Legend1";
            this.chartHistogram.Legends.Add(legend5);
            this.chartHistogram.Location = new System.Drawing.Point(171, 9);
            this.chartHistogram.Name = "chartHistogram";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chartHistogram.Series.Add(series5);
            this.chartHistogram.Size = new System.Drawing.Size(718, 160);
            this.chartHistogram.TabIndex = 3;
            // 
            // labelCamName
            // 
            this.labelCamName.AutoSize = true;
            this.labelCamName.Location = new System.Drawing.Point(51, 56);
            this.labelCamName.Name = "labelCamName";
            this.labelCamName.Size = new System.Drawing.Size(0, 13);
            this.labelCamName.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.imageBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.gMapControl1);
            this.splitContainer3.Size = new System.Drawing.Size(874, 409);
            this.splitContainer3.SplitterDistance = 463;
            this.splitContainer3.TabIndex = 2;
            // 
            // imageBox1
            // 
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.Location = new System.Drawing.Point(0, 0);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(463, 409);
            this.imageBox1.TabIndex = 0;
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(0, 0);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 18;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(407, 409);
            this.gMapControl1.TabIndex = 1;
            this.gMapControl1.Zoom = 16D;
            this.gMapControl1.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gMapControl1_OnMarkerClick);
            // 
            // olvColumnBuckName
            // 
            this.olvColumnBuckName.MinimumWidth = 40;
            this.olvColumnBuckName.Text = "ID";
            this.olvColumnBuckName.Width = 40;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1492, 636);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SpyPointData";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label labelCamName;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bucksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deerToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHistogram;
        private System.Windows.Forms.ComboBox comboBoxChartType;
        private System.Windows.Forms.ToolStripMenuItem importCardPicsToolStripMenuItem;
        private Cyotek.Windows.Forms.ImageBox imageBox1;
        private System.Windows.Forms.ToolStripMenuItem buckAgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAge0p5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAge1p5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAge2p5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAge3p5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAge4p5;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeCameraNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setLocationCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buckIDToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxBuckIDs;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weatherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importManualPicsToolStripMenuItem;
        private BrightIdeasSoftware.TreeListView treeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumnName;
        private BrightIdeasSoftware.OLVColumn olvColumnDeer;
        private BrightIdeasSoftware.OLVColumn olvColumnBuck;
        private BrightIdeasSoftware.OLVColumn olvColumnBuckAge;
        private BrightIdeasSoftware.OLVColumn olvColumnCount;
        private BrightIdeasSoftware.OLVColumn olvColumnLocation;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvColumnHaveGPS;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ToolStripMenuItem enableMapToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvColumnBuckName;

    }
}

