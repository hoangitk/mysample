using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsyncTcpMessages.Simulator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.toolStripButton1.Click += (s, e) =>
            {
                var serverForm = ServerForm.Instance;
                serverForm.MdiParent = this;
                serverForm.Show();
            };

            this.toolStripButton2.Click += (s, e) =>
            {
                ClientForm newClientForm = new ClientForm();
                newClientForm.MdiParent = this;
                newClientForm.Show();
            };
        }
    }
}
