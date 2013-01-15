using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeSheetControl
{
    public class TimeSheetGridContextMenu : MyContextMenu
    {
        public MyMenuItem Copy             { get; private set; }
        public MyMenuItem Paste            { get; private set; }
        public MyMenuItem AssignShift      { get; private set; }
        public MyMenuItem DeleteShift      { get; private set; }
        public MyMenuItem AssignFullDayOff { get; private set; }
        public MyMenuItem AssignHalfDayOff { get; private set; }
        public MyMenuItem DeleteDayOff     { get; private set; }
        public MyMenuItem AssignFullLeave  { get; private set; }
        public MyMenuItem AssignHalfLeave  { get; private set; }
        public MyMenuItem DeleteLeave      { get; private set; }

        public TimeSheetGridContextMenu()
        {
            this.Copy             = new MyMenuItem("Copy"               );
            this.Paste            = new MyMenuItem("Paste"              );
            this.AssignShift      = new MyMenuItem("Assign Shift"       );
            this.DeleteShift      = new MyMenuItem("Delete Shift"       );
            this.AssignFullDayOff = new MyMenuItem("Assign Full Day Off");
            this.AssignHalfDayOff = new MyMenuItem("Assign Half Day Off");
            this.DeleteDayOff     = new MyMenuItem("Delete Day Off"     );
            this.AssignFullLeave  = new MyMenuItem("Assign Full Leave"  );
            this.AssignHalfLeave  = new MyMenuItem("Assign Half Leave"  );
            this.DeleteLeave      = new MyMenuItem("Delete Leave"       );

            this.AddMenuItem(this.Copy             );
            this.AddMenuItem(this.Paste            );
            this.AddMenuItem(this.AssignShift      );
            this.AddMenuItem(this.DeleteShift      );
            this.AddMenuItem(this.AssignFullDayOff );
            this.AddMenuItem(this.AssignHalfDayOff );
            this.AddMenuItem(this.DeleteDayOff     );
            this.AddMenuItem(this.AssignFullLeave  );
            this.AddMenuItem(this.AssignHalfLeave  );
            this.AddMenuItem(this.DeleteLeave      );
        }
    }
}
