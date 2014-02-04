using Db;
using Db.Domains;
using System;

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
			var st = Hashes.GetStageById(cols.Id);

			if (st != null)
				return st;
            
			var part = new PartMapper().FindById(cols.IdPart);
            var surface = new SurfaceMapper().FindById(cols.IdSurface);
			var dep = new DepartamentMapper().FindById(cols.IdDepartament);

			st = new Stage(cols.Id, null, null, dep, surface, part);

			Hashes.StagesHash[st.Id] = st;

			st.StagePrev = Hashes.GetStageById(cols.IdStagePrev) ?? new StageMapper().FindById(cols.IdStagePrev ?? 1);
			st.StageNext = Hashes.GetStageById(cols.IdStageNext) ?? new StageMapper().FindById(cols.IdStageNext ?? 1);

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
