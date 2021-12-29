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
    public partial class DateFilterForm : Form
    {
        public FilterCriteria Filter;
        public DateFilterForm(FilterCriteria filter)
        {
            InitializeComponent();
            Filter = filter;
            dateTimePickerMin.Value = Filter.MinDate;
            dateTimePickerMax.Value = Filter.MaxDate;
            checkBoxIgnoreYear.Checked = Filter.DateIgnoreYear;
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            Filter.MinDate = dateTimePickerMin.Value;
            Filter.MaxDate = dateTimePickerMax.Value;
            Filter.Date = true;
            Filter.DateIgnoreYear = checkBoxIgnoreYear.Checked;
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Filter.MinDate = dateTimePickerMin.Value;
            Filter.MaxDate = dateTimePickerMax.Value;
            Filter.Date = false;
            DialogResult = DialogResult.OK;
        }
    }
}
