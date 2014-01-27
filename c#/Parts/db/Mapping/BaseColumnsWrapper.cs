using System.Data;

namespace Db.Mapping {
    public class BaseColumnsWrapper {
        public BaseColumnsWrapper(DataRow row) {
            Row = row;
        }

        public DataRow Row {
            get;
            private set;
        }
    }
}
