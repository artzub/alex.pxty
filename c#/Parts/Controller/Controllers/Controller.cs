using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db.Mapping;
using Db;
using Db.DataAccess;

namespace Controller {
    public abstract class Controller<T> : BaseController, IController<T> where T : class {


        public Controller() : base() {
        }

        protected virtual object ChangeRow(T item, string procedureName) {
            var cur = item as DomainNamed;
            if (cur == null || string.IsNullOrWhiteSpace(cur.Name))
                return null;

            var builder = new StoredProsedureStatementBuilder(procedureName);
            builder.AddParameter("new_name", cur.Name);
			builder.AddParameter("old_id", Convert.ToInt64(cur.Id ?? 0));
			builder.AddParameter("new_ID", Convert.ToInt64(0), 32, System.Data.ParameterDirection.InputOutput);

			return Provider.DatabaseGateway.StoredProcedureExcecut(builder, "new_ID");;
        }

        #region IController<T> Members

		public override object Save(Db.IDomain item) {
            return ChangeRow((T)item, Mapper.TableName + "_CHANGE_ITEM");
        }

		public override object Update(Db.IDomain item) {
            return ChangeRow((T)item, Mapper.TableName + "_CHANGE_ITEM");
        }

        public T GetNew(object id) {
            var ctor = typeof(T).GetConstructor(new Type[] { typeof(object) });
            return (T)ctor.Invoke(new object[] {id});
        }

        public ICollection<T> GetData(Db.DataAccess.Queries select = null) {
            return ((IMapper<T>)Mapper).GetAll();
        }

        #endregion

        public override object GetNew() {
            return GetNew(null);
        }

        #region IController<T> Members


        public virtual T GetById(object id) {
            return ((IMapper<T>)Mapper).FindById(id);
        }

        public override object GetItemById(object id) {
            return GetById(id);
        }

        #endregion
    }
}
