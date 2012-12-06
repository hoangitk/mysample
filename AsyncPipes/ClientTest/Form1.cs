using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsyncPipes;

namespace ClientTest
{
    public partial class Form1 : Form
    {
        AsyncPipes.PipeClient _pipeClient;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Client";

            _pipeClient = new AsyncPipes.PipeClient("Client", "MyTestPipe");

            _pipeClient.ReceivedMessage += (s, me) =>
            {
                this.SafeInvoke(() =>
                {
                    this.textBox1.AppendText(Encoding.UTF8.GetString(me.Message));
                    this.textBox1.AppendText(Environment.NewLine);
                });
            };

            _pipeClient.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _pipeClient.Send(Encoding.UTF8.GetBytes(this.textBox2.Text));
            }
            catch (Exception ex)
            {
                this.textBox1.AppendText(ex.ToString());
            }
        }
    }
}
