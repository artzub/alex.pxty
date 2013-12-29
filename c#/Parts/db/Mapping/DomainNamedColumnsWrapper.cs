using System.Data;

namespace Db.Mapping {
    public class DomainNamedColumnsWrapper : DomainColumnsWrapper {

        public DomainNamedColumnsWrapper(DataRow row) : base(row) {
        }

        public string Name {
            get {
                return string.Format("{0}", Row["NAME"]).Trim();
            }
        }
    }
}
