using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;

namespace db
{
	public class MyClass
	{
		OracleConnection t;
		public IList<INamed> getSurface ()
		{
			var list = new List<INamed>();

			using (var r = t.CreateCommand()) {
				r.CommandText = "select * from surface";
				using(var er = r.ExecuteReader ()) 
					while(er.Read())
						list.Add(new Surface(er["ID"], er["NAME"].ToString()));
			}
			return list;
		}

		public MyClass ()				
		{
			var conn = "User ID=PARTS;" +
				"Password=Zelda;" +
				"Data Source=(" +
				"DESCRIPTION=(" +
				"ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))" +
				"(CONNECT_DATA=(SERVER=DEDICATED)" +
				"(SERVICE_NAME=XE)))";
			t = new OracleConnection (conn);
			t.Open ();
		}
	}
}

