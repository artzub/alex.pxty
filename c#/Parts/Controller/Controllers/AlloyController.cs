using Db.Domains;
using Db.Mapping;

namespace Controller {
    public class AlloyController : Controller<Alloy> {
        private AlloyMapper mapper;

        public AlloyController()
            : base() {
            mapper = new AlloyMapper();
            base.Mapper = (IMapper<Alloy>)mapper;
        }
    }
}
