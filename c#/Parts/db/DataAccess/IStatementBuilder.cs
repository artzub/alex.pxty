using System.Collections.Generic;

namespace db.DataAccess {
	public interface IStatementBuilder {
		IList<Parameter> Parameters {
			get;
		}

		string TableName {
			get;
		}
	}
}
