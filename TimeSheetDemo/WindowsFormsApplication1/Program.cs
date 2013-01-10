using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using TimeSheetControl;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TypeDescriptor.AddProvider(new TimeSheetItemDescriptionProvider(), typeof(TimeSheetItem));
            Application.Run(new Form1());
        }
    }
}
