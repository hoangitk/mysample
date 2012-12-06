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
    public partial class ClientForm : TestForm
    {
        public ClientForm() : base()
        {
            InitializeComponent();
        }

        public override PipeBase GetNewPipe()
        {
            return new AsyncPipes.PipeClient("Client", "MyPipeServer");
        }
    }
}
