using System.Text;

namespace Db.DataAccess {
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
			builder.AppendFormat("UPDATE {0} SET {1} {2}", TableName, GetParameters(","), !System.String.IsNullOrWhiteSpace(where) ? "WHERE " + where : string.Empty);

			return builder.ToString();
		}
	}
}
