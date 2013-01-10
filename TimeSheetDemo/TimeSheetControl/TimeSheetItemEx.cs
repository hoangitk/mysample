using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TimeSheetControl;

namespace TimeSheetControl
{
    public class TimeSheetItemDescriptionProvider : TypeDescriptionProvider
    {
        private static TypeDescriptionProvider defaultTypeProvider = 
                   TypeDescriptor.GetProvider(typeof(TimeSheetItem));

        public TimeSheetItemDescriptionProvider()
            : base(defaultTypeProvider)
        {
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, 
                                                                object instance)
        {
            ICustomTypeDescriptor defaultDescriptor = 
                                  base.GetTypeDescriptor(objectType, instance);

            return instance == null ? defaultDescriptor : 
                new TimeSheetItemCustomTypeDescriptor(defaultDescriptor, instance);
        }
    }

    public class TimeSheetItemCustomTypeDescriptor : CustomTypeDescriptor
    {
        public TimeSheetItemCustomTypeDescriptor(ICustomTypeDescriptor parent, object instance)
        : base(parent)
        {
            TimeSheetItem tsItem = (TimeSheetItem)instance;

            //customFields.AddRange(CustomFieldsGenerator.GenerateCustomFields(title.Category)
            //    .Select(f => new CustomFieldPropertyDescriptor(f)).Cast<PropertyDescriptor>());

            if (tsItem.TimeSheetDays != null && tsItem.TimeSheetDays.Count > 0)
            {
                foreach (var tsDay in tsItem.TimeSheetDays)
                {
                    customFields.Add(new TimeSheetItemPropertyDescriptor(tsDay.Day.ToString("yyyy_MM_dd")));
                }
            }
        }

        private List<PropertyDescriptor> customFields = new List<PropertyDescriptor>();

        public override PropertyDescriptorCollection GetProperties()
        {
            return new PropertyDescriptorCollection(base.GetProperties()
                .Cast<PropertyDescriptor>().Union(customFields).ToArray());
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(base.GetProperties(attributes)
                .Cast<PropertyDescriptor>().Union(customFields).ToArray());
        }
    }

    class TimeSheetItemPropertyDescriptor : PropertyDescriptor
    {
        public TimeSheetItemPropertyDescriptor(string name) : base(name, null)
        {

        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get { return typeof(TimeSheetItem); }
        }

        public override object GetValue(object component)
        {
            return ((TimeSheetItem)component)[Name];
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
            ((TimeSheetItem)component)[Name] = null;
        }

        public override void SetValue(object component, object value)
        {
            ((TimeSheetItem)component)[Name] = (TimeSheetDay)value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return ((TimeSheetItem)component)[Name] != null;
        }
    }

    #region CustomField
    //class CustomFieldPropertyDescriptor : PropertyDescriptor
    //{
    //    public CustomField CustomField { get; private set; }

    //    public CustomFieldPropertyDescriptor(CustomField customField)
    //        : base(customField.Name, new Attribute[0])
    //    {
    //        CustomField = customField;
    //    }

    //    public override bool CanResetValue(object component)
    //    {
    //        return false;
    //    }

    //    public override Type ComponentType
    //    {
    //        get
    //        {
    //            return typeof(Title);
    //        }
    //    }

    //    public override object GetValue(object component)
    //    {
    //        Title title = (Title)component;
    //        return title[CustomField.Name] ?? (CustomField.DataType.IsValueType ?
    //            (Object)Activator.CreateInstance(CustomField.DataType) : null);
    //    }

    //    public override bool IsReadOnly
    //    {
    //        get
    //        {
    //            return false;
    //        }
    //    }

    //    public override Type PropertyType
    //    {
    //        get
    //        {
    //            return CustomField.DataType;
    //        }
    //    }

    //    public override void ResetValue(object component)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void SetValue(object component, object value)
    //    {
    //        Title title = (Title)component;
    //        title[CustomField.Name] = value;
    //    }

    //    public override bool ShouldSerializeValue(object component)
    //    {
    //        return false;
    //    }

    //    class CustomField
    //    {
    //        public CustomField(String name, Type dataType)
    //        {
    //            Name = name;
    //            DataType = dataType;
    //        }

    //        public String Name { get; private set; }

    //        public Type DataType { get; private set; }
    //    }
    //} 
    #endregion
}
