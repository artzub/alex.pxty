using System;
using System.Collections.Generic;

namespace db.Domains
{
    public class Departament : Domain, IDepartament {

		private void init (long num = 0, object idTypeDep = null)
		{
			Num = num;
			IdTypeDep = idTypeDep;
		}

		public Departament (object id = null, long num = 0, object idTypeDep = null)
			: base(id) {
			init (num, idTypeDep);
		}

		public Departament (object id = null, long num = 0, ITypeDep typeDep = null)
			: base(id) {
			init (num, null);
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

        public ITypeDep TypeDep {
			get;
			set;
        }

        public ICollection<IStage> Stages {
			get;
			set;
        }
    }
}
