using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeSheetControl;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSheetItemCollection tsItems = new TimeSheetItemCollection();
            tsItems.ItemAdded += (s, e) =>
            {
                Console.WriteLine("A new TS Item was added: " + e.AddedTimeSheetItem.EmployeeFullName);
            };
            tsItems.ItemDeleted += (s, e) =>
            {
                Console.WriteLine("Delete a ts item: " + e.DeletedItem.EmployeeFullName);
            };

            Console.WriteLine("Case 1:");
            var ts1 = new TimeSheetItem() { EmployeeId = "123", EmployeeFullName = " Nguyen van A" };
            var ts2 = new TimeSheetItem() { EmployeeId = "456", EmployeeFullName = "Tran Be" };
            tsItems.AddNewTimeSheetItem(ts1);
            tsItems.AddNewTimeSheetItem(ts2);

            Console.WriteLine("Case 2:");
            tsItems.DeleteATimeSheetItem(ts2);

            Console.ReadLine();
        }
    }
}
