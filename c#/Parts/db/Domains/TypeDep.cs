using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class TypeDep : DomainNamed
    {
        public TypeDep(object id = null, string name = null, Func<ICollection<Departament>> lazyFactory = null)
			: base(id, name) {
            lazy = new Lazy<ICollection<Departament>>(lazyFactory ?? (() => new HashSet<Departament>()));
		}

        private Lazy<ICollection<Departament>> lazy;

        public ICollection<Departament> Departaments {
            get {
                return lazy.Value;
            }
        }
    }
}
