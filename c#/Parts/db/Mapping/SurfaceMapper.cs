using Db.Domains;

namespace Db.Mapping {
    public class SurfaceMapper : Mapper<ISurface> {

        private const string tableName = "SURFACE";

        public SurfaceMapper(Db.DataAccess.Queries select)
            : base(tableName, select: select) {
        }

        public SurfaceMapper(string sqlGetAll = default(string))
            : base(tableName, sqlGetAll) {
        }

        protected override ISurface CreateItemFromRow(System.Data.DataRow row) {
            if (row == null)
                return null;
            var cols = new DomainNamedColumnsWrapper(row);
            return new Surface(cols.Id, cols.Name);
        }
    }
}
