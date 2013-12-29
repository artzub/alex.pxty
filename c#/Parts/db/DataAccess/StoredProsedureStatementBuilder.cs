using System.Text;

namespace Db.DataAccess {
	public class StoredProsedureStatementBuilder : StatementBuilder {

		public StoredProsedureStatementBuilder(string storedProcedureName, string paramPrefix = "")
			: base(storedProcedureName, paramPrefix) {
		}

		private string GetParameterNames(string prefix) {
			StringBuilder builder = new StringBuilder();

			foreach (Parameter parameter in Parameters) {
				builder.AppendFormat("{0}{1},", prefix, parameter.Name);
			}
			builder.Remove(builder.Length - 1, 1);

			return builder.ToString();
		}

		public override string ToString() {
			return TableName + "(" + GetParameterNames("") + ")";
		}
	}
}
