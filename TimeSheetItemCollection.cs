using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeSheetControl
{
    public interface ITimeSheetItemCollection
    {
        event EventHandler<ItemAddedEventArgs> ItemAdded;
        event EventHandler<ItemDeletedEventArgs> ItemDeleted;
    }

    public class ItemAddedEventArgs : EventArgs
    {
        public TimeSheetItem AddedTimeSheetItem { get; set; }

        public ItemAddedEventArgs()
        {

        }

        public ItemAddedEventArgs(TimeSheetItem newItem)
        {
            this.AddedTimeSheetItem = newItem;
        }
    }

    public class ItemDeletedEventArgs : EventArgs
    {
        public TimeSheetItem DeletedItem { get; set; }

        public ItemDeletedEventArgs()
        {


        }

        public ItemDeletedEventArgs(TimeSheetItem deletedItem)
        {
            this.DeletedItem = deletedItem;
        }
    }

    public class TimeSheetItemCollection : List<TimeSheetItem>, ITimeSheetItemCollection
    {

        public void AddNewTimeSheetItem(TimeSheetItem newTsItem)
        {
            this.Add(newTsItem);
            OnItemAdded(newTsItem);
        }

        public void DeleteATimeSheetItem(TimeSheetItem deleteItem)
        {
            var existedItem = this.Where(ts => ts.EmployeeId == deleteItem.EmployeeId).FirstOrDefault();

            if (existedItem != null)
            {
                this.Remove(existedItem);
                OnItemDeleted(existedItem);
            }
        }

        #region ITimeSheetItemCollection Members

        public event EventHandler<ItemAddedEventArgs> ItemAdded;
        protected virtual void OnItemAdded(TimeSheetItem newItem)
        {
            var handler = ItemAdded;
            if (handler != null)
            {
                handler(this, new ItemAddedEventArgs(newItem));
            }
        }

        #endregion

        #region ITimeSheetItemCollection Members


        public event EventHandler<ItemDeletedEventArgs> ItemDeleted;
        protected virtual void OnItemDeleted(TimeSheetItem deletedItem)
        {
            var handler = ItemDeleted;
            if (handler != null)
            {
                handler(this, new ItemDeletedEventArgs(deletedItem));
            }
        }

        #endregion
    }
}
