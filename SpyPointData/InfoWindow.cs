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
    public partial class InfoWindow : Form
    {
        
        public InfoWindow()
        {
            InitializeComponent();
        }
        public void SetTextData(string s)
        {
            richTextBox1.Clear();
            richTextBox1.AppendText(s);
            richTextBox1.SelectionStart = 0;
            richTextBox1.ScrollToCaret();
        }
    }
}
