using Db.Mapping;
using Db.Domains;
using Db.DataAccess;
using System;

namespace Controller {
    public class DepartamentController : Controller<Departament> {
        private DepartamentMapper mapper;

        public DepartamentController()
            : base() {
                mapper = new DepartamentMapper();
                base.Mapper = mapper;
        }

        protected override object ChangeRow(Departament item, string procedureName) {
            if (item == null 
                || (item.TypeDep == null && item.TypeDep.Id == null))
                return null;

            var builder = new StoredProsedureStatementBuilder(procedureName);
			builder.AddParameter("new_id_type_dep", item.TypeDep.Id);
            builder.AddParameter("new_num", item.Num);
			builder.AddParameter("old_id", Convert.ToInt64(item.Id ?? 0));
			builder.AddParameter("new_ID", Convert.ToInt64(0), 32, System.Data.ParameterDirection.InputOutput);

            return Provider.DatabaseGateway.StoredProcedureExcecut(builder, "new_ID");
        }
    }
}
