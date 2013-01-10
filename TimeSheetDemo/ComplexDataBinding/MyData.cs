using System;

namespace ComplexDataBinding
{
	/// <summary>
	/// Summary description for MyData.
	/// </summary>
	public class MyData
	{
		private int id = 0;
		private string firstName = null;
		private string lastName = null;
		private MyCollection myChildren = null;

		public MyData()
		{
		}

		public int ID
		{
			get{ return id; }
			set{ id = value; }
		}
		public string FirstName
		{
			get{ return firstName; }
			set{ firstName = value; }
		}
		public string LastName
		{
			get{ return lastName; }
			set{ lastName = value; }
		}
		public MyCollection MyChildren
		{
			get{ return myChildren; }
			set{ myChildren = value; }
		}
	}
}
