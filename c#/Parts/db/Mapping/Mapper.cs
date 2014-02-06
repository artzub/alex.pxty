using System;
using System.Collections.Generic;
using System.Data.Odbc;
using Db.DataAccess;

namespace Db.Mapping {
	public abstract class Mapper<T> : IMapper<T> {
		private string sqlGetAll;		
		private string tableName;

        public string TableName {
            get {
                return tableName;
            }
        }

		/// <summary>
		/// Запрос на получение всех данных их таблицы
		/// </summary>
		virtual public string SqlGetAll {
			get {
				return sqlGetAll;
			}
			set {
				if (!string.IsNullOrEmpty(value))
					sqlGetAll = value;
			}
		}

		protected Mapper(string tableName, string sqlGetAll = "", Queries select = null) {
			this.tableName = tableName;
			if (select != null) {
				sqlGetAll = select.GetSelect();
			}
			else if (string.IsNullOrWhiteSpace(sqlGetAll))
				sqlGetAll = Queries.SqlGetAll(tableName);
			
			this.sqlGetAll = sqlGetAll;
		}		

		/// <summary>
		/// Получить все данные списоком
		/// </summary>
		/// <returns>возвращает список типа <see cref="T"/></returns>
		virtual public IList<T> GetAll(/*ChangeStateDelegate stateChanger = null*/) {
            return CreateListOfItems(Provider.DatabaseGateway.QueryForDataTable(SqlGetAll)/*, stateChanger*/);
		}

		/// <summary>
		/// Получить все данные в таблице 
		/// </summary>
		/// <returns>таблица типом <see cref="System.Data.DataTable"/></returns>
		virtual public System.Data.DataTable GetAllInTable(Queries select = null) {
            var query = string.Empty;
            if (select != null)
                query = select.ToString();
            if (string.IsNullOrWhiteSpace(query))
                query = SqlGetAll;
            return Provider.DatabaseGateway.QueryForDataTable(query);
		}

		/// <summary>
		/// Создает список записей по типу T
		/// </summary>
		/// <param name="results">таблица содержащая записи</param>
		/// <returns>Список объектво типа T</returns>
		virtual protected IList<T> CreateListOfItems(System.Data.DataTable results/*, ChangeStateDelegate stateChanger = null*/) {
			IList<T> lists = new List<T>();

			if (results != null) {

				/*if (stateChanger != null)
					stateChanger(results.Rows.Count, true);*/

				foreach (System.Data.DataRow itemRow in results.Rows) {
					lists.Add(CreateItemFromRow(itemRow));
					/*if (stateChanger != null)
						stateChanger();*/
				}
			}

			return lists;
		}

		/// <summary>
		/// Получить запись по идентификатору
		/// </summary>
		/// <param name="id">идентификатор</param>
		/// <returns>запись типа <see cref="T"/></returns>
		virtual public T FindById(object id) {
			if (id == null)
				return default(T);
            var curTable = Provider.DatabaseGateway.QueryForDataTable(Queries.SqlGetById(tableName, id));
			return CreateItemFromRow(curTable != null && curTable.Rows.Count > 0 ? curTable.Rows[0] : null);
		}

		/// <summary>
		/// Абстрактный метод создающий элемент из записи
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		abstract protected T CreateItemFromRow(System.Data.DataRow row);

		protected string GetStringFromColumn(System.Data.DataRow row, string fieldName) {
			return row.IsNull(fieldName) ? string.Empty : row[fieldName].ToString().Trim();
		}

		public object FindItemById(string id) {
			return FindById(id);
		}

	    public virtual int DeleteById(object id) {
	        if (id == null)
	            return 0;
            var builder = new DeleteStatementBuilder(TableName, ":");
            builder.AddParameter("ID", id);
            var res = Provider.DatabaseGateway.DeleteRow(builder);
            return res;
	    }

	    protected virtual void AfterDelete(object id, int count) {
            if (count > 0)
                Hashes.Remove<T>(id);
	    }
	}
}
