using System;
using Db.Domains;

namespace Db.Mapping {
    public class AlloyMapper : Mapper<Alloy> {

        private const string tableName = "ALLOY";

        public AlloyMapper(Db.DataAccess.Queries select) 
            : base(tableName, select: select) {
        }

        public AlloyMapper(string sqlGetAll = default(string)) 
            : base(tableName, sqlGetAll) {
        }

        protected override Alloy CreateItemFromRow(System.Data.DataRow row) {
            if (row == null)
                return null;

            var cols = new DomainNamedColumnsWrapper(row);
            
            var query = string.Format("select * from part where id_alloy = {0}", cols.Id);

            return Hashes.AlloyHash[cols.Id] = new Alloy(cols.Id,
                cols.Name,
                () => new System.ComponentModel.BindingList<Part>(new PartMapper(query).GetAll()));
        }
    }
}
