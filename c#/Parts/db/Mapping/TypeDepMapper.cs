using Db.Domains;

namespace Db.Mapping {
    public class TypeDepMapper : Mapper<ITypeDep> {
        private const string tableName = "TYPEDEP"; //TODO: correct name

        public TypeDepMapper(Db.DataAccess.Queries select)
            : base(tableName, select: select) {
        }

        public TypeDepMapper(string sqlGetAll = default(string))
            : base(tableName, sqlGetAll) {
        }

        protected override ITypeDep CreateItemFromRow(System.Data.DataRow row) {
            if (row == null)
                return null;
            var cols = new DomainNamedColumnsWrapper(row);
            return new TypeDep(cols.Id, cols.Name);
        }
    }
}
