using db.Domains;

namespace db.Mapping {
    public class StageMapper : Mapper<IStage> {

        private const string tableName = "Stage"; //TODO

        public StageMapper(db.DataAccess.Queries select)
            : base(tableName, select: select) {
        }

        public StageMapper(string sqlGetAll = default(string))
            : base(tableName, sqlGetAll) {
        }

        protected override IStage CreateItemFromRow(System.Data.DataRow row) {
            if (row == null)
                return null;

            var cols = new ColumnsWrapper(row);
            var part = new PartMapper().FindById(cols.IdPart);
            var surface = new SurfaceMapper().FindById(cols.IdSurface);
            var dep = new DepartamentMapper().FindById(cols.IdDepartament);
            var stp = new StageMapper().FindById(cols.IdStagePrev);
            var stn = new StageMapper().FindById(cols.IdStageNext);
            return new Stage(cols.Id, stp, stn, dep, surface, part);
        }

        private sealed class ColumnsWrapper : DomainColumnsWrapper {
            public ColumnsWrapper(System.Data.DataRow row) : base(row) {
            }

            public object IdStagePrev {
                get {
                    return Row["id_stag_prev"];
                }
            }

            public object IdStageNext {
                get {
                    return Row["id_stag_next"];
                }
            }

            public object IdSurface {
                get {
                    return Row["id_surface"];
                }
            }

            public object IdDepartament {
                get {
                    return Row["id_departament"];
                }
            }

            public object IdPart {
                get {
                    return Row["id_part"];
                }
            }
        }
    }
}
