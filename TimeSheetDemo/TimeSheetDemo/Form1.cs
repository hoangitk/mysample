using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeSheetControl;

namespace TimeSheetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Load += (s, e) =>
            {
                var datasource = SampleData.Default.GenerateTimeSheetItemsBindingList(DateTime.Now.AddDays(-15), DateTime.Now.AddDays(15));

                foreach (TimeSheetItem item in datasource)
                {
                    foreach (TimeSheetDay tsDay in item.TimeSheetDays)
                    {
                        if (tsDay.LeaveItems != null && tsDay.LeaveItems.Count > 0)
                        {
                            foreach (TimeSheetRecord tsRecord in tsDay.LeaveItems)
                            {
                                tsRecord.PropertyChanged += (ps, pe) =>
                                {
                                    Trace.WriteLine(pe.PropertyName);
                                };
                            }
                        }
                    }
                }                
            };
        }
    }
}
