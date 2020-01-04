namespace SpyPointData
{
    partial class ChangeCamNameForm
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
            this.textBoxCameraName = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxCameraName
            // 
            this.textBoxCameraName.Location = new System.Drawing.Point(76, 24);
            this.textBoxCameraName.Name = "textBoxCameraName";
            this.textBoxCameraName.Size = new System.Drawing.Size(188, 20);
            this.textBoxCameraName.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(109, 65);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 50);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ChangeCamNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 141);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSave);
            this.KeyPreview = true;
            this.Name = "ChangeCamNameForm";
            this.Text = "Change Camera Name";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangeCamNameForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCameraName;
        private System.Windows.Forms.Button buttonSave;
    }
}