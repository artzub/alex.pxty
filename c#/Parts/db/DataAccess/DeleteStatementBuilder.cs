using System.Text;

namespace Db.DataAccess {
    public class DeleteStatementBuilder : Db.DataAccess.StatementBuilder {
        private string where = string.Empty;

		public DeleteStatementBuilder(string tableName, string paramPrefix = "", string where = "") :
			base(tableName, paramPrefix) {
			this.where = where;
		}

		public string WHERE {
			get {
                if (string.IsNullOrWhiteSpace(where) && parameters.Count > 0)
                    where = MakeWhere();
                else
                    where = string.Empty;
				return where;
			}
		}

        private string MakeWhere() {
            var result = string.Empty;

            result = GetParameters("and");

            return result;
        }

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("Delete from {0} {1}", TableName, !System.String.IsNullOrWhiteSpace(WHERE) ? "WHERE " + where : string.Empty);

			return builder.ToString();
		}
    }
}
