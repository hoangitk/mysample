﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TimeSheetControl
{
    /// <summary>
    /// Extends the BindingList<T> to provide sorting and deep property
    /// binding (e.g. Address.Street).
    /// </summary>

    public class DeepBindingList<T> : BindingList<T>, ITypedList
    {        
        #region IBindingList overrides (to provide sorting)

        private PropertyDescriptor _sort;

        private ListSortDirection _direction;

        protected override bool IsSortedCore
        {
            get { return _sort != null; }
        }

        protected override void RemoveSortCore()
        {
            _sort = null;
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return _direction; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _sort; }
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override void ApplySortCore(PropertyDescriptor pd, ListSortDirection direction)
        {
            // get list to sort
            var items = this.Items as List<T>;

            // apply the sort
            if (items != null)
            {
                var pc = new PropertyComparer<T>(pd, direction);

                items.Sort(pc);
            }

            // save new settings and notify listeners
            _sort = pd;
            _direction = direction;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        // PropertyComparer (used to sort the list)
        private class PropertyComparer<TC> : IComparer<TC>
        {
            private PropertyDescriptor _pd;

            private ListSortDirection _direction;

            public PropertyComparer(PropertyDescriptor pd, ListSortDirection direction)
            {
                _pd = pd;

                _direction = direction;
            }

            public int Compare(TC x, TC y)
            {
                try
                {
                    var v1 = _pd.GetValue(x) as IComparable;

                    var v2 = _pd.GetValue(y) as IComparable;

                    int cmp =

                        v1 == null && v2 == null ? 0 :

                        v1 == null ? +1 :

                        v2 == null ? -1 :

                        v1.CompareTo(v2);

                    return _direction == ListSortDirection.Ascending ? +cmp : -cmp;
                }

                catch
                {
                    return 0; // comparison failed...
                }
            }
        }

        #endregion IBindingList overrides (to provide sorting)
        

        #region ITypedList (to expose inner properties)

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            var list = new List<PropertyDescriptor>();

            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(typeof(T)))
            {
                AddProperties(pd, null, list);
            }

            return new PropertyDescriptorCollection(list.ToArray());
        }

        private void AddProperties(PropertyDescriptor pd, PropertyDescriptor parent, List<PropertyDescriptor> list)
        {
            // add this property
            pd = new DeepPropertyDescriptor(pd, parent);

            list.Add(pd);

            // and subproperties for non-value types
            if (!pd.PropertyType.IsValueType && pd.PropertyType != typeof(string))
            {
                foreach (PropertyDescriptor sub in TypeDescriptor.GetProperties(pd.PropertyType))
                {
                    AddProperties(sub, pd, list);
                }
            }
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return null;
        }

        /// <summary>
        /// property descriptor with support for sub properties (e.g. Address.Street)
        /// </summary>
        private class DeepPropertyDescriptor : PropertyDescriptor
        {
            private PropertyDescriptor _pd;

            private PropertyDescriptor _parentPD;

            public DeepPropertyDescriptor(PropertyDescriptor pd, PropertyDescriptor parentPd)

                : base(pd.Name, null)
            {
                _pd = pd;

                _parentPD = parentPd;
            }

            public override string Name
            {
                get
                {
                    return _parentPD != null

                        ? string.Format("{0}.{1}", _parentPD.Name, _pd.Name)

                        : _pd.Name;
                }
            }

            public override bool IsReadOnly
            {
                get { return _pd.IsReadOnly; }
            }

            public override void ResetValue(object component)
            {
                _pd.ResetValue(component);
            }

            public override bool CanResetValue(object component)
            {
                return _pd.CanResetValue(component);
            }

            public override bool ShouldSerializeValue(object component)
            {
                return _pd.ShouldSerializeValue(component);
            }

            public override Type ComponentType
            {
                get { return _pd.ComponentType; }
            }

            public override Type PropertyType
            {
                get { return _pd.PropertyType; }
            }

            public override object GetValue(object component)
            {
                if (_parentPD != null)
                {
                    component = _parentPD.GetValue(component);
                }

                return _pd.GetValue(component);
            }

            public override void SetValue(object component, object value)
            {
                _pd.SetValue(_parentPD.GetValue(component), value);

                OnValueChanged(component, EventArgs.Empty);
            }
        }

        #endregion ITypedList (to expose inner properties)
    }
}