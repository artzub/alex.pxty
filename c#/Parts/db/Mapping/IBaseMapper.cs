using System;
using System.Data;

namespace db.Mapping {
	public interface IBaseMapper {
		/// <summary>
		/// Получить все данные в таблице 
		/// </summary>
		/// <returns>таблица типом DataTable</returns>
		DataTable GetAllInTable();

		/// <summary>
		/// Получить запись по идентификатору
		/// </summary>
		/// <param name="id">идентификатор</param>
		/// <returns>запись типа <see cref="object"/></returns>
		object FindItemById(string id);
	}
}
