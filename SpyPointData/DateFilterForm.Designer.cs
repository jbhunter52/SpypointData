
namespace SpyPointData
{
    partial class DateFilterForm
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
            this.dateTimePickerMin = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerMax = new System.Windows.Forms.DateTimePicker();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxIgnoreYear = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // dateTimePickerMin
            // 
            this.dateTimePickerMin.Location = new System.Drawing.Point(26, 43);
            this.dateTimePickerMin.Name = "dateTimePickerMin";
            this.dateTimePickerMin.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerMin.TabIndex = 0;
            // 
            // dateTimePickerMax
            // 
            this.dateTimePickerMax.Location = new System.Drawing.Point(346, 43);
            this.dateTimePickerMax.Name = "dateTimePickerMax";
            this.dateTimePickerMax.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerMax.TabIndex = 1;
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(148, 95);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(143, 37);
            this.buttonFilter.TabIndex = 2;
            this.buttonFilter.Text = "Filter";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(297, 95);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(143, 37);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxIgnoreYear
            // 
            this.checkBoxIgnoreYear.AutoSize = true;
            this.checkBoxIgnoreYear.Location = new System.Drawing.Point(483, 95);
            this.checkBoxIgnoreYear.Name = "checkBoxIgnoreYear";
            this.checkBoxIgnoreYear.Size = new System.Drawing.Size(81, 17);
            this.checkBoxIgnoreYear.TabIndex = 4;
            this.checkBoxIgnoreYear.Text = "Ignore Year";
            this.checkBoxIgnoreYear.UseVisualStyleBackColor = true;
            // 
            // DateFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 167);
            this.Controls.Add(this.checkBoxIgnoreYear);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.dateTimePickerMax);
            this.Controls.Add(this.dateTimePickerMin);
            this.Name = "DateFilterForm";
            this.Text = "DateFilterForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerMin;
        private System.Windows.Forms.DateTimePicker dateTimePickerMax;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxIgnoreYear;
    }
}