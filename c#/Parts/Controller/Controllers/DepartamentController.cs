using Db.Mapping;
using Db.Domains;
using Db.DataAccess;

namespace Controller {
    public class DepartamentController : Controller<Departament> {
        private DepartamentMapper mapper;

        public DepartamentController()
            : base() {
                mapper = new DepartamentMapper();
                base.Mapper = (IMapper<Departament>)mapper;
        }

        protected override object ChangeRow(Departament item, string procedureName) {
            if (item == null 
                || (item.TypeDep == null && item.IdTypeDep == null))
                return null;

            var builder = new StoredProsedureStatementBuilder(procedureName);
            builder.AddParameter("new_id_typedep", item.IdTypeDep ?? item.TypeDep.Id);
            builder.AddParameter("new_num", item.Num);
            builder.AddParameter("old_id", item.Id);
            builder.AddParameter("new_ID", null, 32, System.Data.ParameterDirection.InputOutput);

            return new DatabaseGateway().StoredProcedureExcecut(builder, "new_ID");
        }
    }
}
