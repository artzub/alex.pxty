using Db.Mapping;
using Db.Domains;

namespace Controller {
    public class TypeDepController : Controller<TypeDep> {
        private TypeDepMapper mapper;

        public TypeDepController()
            : base() {
                mapper = new TypeDepMapper();
                base.Mapper = (IMapper<TypeDep>)mapper;
        }
    }
}
