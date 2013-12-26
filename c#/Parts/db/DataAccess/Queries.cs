using System.Text;

namespace db.DataAccess {	

	public class Queries {
		
		private string selectSegment;
		private string whereSegment;
		private string groupBySegment;
		private string orderBySegment;

		public Queries(string selectSegment, string whereSegment="",
			string groupBySegment="", string orderBySegment="") {
				this.selectSegment = selectSegment;
				this.whereSegment = whereSegment;
				this.groupBySegment = groupBySegment;
				this.orderBySegment = orderBySegment;
		}

		public string GetSelect() {
			StringBuilder selectResult = new StringBuilder();
			selectResult.Append(selectSegment);
			if (whereSegment != string.Empty)
				selectResult.Append(" " + whereSegment);
			if (groupBySegment != string.Empty)
				selectResult.Append(" " + groupBySegment);
			if (orderBySegment != string.Empty)
				selectResult.Append(" " + orderBySegment);
			return selectResult.ToString();
		}

		public static string SqlGetAll(string tableName) {
			return string.Format("SELECT * FROM {0}", tableName);
		}

		public static string SqlGetById(string tableName, object Id) {
			if (Id is decimal || Id is int || Id is double || Id is long)
				return string.Format("SELECT * FROM {0} WHERE ID={1}", tableName, Id);

			if (Id is string)
				return string.Format("SELECT * FROM {0} WHERE ID='{1}'", tableName, Id);

			return null;
		}
	}
}
