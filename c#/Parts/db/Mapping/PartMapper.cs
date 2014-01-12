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
            var alloy = new AlloyMapper().FindById(cols.IdAlloy);
            return new Part(cols.Id, cols.Name, cols.Cost, alloy);
        }

        private sealed class ColumnsWrapper : DomainNamedColumnsWrapper {
            public ColumnsWrapper(DataRow row) : base (row) {
            }

            public object IdAlloy {
                get {
                    return Row["id_alloy"];
                }
            }

            public long Cost {
                get {
                    return Row.IsNull("Cost") ? 0 : Convert.ToInt64(Row["COST"]);
                }
            }
        }
    }
}
