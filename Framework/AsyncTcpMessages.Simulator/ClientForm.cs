using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cadena.Library.Serialization;

namespace AsyncTcpMessages.Simulator
{
    public partial class ClientForm : Form
    {
        private TcpMessageClient _client;

        public ClientForm()
        {
            InitializeComponent();
            this.Load += (se, ev) =>
            {
                _client = new TcpMessageClient();
                _client.MessageReceived += (s, e) =>
                {
                    object receivedObj = ObjectSerializer.FromBinary(e.PayLoad);
                    this.textBox2.AppendText(string.Format("[{0}] - {1}/{2}" + Environment.NewLine,
                        DateTime.Now, receivedObj, e.PayLoad.Length));
                };

                _client.Connect(MyConfig.HostName, MyConfig.Port);
            };

            this.button1.Click += (s, e) =>
            {
                if (File.Exists(this.textBox1.Text))
                {
                    _client.Send(new FileMessage(this.textBox1.Text));
                }
                else
                {
                    _client.Send(this.textBox1.Text);
                }
            };

        }
    }
}
