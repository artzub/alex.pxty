using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class TypeDep : DomainNamed
    {
        public TypeDep(object id = null)
            : this(id, string.Empty) {
        }

        public TypeDep(object id = null, string name = null, Func<IList<Departament>> lazyFactory = null)
			: base(id, name) {
                InitLazyFactory(lazyFactory);
		}

        public void InitLazyFactory(Func<IList<Departament>> lazyFactory) {
            lazy = new Lazy<IList<Departament>>(lazyFactory ?? (() => new System.ComponentModel.BindingList<Departament>()));
        }

        private Lazy<IList<Departament>> lazy;

        public IList<Departament> Departaments {
            get {
                return lazy.Value;
            }        
        }

        public override void Update(IDomain obj) {
            var item = obj as TypeDep;
            if (item == null)
                return;

            base.Update(obj);
            lazy = item.lazy;
        }

        private static TypeDep defValue;
        public static TypeDep Default {
            get {
                return defValue ?? (defValue = new TypeDep(1, "(None)"));
            }
        }

        public static TypeDep Empty {
            get {
                return new TypeDep(null);
            }
        }
    }
}
