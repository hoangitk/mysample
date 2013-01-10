using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using TimeSheetControl;

namespace WindowsFormsApplication1
{
	public delegate object DynamicGetValue(object component);
	public delegate void DynamicSetValue(object component, object newValue);

	public class DynamicPropertyDescriptor : PropertyDescriptor 
    {
		protected Type _componentType;
		protected Type _propertyType;
		protected DynamicGetValue _getDelegate;
		protected DynamicSetValue _setDelegate;

		public DynamicPropertyDescriptor(Type componentType, string name, Type propertyType, DynamicGetValue getDelegate, DynamicSetValue setDelegate)
			:
			base(name, null) {
			_componentType = componentType;
			_propertyType = propertyType;
			_getDelegate = getDelegate;
			_setDelegate = setDelegate;
		}

		public override bool CanResetValue(object component) {
			return false;
		}

		public override Type ComponentType {
			get { return _componentType; }
		}

		public override object GetValue(object component) {
			return _getDelegate(component);
		}

		public override bool IsReadOnly {
			get { return _setDelegate == null; }
		}

		public override Type PropertyType {
			get { return _propertyType; }
		}

		public override void ResetValue(object component) {
		}

		public override void SetValue(object component, object value) {
			_setDelegate(component, value);
		}

		public override bool ShouldSerializeValue(object component) {
			return true;
		}
	}
}
