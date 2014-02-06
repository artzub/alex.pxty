using Db.Domains;

namespace Db.Mapping {
    public class SurfaceMapper : Mapper<Surface> {

        private const string tableName = "SURFACE";

        public SurfaceMapper(Db.DataAccess.Queries select)
            : base(tableName, select: select) {
        }

        public SurfaceMapper(string sqlGetAll = default(string))
            : base(tableName, sqlGetAll) {
        }

        protected override Surface CreateItemFromRow(System.Data.DataRow row) {
            if (row == null)
                return null;
            var cols = new DomainNamedColumnsWrapper(row);
            var query = string.Format("select st.* from stage st where st.id_surface = {0}", cols.Id);
            return Hashes.SurfaceHash[cols.Id] = new Surface(cols.Id,
                cols.Name,
                () => new System.ComponentModel.BindingList<Stage>(new StageMapper(query).GetAll()));
        }
    }
}
