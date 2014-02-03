using Db.Domains;
using Db.Mapping;

namespace Controller {
	public class AlloyController : Controller<Alloy> {
        public AlloyController() {
            base.Mapper = new AlloyMapper();
        }
    }
}
