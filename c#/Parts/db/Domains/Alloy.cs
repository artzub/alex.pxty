using System;
using System.Collections.Generic;

namespace Db.Domains
{
	public class Alloy : DomainNamed
	{
        public Alloy(object id)
            : this(id, default(string), null) {
        }

		public Alloy (object id = null, string name = default(string), Func<ICollection<Part>> lazyFactory = null) 
			: base(id, name) {
			InitLazyFactory(lazyFactory);
		}

        public void InitLazyFactory(Func<ICollection<Part>> lazyFactory) {
			lazy = new Lazy<ICollection<Part>>(lazyFactory ?? (() => new HashSet<Part>()));
        }

        private Lazy<ICollection<Part>> lazy;

		public ICollection<Part> Parts {
			get {
                return lazy.Value;
            }
		}

        private static Alloy defValue;
        public static Alloy Default {
            get {
                if (defValue == null)
                    defValue = new Alloy(1, "(None)");
                return defValue;
            }
        }

        public static Alloy Empty {
            get {
                return new Alloy(null);
            }
        }
    }
}

