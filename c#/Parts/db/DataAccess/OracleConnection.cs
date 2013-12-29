using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System;

namespace db.DataAccess {
	public class OracleConnection : IDatabaseConnection {
		private DatabaseConnection dbconn;
		private string SettingSectionName = "DatabaseConnection";

		public void Close() {			
			dbconn.Close();
		}

		public void Open() {			
            try {
                dbconn.Open();
            }
            catch (Exception ex) {
                throw(new Exception(ex.Message, ex));
            }
		}

		public IDbCommand CreateCommand(string sqlExpression) {
			return dbconn.CreateCommand(sqlExpression);
		}

		public IDbConnection BaseConnection {
			get {
				return dbconn.BaseConnection;
			}
		}

		public void Dispose() {
			dbconn.Dispose();
		}

		private DatabaseConnection DatabaseConnection {
			get {
				return dbconn;
			}
		}

        public void Initialize(string connectionString) {
            try {
                dbconn = new DatabaseConnection(new System.Data.OracleClient.OracleConnection(connectionString));
            }
            catch (Exception) {
                
                throw;
            }
        }

		private OracleConnection() {
			try {
				SettingSectionName = this.GetType().Name;
				//dbconn = //new DataAccess.DatabaseConnection(SettingSectionName);
			}
			catch (Exception e) {
				throw (e);
			}
		}

        public static OracleConnection instance;
        public static OracleConnection Instance {
            get {
                if (instance == null)
                    instance = new OracleConnection();
                return instance;
            }
        }
	}
}
