using System;
using db.DataAccess;
using db.Mapping;

namespace consoleClient
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var conn = "User ID=PARTS;" +
				"Password=Zelda;" +
					"Data Source=(" +
					"DESCRIPTION=(" +
					"ADDRESS=(PROTOCOL=TCP)(HOST=172.22.3.128)(PORT=1521))" +
					"(CONNECT_DATA=(SERVER=DEDICATED)" +
					"(SERVICE_NAME=XE)))";
			OracleConnection.Instance.Initialize(conn);
			Provider.Initialize(OracleConnection.Instance);
			OracleConnection.Instance.Open ();

			var list = new SurfaceMapper().GetAll();
			foreach (var item in list) 
				Console.WriteLine (item);
			OracleConnection.Instance.Close ();
		}
	}
}
