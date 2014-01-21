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
            builder.AddParameter("old_id", cur.Id);
            builder.AddParameter("new_ID", null, 32, System.Data.ParameterDirection.InputOutput);

            return new DatabaseGateway().StoredProcedureExcecut(builder, "new_ID");
        }

        #region IController<T> Members

        public virtual object Save(T item) {
            return ChangeRow(item, Mapper.TableName + "_CHANGE_ITEM");
        }

        public virtual object Update(T item) {
            return ChangeRow(item, Mapper.TableName + "_CHANGE_ITEM");
        }

        public T GetNew(object id) {
            var ctor = typeof(T).GetConstructor(new Type[] { typeof(object) });
            return (T)ctor.Invoke(new object[] {id});
        }

        public ICollection<T> GetData(Db.DataAccess.Queries select = null) {
            return ((IMapper<T>)Mapper).GetAll();
        }

        #endregion

        public override object Save(IDomain item) {
            return Save((T)item);
        }

        public override object Update(IDomain item) {
            return Update((T)item);
        }

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
