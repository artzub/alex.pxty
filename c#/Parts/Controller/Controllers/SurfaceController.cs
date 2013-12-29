using Db.Domains;
using Db.Mapping;

namespace Controller {
    public class SurfaceController : Controller<Surface> {
        private SurfaceMapper mapper;

        public SurfaceController()
            : base() {
                mapper = new SurfaceMapper();
                base.Mapper = (IMapper<Surface>)mapper;
        }
    }
}
