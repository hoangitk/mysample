using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsyncPipes;

namespace ServerTest
{
    public partial class Form1 : Form
    {
        AsyncPipes.PipeServer _pipeServer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Server";

            _pipeServer = new AsyncPipes.PipeServer("MyTestPipe");

            _pipeServer.ReceivedMessage += (s, me) =>
            {
                this.SafeInvoke(() =>
                {
                    this.textBox1.AppendText(Encoding.UTF8.GetString(me.Message));
                    this.textBox1.AppendText(Environment.NewLine);
                });
            };

            _pipeServer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _pipeServer.Send(Encoding.UTF8.GetBytes(this.textBox2.Text));
            }
            catch (Exception ex)
            {
                this.textBox1.AppendText(ex.ToString());
            }
        }
    }
}
