using System;

namespace ComplexDataBinding
{
	/// <summary>
	/// Summary description for FakeData.
	/// </summary>
	public class FakeData
	{
		private FakeData(){}

		public static MyCollection GetData()
		{
			MyCollection collection = new MyCollection();
			
			for ( int i = 0; i < 10; i++ )
			{
				MyCollection iCollection = new MyCollection();
				MyData ii = new MyData();
				ii.ID = i;
				ii.FirstName = "aaa" + i.ToString();
				ii.LastName = "bbb" + i.ToString();
				
				ii.MyChildren = iCollection;
				collection.Add(ii);

				for ( int j = 0; j < 10; j++ )
				{
					MyCollection jCollection = new MyCollection();
					MyData jj = new MyData();
					jj.ID = j;
					jj.FirstName = "ccc" + j.ToString();
					jj.LastName = "ddd" + j.ToString();
				
					jj.MyChildren = jCollection;
					iCollection.Add(jj);

					for ( int k = 0; k < 10; k++ )
					{
						MyCollection kCollection = new MyCollection();
						MyData kk = new MyData();
						kk.ID = k;
						kk.FirstName = "eee" + k.ToString();
						kk.LastName = "fff" + k.ToString();
				
						// dont set
						//kk.MyChildren
						jCollection.Add(kk);
					}
				}
			}
			return collection;
		}
	}
}
