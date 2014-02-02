using Db.DataAccess;
using Db.Mapping;
using Db.Domains;
using System;

namespace Controller {
    public class PartController : Controller<Part> {
        private PartMapper mapper;

        public PartController()
            : base() {
                mapper = new PartMapper();
                base.Mapper = mapper;
        }

        protected override object ChangeRow(Part item, string procedureName) {
            if (item == null 
                || (item.Alloy == null && item.IdAlloy == null)
                || (string.IsNullOrWhiteSpace(item.Name)))
                return null;

            var builder = new StoredProsedureStatementBuilder(procedureName);
            builder.AddParameter("new_id_alloy", Convert.ToInt64(item.IdAlloy ?? item.Alloy.Id));
            builder.AddParameter("new_name", item.Name);
			builder.AddParameter("new_blnum", item.BLNumber);
            builder.AddParameter("new_cost", item.Cost);
			builder.AddParameter("old_id", Convert.ToInt64(item.Id));
			builder.AddParameter("new_ID", Convert.ToInt64(null), 32, System.Data.ParameterDirection.InputOutput);

            return Provider.DatabaseGateway.StoredProcedureExcecut(builder, "new_ID");
        }
    }
}
