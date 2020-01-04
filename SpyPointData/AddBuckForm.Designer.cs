namespace SpyPointData
{
    partial class AddBuckNameForm
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
            this.textBoxBuckName = new System.Windows.Forms.TextBox();
            this.buttonSaveName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxBuckName
            // 
            this.textBoxBuckName.Location = new System.Drawing.Point(76, 24);
            this.textBoxBuckName.Name = "textBoxBuckName";
            this.textBoxBuckName.Size = new System.Drawing.Size(188, 20);
            this.textBoxBuckName.TabIndex = 0;
            // 
            // buttonSaveName
            // 
            this.buttonSaveName.Location = new System.Drawing.Point(109, 65);
            this.buttonSaveName.Name = "buttonSaveName";
            this.buttonSaveName.Size = new System.Drawing.Size(120, 50);
            this.buttonSaveName.TabIndex = 1;
            this.buttonSaveName.Text = "Save";
            this.buttonSaveName.UseVisualStyleBackColor = true;
            this.buttonSaveName.Click += new System.EventHandler(this.buttonSaveName_Click);
            // 
            // AddBuckNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 141);
            this.Controls.Add(this.buttonSaveName);
            this.Controls.Add(this.textBoxBuckName);
            this.KeyPreview = true;
            this.Name = "AddBuckNameForm";
            this.Text = "Add Buck Name";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddBuckNameForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxBuckName;
        private System.Windows.Forms.Button buttonSaveName;
    }
}