using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsyncPipes
{
    public static class ExMethod
    {
        public static void SafeInvoke(this Form frm, Action act)
        {
            if (frm.InvokeRequired)
            {
                frm.Invoke(new MethodInvoker(act));
            }
            else
            {
                act();
            }
        }
    }
}
