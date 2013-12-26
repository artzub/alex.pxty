using System.Collections.Generic;
using System.Data;

namespace db.DataAccess {
	public abstract class StatementBuilder : IStatementBuilder {

		protected IDictionary<string, Parameter> parameters;
		private string tableName;
		private string paramPrefix = string.Empty;

		public IList<Parameter> Parameters {
			get {
				return new List<Parameter>(this.parameters.Values);
			}
		}

		public string TableName {
			get {
				return tableName;
			}
		}

		public string ParamPrefix {
			get {
				return paramPrefix;
			}
			set {
				paramPrefix = value;
			}
		}

		public StatementBuilder(string tableName, string paramPrefix = "") {
			this.tableName = tableName;
			this.parameters = new Dictionary<string, Parameter>();
			this.paramPrefix = paramPrefix;
		}

		public virtual void AddParameter(string name, object value) {
			parameters[name] = new Parameter(name, value);
		}

		public void AddParameter(string name, object value,
			int size, ParameterDirection direction = ParameterDirection.Input) {
			parameters[name] = new Parameter(name, value, size, direction);
		}

		public void AddParameter(string name, object value, ParameterDirection direction = ParameterDirection.Input) {
			parameters[name] = new Parameter(name, value, direction);
		}

		public override abstract string ToString();
	}
}
