using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using TimeSheetControl;

namespace ComplexDataBinding
{
	/// <summary>
	/// We use array list so we dont have to write all those
	/// boring methods ( Count, Add(), RemoveAt() ...)
	/// </summary>
	[TypedCollection(typeof(TimeSheetItem))]
	public class MyCollection : ArrayList, IBindingList , ITypedList
	{
		public MyCollection()
		{
			// discover the type inside the collections - this is jsut for fun.
			// could have been passed on the constructor
			foreach ( Attribute a in this.GetType().GetCustomAttributes(typeof(TypedCollectionAttribute),false) )
			{
				if ( a is TypedCollectionAttribute )
				{
					this.finalType = ((TypedCollectionAttribute)a).CollectionType;
					break;
				}
			}
			if ( this.finalType == null )
				throw new Exception("Must supply the type as a TypedCollectionAttibute of the class");
		}

		/// <summary>
		/// The type inside the collection
		/// </summary>
		private Type finalType = null;

		#region IBindingList

		private ListChangedEventArgs resetEvent = new ListChangedEventArgs(ListChangedType.Reset, -1);
		private ListChangedEventHandler onListChanged;

		protected virtual void OnListChanged(ListChangedEventArgs ev) 
		{
			if (onListChanged != null) 
			{
				onListChanged(this, ev);
			}
		}

		event ListChangedEventHandler IBindingList.ListChanged 
		{
			add 
			{
				onListChanged += value;
			}
			remove 
			{
				onListChanged -= value;
			}
		}
		
		bool IBindingList.AllowEdit 
		{ 
			get { return true ; }
		}

		bool IBindingList.AllowNew 
		{ 
			get { return true ; }
		}

		bool IBindingList.AllowRemove 
		{ 
			get { return true ; }
		}

		bool IBindingList.SupportsChangeNotification 
		{ 
			get { return false ; }
		}
        

		bool IBindingList.SupportsSearching 
		{ 
			get { return true ; }
		}

		bool IBindingList.SupportsSorting 
		{ 
			get { return true ; }
		}

		object IBindingList.AddNew() 
		{
			return finalType.GetConstructor(new Type[]{}).Invoke(null);
		}

		private bool isSorted = false;

		bool IBindingList.IsSorted 
		{ 
			get { return isSorted; }
		}

		private ListSortDirection listSortDirection = ListSortDirection.Ascending;
		
		ListSortDirection IBindingList.SortDirection 
		{ 
			get { return listSortDirection; }
		}

		PropertyDescriptor sortProperty = null;

		PropertyDescriptor IBindingList.SortProperty 
		{ 
			get { return sortProperty; }
		}

		void IBindingList.AddIndex(PropertyDescriptor property) 
		{
			isSorted = true;
			sortProperty = property;
		}

		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction) 
		{
			isSorted = true;
			sortProperty = property;
			listSortDirection = direction;

			ArrayList a = new ArrayList();

			this.Sort( new ObjectPropertyComparer(property.Name));
			if (direction == ListSortDirection.Descending) this.Reverse();
		}

		int IBindingList.Find(PropertyDescriptor property, object key) 
		{
			foreach( object o in this)
			{
				if ( Match( finalType.GetProperty(property.Name).GetValue(o,null) , key) ) 
					return this.IndexOf(o);
			}
			return -1;
		}

		void IBindingList.RemoveIndex(PropertyDescriptor property) 
		{
			sortProperty = null;
		}

		void IBindingList.RemoveSort() 
		{
			isSorted = false;
			sortProperty = null;
		}

		#endregion

		#region ITypedList
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			ArrayList input = null ;
			ArrayList output = new ArrayList();

			if ( listAccessors != null && listAccessors.Length > 0)
			{
				// if an listAccessors is suppled, we return the 
				// properties for the LAST one - dont ask me why - 
				// I found it in the sourse code for 
				// DataView.ITypedList.GetItemProperties using a 
				// decompiler

				PropertyDescriptor childProperty = listAccessors[listAccessors.Length - 1];

				Type t = null;

				foreach ( Attribute a in childProperty.Attributes )
				{
					if ( a is TypedCollectionAttribute )
					{
						t = ((TypedCollectionAttribute)a).CollectionType;
						break;
					}
				}

				if ( t != null )
					input = new ArrayList(TypeDescriptor.GetProperties(t));
			}
			else
			{
				input = new ArrayList(TypeDescriptor.GetProperties(finalType));
			}

			return GetPropertyDescriptorCollection(input);
		}

		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			string name = "";

			if ( listAccessors != null )
			{
				foreach ( PropertyDescriptor p in listAccessors )
				{
					name += p.PropertyType.Name + "_";
				}
				name = name.TrimEnd('_');
			}
			else
				name = this.GetType().Name;

			return name;
		}
		#endregion

		#region Helper functions

		protected PropertyDescriptorCollection GetPropertyDescriptorCollection( ArrayList properties )
		{
			if ( properties == null || properties.Count == 0 )
				return new PropertyDescriptorCollection(null);

			ArrayList output = new ArrayList();

			foreach ( PropertyDescriptor p in properties )
			{
				if ( p.Attributes.Matches(new Attribute[]{new BindableAttribute(false)}) ) continue;

				if ( p.PropertyType.Namespace == "System.Data.SqlTypes" )
				{
					// create the base type property descriptor
					output.Add(TimeSheetItemPropertyDescriptor.GetProperty( p.Name, p.PropertyType ) );
				}
				else
				{
					output.Add(p);
				}
			}
			return new PropertyDescriptorCollection((PropertyDescriptor[])output.ToArray(typeof(PropertyDescriptor)));
		}

		protected bool Match( object data, object searchValue )
		{
			// handle nulls
			if ( data == null || searchValue == null )
			{
				return (bool)(data == searchValue);
			}

			// if its a string, our comparisons should be 
			// case insensitive.
			bool IsString = (bool)(data is string);
			

			// bit of validation b4 we start...
			if ( data.GetType() != searchValue.GetType() )
				throw new ArgumentException("Objects must be of the same type");

			if ( ! (data.GetType().IsValueType || data is string ) )
				throw new ArgumentException("Objects must be a value type");

			

			/*
			 * Less than zero a is less than b. 
			 * Zero a equals b. 
			 * Greater than zero a is greater than b. 
			 */

			if ( IsString )
			{
				string stringData = ((string)data).ToLower(CultureInfo.CurrentCulture);
				string stringMatch = ((string)searchValue).ToLower(CultureInfo.CurrentCulture);

				return (bool)(stringData == stringMatch);			
			}		
			else
			{
				return (bool)(Comparer.Default.Compare(data,searchValue) == 0 );			
			}
		}

		#endregion
	}
}
