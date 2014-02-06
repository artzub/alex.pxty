using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db.Mapping;
using Db.DataAccess;

namespace Controller {
    public abstract class BaseController : IController {
        protected virtual IBaseMapper Mapper {
            get;
            set;
        }

        #region IController Members

        public object Delete(Db.IDomain item) {
            if (item == null)
                return null;
            return Mapper.DeleteById(item.Id);
        }

        public System.Data.DataTable GetDataInTable(Queries select = null) {
            return Mapper.GetAllInTable(select);
        }

        public abstract object Save(Db.IDomain item);

		public abstract object Update(Db.IDomain item);

        public abstract object GetNew();

        public abstract object GetItemById(object id);

        public abstract object AddItem(object item);
        public abstract void RemoveItem(object item);

        #endregion
    }
}
