using db.Domains;

namespace db.Mapping {
    public class AlloyMapper : Mapper<IAlloy> {

        private const string tableName = "ALLOY";

        public AlloyMapper(db.DataAccess.Queries select) 
            : base(tableName, select: select) {
        }

        public AlloyMapper(string sqlGetAll = default(string)) 
            : base(tableName, sqlGetAll) {
        }

        protected override IAlloy CreateItemFromRow(System.Data.DataRow row) {
            if (row == null)
                return null;
            var cols = new DomainNamedColumnsWrapper(row);
            return new Alloy(cols.Id, cols.Name);
        }
    }
}
