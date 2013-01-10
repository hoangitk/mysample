using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TimeSheetControl;

namespace WindowsFormsApplication1
{
    public class TimeSheetDayCollection : List<TimeSheetDay>, ITypedList
    {
        public TimeSheetDay this[string day]
        {
            get
            {
                return this.Where(d => d.Day.ToString("yyyy_MM_dd") == day).SingleOrDefault();
            }

            set
            {                
                var item = this.Where(d => d.Day.ToString("yyy_MM_dd") == day).SingleOrDefault();
                if (item != null)
                    item = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors == null || listAccessors.Length == 0)
            {
                PropertyDescriptor[] props = new PropertyDescriptor[this.Count];
                for (int i = 0; i < props.Length; i++)
                {
                    props[i] = new TimeSheetDayPropertyDescriptor(this[i].Day.ToString("yyyy_MM_dd"));
                }
                return new PropertyDescriptorCollection(props, true);
            }
            throw new NotImplementedException("Relations not implement");
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return "TimeSheetDayCollection";
        }
    }

    public class TimeSheetDayPropertyDescriptor : PropertyDescriptor
    {
        public TimeSheetDayPropertyDescriptor(string name)
            : base(name, null)
        {

        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get { return typeof(TimeSheetDay); }
        }

        public override object GetValue(object component)
        {
            return component;
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get { return typeof(TimeSheetDay); }
        }

        public override void ResetValue(object component)
        {
            component = null;
        }

        public override void SetValue(object component, object value)
        {
            component = value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return component != null;
        }
    }
}
