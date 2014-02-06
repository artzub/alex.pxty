using System;
using System.Data;

namespace Db.Mapping {
	public interface IBaseMapper {
        string TableName {
            get;
        }

		/// <summary>
		/// Получить все данные в таблице 
		/// </summary>
		/// <returns>таблица типом DataTable</returns>
        DataTable GetAllInTable(Db.DataAccess.Queries select = null);

		/// <summary>
		/// Получить запись по идентификатору
		/// </summary>
		/// <param name="id">идентификатор</param>
		/// <returns>запись типа <see cref="object"/></returns>
		object FindItemById(string id);

	    int DeleteById(object id);
	}
}
