using System.Collections.Generic;

namespace Db.Mapping {
	/// <summary>
	/// Позволяет обеспечить получение всех данных или поиска данных по идентификатору записи.
	/// </summary>
	/// <typeparam name="T">Тип, в котором возвращать данные</typeparam>
	public interface IMapper<T> : IBaseMapper {
		/// <summary>
		/// Получить все данные списоком
		/// </summary>
		/// <returns>возвращает список типа <see cref="T"></returns>
		IList<T> GetAll(/*ChangeStateDelegate statChanger = null*/);		

		/// <summary>
		/// Получить запись по идентификатору
		/// </summary>
		/// <param name="id">идентификатор</param>
        /// <returns>запись типа <see cref="T" /></returns>
		T FindById(object id);
	}
}
