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

        public static string RandomString(int length)
        {
            Random ramdom = new Random();
            string[] array = new string[54]	{
                "0","2","3","4","5","6","8","9",
                "a","b","c","d","e","f","g","h","j","k","m","n","p","q","r","s","t","u","v","w","x","y","z",
                "A","B","C","D","E","F","G","H","J","K","L","M","N","P","R","S","T","U","V","W","X","Y","Z"
            };

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            for (int i = 0; i < length; i++) sb.Append(array[ramdom.Next(53)]);
            
            return sb.ToString();
        }
    }
}
