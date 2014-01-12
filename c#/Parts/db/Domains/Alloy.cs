using System;
using System.Collections.Generic;

namespace Db.Domains
{
	public class Alloy : DomainNamed
	{
		public Alloy (object id = null, string name = default(string), Func<ICollection<Part>> lazyFactory = null) 
			: base(id, name) {
            lazy = new Lazy<ICollection<Part>>(lazyFactory ?? (() => new HashSet<Part>()));
		}

        private Lazy<ICollection<Part>> lazy;

		public ICollection<Part> Parts {
			get {
                return lazy.Value;
            }
		}

	}
}

