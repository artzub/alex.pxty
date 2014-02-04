using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class Departament : Domain {

        private void init(long num = 0, object idTypeDep = null, Func<IList<Stage>> lazyFactory = null) {
			Num = num;
			IdTypeDep = idTypeDep;
            lazy = new Lazy<IList<Stage>>(lazyFactory ?? (() => new System.ComponentModel.BindingList<Stage>()));
		}

        public Departament(object id = null)
            : base(id) {
            init();
        }

        public Departament(object id = null, long num = 0, object idTypeDep = null, Func<IList<Stage>> lazyFactory = null)
			: base(id) {
            init(num, idTypeDep, lazyFactory);
		}

        public Departament(object id = null, long num = 0, TypeDep typeDep = null, Func<IList<Stage>> lazyFactory = null)
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

		private static Departament defValue;
		public static Departament Default {
			get {
                return defValue ?? (defValue = new Departament(1, 0, TypeDep.Default));
			}
		}

        private Lazy<IList<Stage>> lazy;

        public IList<Stage> Stages {
			get {
                return lazy.Value;
            }
        }

        public override void Update(IDomain obj) {
            var item = obj as Departament;
            if (item == null)
                return;

            base.Update(obj);

            IdTypeDep = item.IdTypeDep;
            TypeDep = item.TypeDep;
            if (TypeDep != null)
                IdTypeDep = TypeDep.Id;

            Num = item.Num;

            lazy = item.lazy;
        }

        public override string ToString () {
			return string.Format ("{0} {1}", Num, TypeDep);
		}
    }
}
