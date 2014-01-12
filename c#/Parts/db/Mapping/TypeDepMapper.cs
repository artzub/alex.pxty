using Db.Domains;

namespace Db.Mapping {
    public class TypeDepMapper : Mapper<TypeDep> {
        private const string tableName = "TYPE_DEP"; //TODO: correct name

        public TypeDepMapper(Db.DataAccess.Queries select)
            : base(tableName, select: select) {
        }

        public TypeDepMapper(string sqlGetAll = default(string))
            : base(tableName, sqlGetAll) {
        }

        protected override TypeDep CreateItemFromRow(System.Data.DataRow row) {
            if (row == null)
                return null;
            var cols = new DomainNamedColumnsWrapper(row);
            var query = string.Format("select d.* from dep d where d.id_type_dep = {0}", cols.Id);
            return new TypeDep(cols.Id, cols.Name, () => {
                return new System.Collections.Generic.HashSet<Departament>(new DepartamentMapper(query).GetAll());
            });
        }
    }
}
