namespace SpyPointData
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCardPicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bucksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.comboBoxChartType = new System.Windows.Forms.ComboBox();
            this.chartHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.checkBoxDeer = new System.Windows.Forms.CheckBox();
            this.labelCamName = new System.Windows.Forms.Label();
            this.checkBoxBuck = new System.Windows.Forms.CheckBox();
            this.imageBox1 = new Cyotek.Windows.Forms.ImageBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1347, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.mergeToolStripMenuItem,
            this.importCardPicsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.importToolStripMenuItem.Text = "Import From Server";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.mergeToolStripMenuItem.Text = "Merge from Server";
            this.mergeToolStripMenuItem.Click += new System.EventHandler(this.mergeToolStripMenuItem_Click);
            // 
            // importCardPicsToolStripMenuItem
            // 
            this.importCardPicsToolStripMenuItem.Name = "importCardPicsToolStripMenuItem";
            this.importCardPicsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.importCardPicsToolStripMenuItem.Text = "Import Card Pics";
            this.importCardPicsToolStripMenuItem.Click += new System.EventHandler(this.importCardPicsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deerToolStripMenuItem,
            this.bucksToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // deerToolStripMenuItem
            // 
            this.deerToolStripMenuItem.CheckOnClick = true;
            this.deerToolStripMenuItem.Name = "deerToolStripMenuItem";
            this.deerToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.deerToolStripMenuItem.Text = "Deer";
            this.deerToolStripMenuItem.Click += new System.EventHandler(this.deerToolStripMenuItem_Click);
            // 
            // bucksToolStripMenuItem
            // 
            this.bucksToolStripMenuItem.CheckOnClick = true;
            this.bucksToolStripMenuItem.Name = "bucksToolStripMenuItem";
            this.bucksToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.bucksToolStripMenuItem.Text = "Bucks";
            this.bucksToolStripMenuItem.Click += new System.EventHandler(this.bucksToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1347, 612);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(246, 612);
            this.treeView1.TabIndex = 0;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
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
            this.splitContainer2.Panel1.Controls.Add(this.comboBoxChartType);
            this.splitContainer2.Panel1.Controls.Add(this.chartHistogram);
            this.splitContainer2.Panel1.Controls.Add(this.checkBoxDeer);
            this.splitContainer2.Panel1.Controls.Add(this.labelCamName);
            this.splitContainer2.Panel1.Controls.Add(this.checkBoxBuck);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.imageBox1);
            this.splitContainer2.Size = new System.Drawing.Size(1097, 612);
            this.splitContainer2.SplitterDistance = 199;
            this.splitContainer2.TabIndex = 1;
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
            chartArea6.Name = "ChartArea1";
            this.chartHistogram.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chartHistogram.Legends.Add(legend6);
            this.chartHistogram.Location = new System.Drawing.Point(171, 9);
            this.chartHistogram.Name = "chartHistogram";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chartHistogram.Series.Add(series6);
            this.chartHistogram.Size = new System.Drawing.Size(807, 160);
            this.chartHistogram.TabIndex = 3;
            // 
            // checkBoxDeer
            // 
            this.checkBoxDeer.AutoSize = true;
            this.checkBoxDeer.Location = new System.Drawing.Point(51, 9);
            this.checkBoxDeer.Name = "checkBoxDeer";
            this.checkBoxDeer.Size = new System.Drawing.Size(49, 17);
            this.checkBoxDeer.TabIndex = 2;
            this.checkBoxDeer.Text = "Deer";
            this.checkBoxDeer.UseVisualStyleBackColor = true;
            this.checkBoxDeer.CheckedChanged += new System.EventHandler(this.checkBoxDeer_CheckedChanged);
            // 
            // labelCamName
            // 
            this.labelCamName.AutoSize = true;
            this.labelCamName.Location = new System.Drawing.Point(51, 56);
            this.labelCamName.Name = "labelCamName";
            this.labelCamName.Size = new System.Drawing.Size(0, 13);
            this.labelCamName.TabIndex = 1;
            // 
            // checkBoxBuck
            // 
            this.checkBoxBuck.AutoSize = true;
            this.checkBoxBuck.Location = new System.Drawing.Point(51, 32);
            this.checkBoxBuck.Name = "checkBoxBuck";
            this.checkBoxBuck.Size = new System.Drawing.Size(51, 17);
            this.checkBoxBuck.TabIndex = 0;
            this.checkBoxBuck.Text = "Buck";
            this.checkBoxBuck.UseVisualStyleBackColor = true;
            this.checkBoxBuck.CheckedChanged += new System.EventHandler(this.checkBoxBuck_CheckedChanged);
            // 
            // imageBox1
            // 
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.Location = new System.Drawing.Point(0, 0);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(1097, 409);
            this.imageBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 636);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SpyPointData";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox checkBoxBuck;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label labelCamName;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bucksToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxDeer;
        private System.Windows.Forms.ToolStripMenuItem deerToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHistogram;
        private System.Windows.Forms.ComboBox comboBoxChartType;
        private System.Windows.Forms.ToolStripMenuItem importCardPicsToolStripMenuItem;
        private Cyotek.Windows.Forms.ImageBox imageBox1;

    }
}

