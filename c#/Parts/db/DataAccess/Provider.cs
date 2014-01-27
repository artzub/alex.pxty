
namespace Db.DataAccess {
    public class Provider {
        public static DatabaseGateway DatabaseGateway {
            get;
            private set;
        }

        public static void Initialize(IDatabaseConnection conn) {
            DatabaseGateway = new DatabaseGateway(conn);
        }
    }
}
