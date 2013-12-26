using System.Text;

namespace db.DataAccess {
	public class UpdateStatementBuilder : StatementBuilder {
		
		private string where = string.Empty;

		public UpdateStatementBuilder(string tableName, string paramPrefix = "", string where = "") :
			base(tableName, paramPrefix) {
			this.where = where;
		}

		public string WHERE {
			get {
				return where;
			}
		}		

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("UPDATE {0} SET {1} {2}", TableName, GetParameters(ParamPrefix), !System.String.IsNullOrWhiteSpace(where) ? "WHERE " + where : string.Empty);

			return builder.ToString();
		}

		private string GetParameters(string prefix) {
			StringBuilder builder = new StringBuilder();

			foreach (Parameter parameter in Parameters) {
				builder.AppendFormat("{0}={1}{0},", parameter.Name, prefix);
			}
			builder.Remove(builder.Length - 1, 1);

			return builder.ToString();
		}
	}
}
