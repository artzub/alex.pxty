using Db.Domains;

namespace Db.Mapping {
    public class StageMapper : Mapper<Stage> {

        private const string tableName = "Stage"; //TODO

        public StageMapper(Db.DataAccess.Queries select)
            : base(tableName, select: select) {
        }

        public StageMapper(string sqlGetAll = default(string))
            : base(tableName, sqlGetAll) {
        }

        protected override Stage CreateItemFromRow(System.Data.DataRow row) {
            if (row == null)
                return null;

            var cols = new ColumnsWrapper(row);
            var part = new PartMapper().FindById(cols.IdPart);
            var surface = new SurfaceMapper().FindById(cols.IdSurface);
            var dep = new DepartamentMapper().FindById(cols.IdDepartament);

            var st = new Stage(cols.Id, null, null, dep, surface, part);

            st.StagePrev = cols.IdStagePrev.Equals(cols.Id) ? st : (cols.IdStagePrev.Equals(1) ? Stage.Default : (new StageMapper()).FindById(cols.IdStagePrev));
            st.StageNext = cols.IdStageNext.Equals(cols.Id) ? st : (cols.IdStageNext.Equals(1) ? Stage.Default : (new StageMapper()).FindById(cols.IdStageNext));

            return st;
        }

        private sealed class ColumnsWrapper : DomainColumnsWrapper {
            public ColumnsWrapper(System.Data.DataRow row) : base(row) {
            }

            public object IdStagePrev {
                get {
                    return Row["id_prev"];
                }
            }

            public object IdStageNext {
                get {
                    return Row["id_next"];
                }
            }

            public object IdSurface {
                get {
                    return Row["id_surface"];
                }
            }

            public object IdDepartament {
                get {
                    return Row["id_dep"];
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
