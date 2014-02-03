namespace Controller
{
	public class ConnectionOraSetting {
	    public string User {
	        get; 
            set;
        }
		public string Pass {
			get;
			set;
		}

	    public string Host {
	        get;
	        set;
	    }

	    public string Port {
	        get;
	        set;
	    }

	    public string Service {
	        get;
	        set;
	    }

	    public override string ToString() {
			return /*"User ID=" + User + ";" +
								"Password=" + Pass + ";" +
										"Data Source=(" +
										"DESCRIPTION=(" +
										"ADDRESS=(PROTOCOL=TCP)(HOST="+ Host +")(PORT="+ Port +"))" +
										"(CONNECT_DATA=(SERVER=DEDICATED)" +
										"(SERVICE_NAME=" + Service + ")))";*/



				string.Format(
                "User ID={0};"
	            + "Password={1};"
	            + "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)"
	            + "(HOST={2})"
	            + "(PORT={3}))(CONNECT_DATA=(SERVER=DEDICATED)"
	            + "(SERVICE_NAME={4})))", 
                User,
                Pass,
                Host,
                Port,
                Service
	        );
	    }
	}
}

