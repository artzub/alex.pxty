using System.Text;

namespace db.DataAccess {
	public class InsertStatementBuilder : StatementBuilder {		

		public InsertStatementBuilder(string tableName, string paramPrefix = "") :
			base(tableName, paramPrefix){			
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("INSERT INTO {0}({1}) VALUES({2})", TableName, GetParameterNames(string.Empty),
								 GetParameterNames(ParamPrefix));

			return builder.ToString();
		}

		private string GetParameterNames(string prefix) {
			StringBuilder builder = new StringBuilder();

			foreach (Parameter parameter in parameters.Values) {
				builder.AppendFormat("{0}{1},", prefix, parameter.Name);
			}
			builder.Remove(builder.Length - 1, 1);

			return builder.ToString();
		}		
	}
}
