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
            var builder = new DeleteStatementBuilder(Mapper.TableName, ":p");
            builder.AddParameter("ID", item.Id);
            return Provider.DatabaseGateway.DeleteRow(builder);
        }

        public System.Data.DataTable GetDataInTable(Queries select = null) {
            return Mapper.GetAllInTable(select);
        }

        public abstract object Save(Db.IDomain item);

        public abstract object Update(Db.IDomain item);

        public abstract object GetNew();

        #endregion
    }
}
