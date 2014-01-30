using System;
using System.Data;
using Db.Domains;

namespace Db.Mapping {
    public class DepartamentMapper : Mapper<Departament> {

        private const string tableName = "Dep"; //TODO

        public DepartamentMapper(Db.DataAccess.Queries select)
            : base(tableName, select: select) {
        }

        public DepartamentMapper(string sqlGetAll = default(string))
            : base(tableName, sqlGetAll) {
        }

        protected override Departament CreateItemFromRow(System.Data.DataRow row) {
            if (row == null)
                return null;

            var cols = new ColumnsWrapper(row);

            var typeDep = new TypeDepMapper().FindById(cols.IdTypeDep);
            var query = string.Format("select * from stage where id_dep = {0}", cols.Id);
            return new Departament(cols.Id, cols.Num, typeDep, () => new System.ComponentModel.BindingList<Stage>(new StageMapper(query).GetAll()));
        }

        private sealed class ColumnsWrapper : DomainColumnsWrapper {
            public ColumnsWrapper(DataRow row) 
                : base(row) {
            }

            public long Num {
                get {
                    return Row.IsNull("NUM") ? 0 : Convert.ToInt64(Row["NUM"]);
                }
            }

            public object IdTypeDep {
                get {
                    return Row["id_type_dep"];
                }
            }
        }
    }
}
