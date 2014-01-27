using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Db.DataAccess {
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

        protected virtual string GetParameters(string separator) {
            StringBuilder builder = new StringBuilder();

            foreach (Parameter parameter in Parameters) {
                builder.AppendFormat(" {0}={1}{0} {2}", parameter.Name, ParamPrefix, separator);
            }
            builder.Remove(builder.Length - separator.Length, separator.Length);

            return builder.ToString();
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
