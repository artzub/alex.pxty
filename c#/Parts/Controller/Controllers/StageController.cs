using Db.Mapping;
using Db.Domains;
using Db.DataAccess;

namespace Controller {
    public class StageController : Controller<Stage> {
       private StageMapper mapper;

        public StageController()
            : base() {
                mapper = new StageMapper();
                base.Mapper = (IMapper<Stage>)mapper;
        }

        protected override object ChangeRow(Stage item, string procedureName) {
            if (item == null 
                || (item.Part == null && item.IdPart == null)
                || (item.Surface == null && item.IdSurface == null))
                return null;

            var builder = new StoredProsedureStatementBuilder(procedureName);
            builder.AddParameter("new_id_part", item.IdPart ?? item.Part.Id);
            builder.AddParameter("new_id_surface", item.IdSurface ?? item.Surface.Id);
            builder.AddParameter("new_id_stagenext", item.IdStageNext ?? (item.StageNext ?? Stage.Default).Id);
            builder.AddParameter("new_id_stageprev", item.IdStagePrev ?? (item.StagePrev ?? Stage.Default).Id);
            builder.AddParameter("old_id", item.Id);
            builder.AddParameter("new_ID", null, 32, System.Data.ParameterDirection.InputOutput);

            return new DatabaseGateway().StoredProcedureExcecut(builder, "new_ID");
        }
    }
}
