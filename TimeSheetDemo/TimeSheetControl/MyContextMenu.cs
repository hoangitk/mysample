﻿using System;
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

        public void AddNewCommand(string commandId,  string commandName, Action<object, EventArgs> command = null, 
            System.Drawing.Image icon = null, string parentId = "", bool overrideIfExisted = false)
        {                        
            bool isExisted = this.Items.ContainsKey(commandId);
            if (!isExisted || (isExisted && overrideIfExisted))
            {
                var newMenuItem = new ToolStripMenuItem(commandName);
                newMenuItem.Name = commandId;

                if (command != null)
                {
                    newMenuItem.Click += new EventHandler(command);
                }

                if (icon != null)
                {
                    newMenuItem.Image = icon;
                }

                if (string.IsNullOrWhiteSpace(parentId))
                {
                    this.Items.Add(newMenuItem);
                }
                else
                {
                    var findMenuItems = this.Items.Find(parentId, true);

                    if (findMenuItems.Length == 1)
                    {
                        var parentMenuItem = findMenuItems[0] as ToolStripMenuItem;

                        if (parentMenuItem != null)
                        {
                            parentMenuItem.DropDownItems.Add(newMenuItem);
                        }
                        else
                        {
                            throw new NotToolStripMenuItem("Can not add command into not a ToolStripMenuItem");
                        }
                    }
                    else
                    {
                        throw new OverParentCommandException("There are many found MenuItems with specific commandId");
                    }
                }
            }
        }

        public void RemoveCommand(string commandId, bool searchAllChildren = false)
        {
            var findMenuItems = this.Items.Find(commandId, searchAllChildren);
            for (int i = 0; i < findMenuItems.Length; i++)
            {
                this.Items.Remove(findMenuItems[i]);
            }
        }
    }

    public class OverParentCommandException : Exception
    {
        public OverParentCommandException() : base()
        {            
        }

        public OverParentCommandException(string message) : base(message)
        {

        }
    }

    public class NotToolStripMenuItem : Exception
    {
        public NotToolStripMenuItem() : base()
        {

        }

        public NotToolStripMenuItem(string message) : base(message)
        {

        }
    }
}