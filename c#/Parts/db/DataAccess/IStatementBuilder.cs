using System.Collections.Generic;

namespace Db.DataAccess {
	public interface IStatementBuilder {
		IList<Parameter> Parameters {
			get;
		}

		string TableName {
			get;
		}
	}
}
