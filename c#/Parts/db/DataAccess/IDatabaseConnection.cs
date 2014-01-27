using System;
using System.Data;

namespace Db.DataAccess {
	public interface IDatabaseConnection : IDisposable {
		void Close();
		void Open();
		IDbCommand CreateCommand(string sqlExpression);
		IDbConnection BaseConnection { get; }
	}
}
