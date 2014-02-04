using Db.Mapping;
using Db.Domains;
using Db.DataAccess;
using System;

namespace Controller {
    public class StageController : Controller<Stage> {
       private StageMapper mapper;

        public StageController()
            : base() {
                mapper = new StageMapper();
                base.Mapper = mapper;
        }

        protected override object ChangeRow(Stage item, string procedureName) {
            if (item == null 
			    || (item.Departament == null && item.Departament.Id == null)
                || (item.Part == null && item.Part.Id == null)
			    || (item.Surface == null && item.Surface.Id == null))
                return null;

            var builder = new StoredProsedureStatementBuilder(procedureName);
			builder.AddParameter("new_id_dep", item.Departament.Id);
            builder.AddParameter("new_id_part", item.Part.Id);
            builder.AddParameter("new_id_surface", item.Surface.Id);
            builder.AddParameter("new_id_next", (item.StageNext ?? Stage.Default).Id);
            builder.AddParameter("new_id_prev", (item.StagePrev ?? Stage.Default).Id);
			builder.AddParameter("old_id", Convert.ToInt64(item.Id));
			builder.AddParameter("new_ID", Convert.ToInt64(0), 32, System.Data.ParameterDirection.InputOutput);

            return Provider.DatabaseGateway.StoredProcedureExcecut(builder, "new_ID");
        }
    }
}
