using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class Departament : Domain {

        private void init(long num = 0, object idTypeDep = null, Func<ICollection<Stage>> lazyFactory = null) {
			Num = num;
			IdTypeDep = idTypeDep;
            lazy = new Lazy<ICollection<Stage>>(lazyFactory ?? (() => new HashSet<Stage>()));
		}

        public Departament(object id = null)
            : base(id) {
            init();
        }

        public Departament(object id = null, long num = 0, object idTypeDep = null, Func<ICollection<Stage>> lazyFactory = null)
			: base(id) {
            init(num, idTypeDep, lazyFactory);
		}

        public Departament(object id = null, long num = 0, TypeDep typeDep = null, Func<ICollection<Stage>> lazyFactory = null)
			: base(id) {
            init(num, null, lazyFactory);
			TypeDep = typeDep;
			if (TypeDep != null)
				IdTypeDep = TypeDep.Id;
		}

        public long Num {
			get;
			set;
        }

        public object IdTypeDep {
			get /*{
				if(TypeDep == null)
					return null;
				return TypeDep.Id;
			}*/;
			set;
        }

        public TypeDep TypeDep {
			get;
			set;
        }

        private Lazy<ICollection<Stage>> lazy;

        public ICollection<Stage> Stages {
			get {
                return lazy.Value;
            }
        }
    }
}
