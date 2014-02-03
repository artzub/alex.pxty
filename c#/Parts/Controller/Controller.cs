using System;
using System.Collections.Generic;
using System.Linq;
using Db.Mapping;
using Db;
using Db.DataAccess;

namespace Controller {
    public abstract class Controller<T> : BaseController, IController<T> where T : class, IDomain {

        protected virtual object ChangeRow(T item, string procedureName) {
            var cur = item as DomainNamed;
            if (cur == null || string.IsNullOrWhiteSpace(cur.Name))
                return null;

            var builder = new StoredProsedureStatementBuilder(procedureName);
            builder.AddParameter("new_name", cur.Name);
			builder.AddParameter("old_id", Convert.ToInt64(cur.Id ?? 0));
			builder.AddParameter("new_ID", Convert.ToInt64(0), 32, System.Data.ParameterDirection.InputOutput);

			return Provider.DatabaseGateway.StoredProcedureExcecut(builder, "new_ID");
        }

        #region IController<T> Members

		public override object Save(IDomain item) {
            return ChangeRow((T)item, Mapper.TableName + "_CHANGE_ITEM");
        }

		public override object Update(IDomain item) {
            return ChangeRow((T)item, Mapper.TableName + "_CHANGE_ITEM");
        }

        public T GetNew(object id) {
            var ctor = typeof(T).GetConstructor(new[] { typeof(object) });
            return ctor == null ? null : (T)ctor.Invoke(new [] {id});
        }

        public IList<T> GetData(Queries select = null) {
            return ((IMapper<T>)Mapper).GetAll();
        }

        private Dictionary<object, T> hash;
        private System.ComponentModel.BindingList<T> items;
        public IList<T> Items {
            get {
                if (items == null) {
                    hash = GetData().ToDictionary(x => x.Id);
                    items = new System.ComponentModel.BindingList<T>(hash.Values.ToList());
                }
                return items;
            }
        }

        public virtual T GetById(object id) {
            return ((IMapper<T>)Mapper).FindById(id);
        }

        public override object GetItemById(object id) {
            return GetById(id);
        }

        public T AddItem(T item) {
            if (item == null)
                return null;

            T value;

			if (hash.TryGetValue (item.Id, out value))
				item = value;
			else {
				hash [item.Id] = item;
				items.Add (item);
			}

            return item;
        }

        public void RemoveItem(T item) {
            if (item == null)
                return;

			T value;

			if (hash.TryGetValue (item.Id, out value)) {
				hash.Remove(item.Id);
				items.Remove (value);
			}
            
        }

        #endregion

        public override object GetNew() {
            return GetNew(null);
        }

        public override object AddItem(object item) {
            return AddItem((T)item);
        }

		public override void RemoveItem (object item) {
			RemoveItem((T)item);
		}
    }
}
