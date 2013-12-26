using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using db.Domains;

namespace db
{
	public class Connection
	{
		/*OracleConnection t;
		public IList<INamed> getSurface ()
		{
			var list = new List<INamed>();

			var r = t.CreateCommand();
			r.CommandText = "select * from surface";
			var er = r.ExecuteReader ();
			while(er.Read())
				list.Add(new Surface(er["ID"], er["NAME"].ToString()));			
			return list;
		}

		public Connection ()				
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
		}*/
	}
}

