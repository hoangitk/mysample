using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class MyContextMenu : ContextMenuStrip
    {        
        public MyContextMenu() : base()
        {            
        }

        public MyContextMenu(IContainer container) : base(container)
        {         
        }

        public void AddNewCommand(string commandName, Action<object, EventArgs> command, System.Drawing.Image icon)
        {
            var newMenuItem = new ToolStripMenuItem(commandName);
            newMenuItem.Click += new EventHandler(command);
            if (icon != null)
            {
                newMenuItem.Image = icon;
            }

            this.Items.Add(newMenuItem);
        }

    }
}
