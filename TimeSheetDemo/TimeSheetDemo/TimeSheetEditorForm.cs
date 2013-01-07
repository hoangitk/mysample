using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeSheetControl;

namespace TimeSheetDemo
{
    public partial class TimeSheetEditorForm : Form
    {
        public TimeSheetEditorForm(TimeSheetDay tsDay)
        {
            InitializeComponent();

            this.timeSheetDayBindingSource.DataSource = tsDay;
        }
    }
}
