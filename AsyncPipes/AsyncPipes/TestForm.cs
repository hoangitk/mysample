using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsyncPipes
{
    public  abstract partial class TestForm : Form
    {
        public AsyncPipes.PipeBase Pipe { get; set; }
        Random rand = new Random();

        private bool _timerRunning;
        public bool TimerRunning
        {
            get { return _timerRunning; }
            set
            {
                _timerRunning = value;

                if (_timerRunning)
                {
                    timer1.Stop();
                    this.button2.Text = "Start";
                }
                else
                {
                    timer1.Start();
                    this.button2.Text = "Stop";
                }
            }
        }

        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            Pipe = GetNewPipe();
            Pipe.ReceivedMessage += (s, me) =>
            {
                this.SafeInvoke(() =>
                {   
                    this.txtReceive.AppendText(string.Format("{0}\n{1}\t\n--------\n", 
                        DateTime.Now,
                        Encoding.UTF8.GetString(me.Message)));                    
                });
            };
            
            Pipe.Start();
            TimerRunning = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Send(ExMethod.RandomString(rand.Next(100)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TimerRunning = !TimerRunning;
        }

        public abstract AsyncPipes.PipeBase GetNewPipe();

        private void button1_Click(object sender, EventArgs e)
        {          
            Send(this.textBox2.Text);            
        }

        private void Send(string message)
        {
            Pipe.Send(Encoding.UTF8.GetBytes(message));
            this.txtSend.AppendText(string.Format("{0}\n{1}\t\n--------\n",
                DateTime.Now,
                message));
        }

    }
}
