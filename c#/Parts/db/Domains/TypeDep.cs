using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class TypeDep : DomainNamed, ITypeDep
    {
		public TypeDep (object id = null, string name = null)
			: base(id, name) {
		}

        public ICollection<IDepartament> Departaments {
			get;
			set;
        }
    }
}
