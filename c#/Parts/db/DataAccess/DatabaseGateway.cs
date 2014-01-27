using System.Data;
using System;

namespace Db.DataAccess {
	public class DatabaseGateway {
		private IDatabaseConnection connection;
		public IDatabaseConnection Connection {
			get {
				return connection;
			}
		}

		public DatabaseGateway(IDatabaseConnection connection = null) {
			this.connection = connection;
		}

		public DataTable QueryForDataTable(string expression) {
			if (string.IsNullOrWhiteSpace(expression))
				return null;
			try {
				DataTable table = new DataTable();

				using (var reader = connection.CreateCommand(expression).ExecuteReader()) {
					table.Load(reader);
				}
				return table;
			}
			catch (System.Exception e) {
				if(e.InnerException == null)
					throw (e);
				throw (e.InnerException);
			}
		}

		public object ExecuteScalar(string expression) {
			try {
				IDbCommand command = connection.CreateCommand(expression);
				return command.ExecuteScalar();
			}
			catch (System.Exception e) {
				throw (e.InnerException);
			}
		}

		public int ExecuteStatement(string sql) {
			try {
				return connection.CreateCommand(sql).ExecuteNonQuery();
			}
			catch (System.Exception e) {
				throw (e.InnerException);
			}
		}

		public int ChangeRow(IStatementBuilder builder) {
			try {
				int result = 0;
				//IDbTransaction transaction = connection.BaseConnection.BeginTransaction(IsolationLevel.ReadCommitted);
				IDbCommand command = connection.CreateCommand(builder.ToString());
				foreach (Parameter parameter in builder.Parameters) {
					IDbDataParameter commandParameter = command.CreateParameter();
					commandParameter.ParameterName = parameter.Name;
					commandParameter.Direction = parameter.Direction;
					if (parameter.Size > 0)
						commandParameter.Size = parameter.Size;
					commandParameter.Value = parameter.Value;
					command.Parameters.Add(commandParameter);
				}
				try {
					result = command.ExecuteNonQuery();
					//transaction.Commit();
				}
				catch (System.Exception e) {
					//transaction.Rollback();
					throw (e);
				}
				finally {
					//transaction.Dispose();
				}
				return result;
			}
			catch (System.Exception e) {
				if (e.InnerException != null)
					throw new Exception(e.Message, e.InnerException);
				else
					throw  new Exception(e.Message, e);
			}
		}

		public int InsertRow(InsertStatementBuilder builder) {
			return ChangeRow(builder);
		}

		public int UpdateRow(UpdateStatementBuilder builder) {
			return ChangeRow(builder);
		}

        public int DeleteRow(DeleteStatementBuilder builder) {
            return ChangeRow(builder);
        }


		public object StoredProcedureExcecut(IStatementBuilder builder, string nameReturningParameter) {
			try {
				IDbCommand command = connection.CreateCommand(builder.TableName);
				command.CommandType = CommandType.StoredProcedure;

				foreach (Parameter parameter in builder.Parameters) {
					IDbDataParameter commandParameter = command.CreateParameter();
					commandParameter.ParameterName = parameter.Name;
					commandParameter.Direction = parameter.Direction;
					if (parameter.Size > 0)
						commandParameter.Size = parameter.Size;
					commandParameter.Value = parameter.Value;										
					command.Parameters.Add(commandParameter);
				}
				try {
					command.ExecuteNonQuery();
				}
				catch (System.Exception e) {
					throw (e);
				}
				finally {
				}
				return ((IDbDataParameter) command.Parameters[nameReturningParameter]).Value;
			}
			catch (System.Exception e) {
				if (e.InnerException != null)
					throw (e.InnerException);
				else
					throw (e);
			}
		}

		public bool TestConnection() {
			try {
				return connection.BaseConnection.State == ConnectionState.Open;
			}
			catch (System.Exception e) {					
				throw (e.InnerException);
			}					
		}
	}
}
