using System.Data;

namespace db.Mapping {
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
