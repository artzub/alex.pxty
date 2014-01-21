using System.Text;

namespace Db.DataAccess {
	public class StoredProsedureStatementBuilder : StatementBuilder {

		public StoredProsedureStatementBuilder(string storedProcedureName, string paramPrefix = ":")
			: base(storedProcedureName, paramPrefix) {
		}

		private string GetParameterNames(string prefix) {
			StringBuilder builder = new StringBuilder();

			foreach (Parameter parameter in Parameters) {
				if (builder.Length > 0)
					builder.Append(",");
				builder.AppendFormat("{1}=>{0}{1}", prefix, parameter.Name);
			}

			return builder.ToString();
		}

		public override string ToString() {
			return "begin " + TableName + "(" + GetParameterNames(ParamPrefix) + "); end;";
		}
	}
}
