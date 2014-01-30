using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class TypeDep : DomainNamed
    {
        private void init(Func<IList<Departament>> lazyFactory = null) {
            lazy = new Lazy<IList<Departament>>(lazyFactory ?? (() => new System.ComponentModel.BindingList<Departament>()));
        }

        public TypeDep(object id = null)
            : base(id) {
                init();
        }

        public TypeDep(object id = null, string name = null, Func<IList<Departament>> lazyFactory = null)
			: base(id, name) {
                init(lazyFactory);
		}

        public void InitLazyFactory(Func<IList<Departament>> lazyFactory) {
            init(lazyFactory);
        }

        private Lazy<IList<Departament>> lazy;

        public IList<Departament> Departaments {
            get {
                return lazy.Value;
            }        
        }

        private static TypeDep defValue;
        public static TypeDep Default {
            get {
                if (defValue == null)
                    defValue = new TypeDep(1, "(None)");
                return defValue;
            }
        }

        public static TypeDep Empty {
            get {
                return new TypeDep(null);
            }
        }
    }
}
