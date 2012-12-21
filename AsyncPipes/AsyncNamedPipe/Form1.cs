﻿using System;
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
    public partial class Form1 : TestForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        public override PipeBase GetNewPipe()
        {
            return new AsyncPipes.PipeServer("MyPipeServer");
        }
    }
}