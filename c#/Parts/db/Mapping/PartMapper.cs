using System;
using System.Data;
using Db.Domains;

namespace Db.Mapping {
    public class PartMapper : Mapper<Part> {

        private const string tableName = "PART";

        public PartMapper(Db.DataAccess.Queries select)
            : base(tableName, select: select) {
        }

        public PartMapper(string sqlGetAll = default(string))
            : base(tableName, sqlGetAll) {
        }

        protected override Part CreateItemFromRow(System.Data.DataRow row) {
            if (row == null)
                return null;
            var cols = new ColumnsWrapper(row);
			var alloy = Hashes.GetAlloyById(cols.IdAlloy) ?? new AlloyMapper().FindById(cols.IdAlloy);
            var query = string.Format("select * from stage where id_part = {0}", cols.Id);
            return Hashes.PartHash[cols.Id] = new Part(cols.Id,
                cols.Name,
                cols.Cost,
                cols.BLNumber,
                alloy,
                () => new System.ComponentModel.BindingList<Stage>(new StageMapper(query).GetAll()));
        }

        private sealed class ColumnsWrapper : DomainNamedColumnsWrapper {
            public ColumnsWrapper(DataRow row) : base (row) {
            }

            public object IdAlloy {
                get {
                    return Row["id_alloy"];
                }
            }

            public decimal Cost {
                get {
                    return Row.IsNull("Cost") ? 0 : Convert.ToDecimal(Row["Cost"]);
                }
            }

			public string BLNumber {
				get {
					return string.Format("{0}", Row ["BLNUM"]);
				}
			}
        }
    }
}
