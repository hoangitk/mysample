﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public interface IViewBuilder
    {
        PropertyDescriptorCollection GetView();
    }
}