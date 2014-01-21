using System;
using System.Collections.Generic;

namespace Db.Domains
{
	public class Alloy : DomainNamed
	{
        private void init(Func<ICollection<Part>> lazyFactory = null) {
            lazy = new Lazy<ICollection<Part>>(lazyFactory ?? (() => new HashSet<Part>()));
        }

        public Alloy(object id)
            : base(id) {
                init();
        }

		public Alloy (object id = null, string name = default(string), Func<ICollection<Part>> lazyFactory = null) 
			: base(id, name) {
            init(lazyFactory);
		}

        public void InitLazyFactory(Func<ICollection<Part>> lazyFactory) {
            init(lazyFactory);
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

