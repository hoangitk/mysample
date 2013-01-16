using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeSheetControl
{
    public class TimeSheetGridContextMenu : MyContextMenu
    {
        public MyMenuItem MenuItemCopy               { get; private set; }
        public MyMenuItem MenuItemPaste              { get; private set; }
        public MyMenuItem MenuItemPasteNormal        { get; private set; }
        public MyMenuItem MenuItemPasteSelectedCells { get; private set; }
        public MyMenuItem MenuItemAssignShift        { get; private set; }        
        public MyMenuItem MenuItemAssignFullDayOff   { get; private set; }
        public MyMenuItem MenuItemAssignHalfDayOff   { get; private set; }
        public MyMenuItem MenuItemAssignFullLeave    { get; private set; }
        public MyMenuItem MenuItemAssignHalfLeave    { get; private set; }
        public MyMenuItem MenuItemDelete             { get; private set; }        

        public TimeSheetGridContextMenu()
        {
            this.MenuItemCopy               = new MyMenuItem("Copy"               );
            this.MenuItemPaste              = new MyMenuItem("Paste"              );
            this.MenuItemPasteNormal        = new MyMenuItem("Normal"             );
            this.MenuItemPasteSelectedCells = new MyMenuItem("Into selected cell(s)");
            this.MenuItemAssignShift        = new MyMenuItem("Assign Shift"       );            
            this.MenuItemAssignFullDayOff   = new MyMenuItem("Assign Full Day Off");
            this.MenuItemAssignHalfDayOff   = new MyMenuItem("Assign Half Day Off");            
            this.MenuItemAssignFullLeave    = new MyMenuItem("Assign Full Leave"  );
            this.MenuItemAssignHalfLeave    = new MyMenuItem("Assign Half Leave"  );
            this.MenuItemDelete             = new MyMenuItem("Delete"             );            

            this.AddMenuItem(this.MenuItemCopy             );
            this.AddMenuItem(this.MenuItemPaste 
                                    .AddChild(this.MenuItemPasteNormal)
                                    .AddChild(this.MenuItemPasteSelectedCells)
                            );
            this.AddSeperator();
            this.AddMenuItem(this.MenuItemAssignShift      );
            this.AddMenuItem(this.MenuItemAssignFullDayOff );
            this.AddMenuItem(this.MenuItemAssignHalfDayOff );
            this.AddMenuItem(this.MenuItemAssignFullLeave  );
            this.AddMenuItem(this.MenuItemAssignHalfLeave  );
            this.AddSeperator();
            this.AddMenuItem(this.MenuItemDelete           );
        }
    }
}
