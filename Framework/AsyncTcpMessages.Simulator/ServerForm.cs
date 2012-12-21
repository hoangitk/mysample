using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Cadena.Library.Serialization;

namespace AsyncTcpMessages.Simulator
{
    public partial class ServerForm : Form
    {
        private static ServerForm _instance = null;
        public static ServerForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServerForm();

                return _instance;
            }
        }

        TcpMessageServer _server;

        private ServerForm()
        {
            InitializeComponent();

            this.Load += (se, ev) =>
            {
                _server = new TcpMessageServer(IPAddress.Any, MyConfig.Port);

                _server.ClientConnected += (s, e) =>
                {
                    var p = this.panelContainer.Controls.Find(s.GetHashCode().ToString(), false);
                    if (p.Length == 0)
                    {
                        var newCnnPanel = new ConnectionPanel();
                        newCnnPanel.Name = s.GetHashCode().ToString();
                        newCnnPanel.ConnectionName = newCnnPanel.Name;
                        newCnnPanel.Width = this.panelContainer.Width - 10;                        
                        SafeInvoke(this.panelContainer,
                            () => { this.panelContainer.Controls.Add(newCnnPanel); });
                    }
                };

                _server.MessageReceived += (s, e) =>
                {
                    var p = this.panelContainer.Controls.Find(s.GetHashCode().ToString(), false);
                    if (p != null && p.Length == 1)
                    {
                        string output = string.Empty;

                        object receivedObj = ObjectSerializer.FromBinary(e.PayLoad);
                                                
                        if (receivedObj.GetType() == typeof(FileMessage))
                        {
                            FileMessage fileMsg = (FileMessage)receivedObj;
                            output = fileMsg.FileName;
                            File.WriteAllBytes(output, fileMsg.FileInBytes);
                        }
                        else if (receivedObj.GetType() == typeof(string))
                        {
                            output = (string)receivedObj;
                        }                        

                        var cnnPanel = (ConnectionPanel)p[0];
                        cnnPanel.AppendText(string.Format("[{0}] - {1}/{2}" + Environment.NewLine,
                            DateTime.Now, 
                            output, e.PayLoad.Length));
                        
                    }
                };

                _server.ClientDisconnected += (s, e) =>
                {
                    var p = this.panelContainer.Controls.Find(s.GetHashCode().ToString(), false);
                    if (p != null && p.Length == 1)
                    {
                        p[0].Enabled = false;
                    }
                };

                _server.Start();
            };

            this.button1.Click += (s, e) =>
            {
                _server.Send(this.textBox1.Text);
            };
        }

        public void SafeInvoke(Control control, Action act)
        {
            if (control.InvokeRequired)
                control.BeginInvoke(new MethodInvoker(act));
            else
                act();
        }

        private void panelContainer_Layout(object sender, LayoutEventArgs e)
        {
            FlowLayoutPanel flowPanel = sender as FlowLayoutPanel;
            
            if (flowPanel != null)
            {
                if (flowPanel.Controls.Count > 0)
                {
                    flowPanel.Controls[0].Dock = DockStyle.None;
                    for (int i = 1; i < flowPanel.Controls.Count; i++)
                    {
                        flowPanel.Controls[i].Dock = DockStyle.Top;
                    }
                    flowPanel.Controls[0].Width = flowPanel.DisplayRectangle.Width - flowPanel.Controls[0].Margin.Horizontal;
                }
            }
        }
    }
}
