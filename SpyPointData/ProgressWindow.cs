using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpyPointData
{
    public partial class ProgressWindow : Form
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }
        public void AddText(string s)
        {
            richTextBox1.AppendText(DateTime.Now.ToLongTimeString() +",     " + s + "\n");
            // set the current caret position to the end
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();
        }
        public void Start()
        {
            progressBar1.MarqueeAnimationSpeed = 200;
            progressBar1.Style = ProgressBarStyle.Marquee;
        }

    }
}
