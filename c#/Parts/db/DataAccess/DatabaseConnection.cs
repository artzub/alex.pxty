using System.Configuration;
using System.Data;
using System.Data.Common;
using System;

namespace db.DataAccess {
	public class DatabaseConnection : IDatabaseConnection {
		private ConnectionInformation connectionInformation;
		private IDbConnection baseConnection;
		private string settingSectionName;
		public string SettingSectionName {
			get {
				return settingSectionName;
			}
			set {
				if (!string.IsNullOrWhiteSpace(value))
					settingSectionName = value;
			}
		}

		private class ConnectionInformation {
			private DbProviderFactory providerFactory;
			private ConnectionStringSettings settings;

			public ConnectionInformation(ConnectionStringSettings settings) {
				if (settings == null)
					throw new Exception("Не найдено настроек подключения для объекта.");
				this.providerFactory = DbProviderFactories.GetFactory(settings.ProviderName);
				this.settings = settings;
			}

			public IDbConnection CreateConnection() {
				IDbConnection connection = providerFactory.CreateConnection();
				if (settings.ConnectionString == string.Empty)
					throw new Exception("Парамметр connectionString пуст!");
				connection.ConnectionString = settings.ConnectionString;
				return connection;
			}
		}

		public DatabaseConnection(string settingSectionName) {
			try {
				SettingSectionName = settingSectionName;
				InitializeConnectionInformation();
				OpenBaseConnection();
			}
			catch (Exception e) {
				throw (e);
			}
		}

		private void OpenBaseConnection() {			
			try {
				baseConnection = connectionInformation.CreateConnection();				
			}
			catch (Exception e) {
				throw (e);
			}
		}

		public void Close() {
			baseConnection.Close();
		}

		public void Open() {
			baseConnection.Open();
		}

		private void InitializeConnectionInformation() {
			connectionInformation =
				new ConnectionInformation(
					ConfigurationManager.ConnectionStrings[
						ConfigurationManager.AppSettings[settingSectionName]]);
		}

		public virtual void Dispose() {
			Close();
			baseConnection.Dispose();
			baseConnection = null;
		}

		public IDbConnection BaseConnection {
			get {
				return baseConnection;
			}
		}

		public IDbCommand CreateCommand(string sqlExpression) {
			IDbCommand command = baseConnection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = sqlExpression;
			return command;
		}
	}
}
