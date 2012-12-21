using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsyncTcpMessages.Simulator
{
    public partial class ConnectionPanel : UserControl
    {
        public ConnectionPanel()
        {
            InitializeComponent();            
        }

        public string ConnectionName
        {
            get { return this.labelConnectionName.Text; }
            set { this.labelConnectionName.Text = value; }
        }

        public void AppendText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                this.textBox1.BeginInvoke(new MethodInvoker(() =>
                {
                    this.textBox1.AppendText(text);
                }));
            }
            else
            {
                this.textBox1.AppendText(text);
            }
        }
    }
}
