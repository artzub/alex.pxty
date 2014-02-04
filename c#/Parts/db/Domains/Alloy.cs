using System;
using System.Collections.Generic;

namespace Db.Domains
{
	public class Alloy : DomainNamed
	{
        public Alloy(object id)
            : this(id, string.Empty) {
        }

		public Alloy (object id = null, string name = default(string), Func<IList<Part>> lazyFactory = null) 
			: base(id, name) {
			InitLazyFactory(lazyFactory);
		}

        public void InitLazyFactory(Func<IList<Part>> lazyFactory) {
            lazy = new Lazy<IList<Part>>(lazyFactory ?? (() => new System.ComponentModel.BindingList<Part>()));
        }

	    public override void Update(IDomain obj) {
	        var item = obj as Alloy;
            if (item == null)
                return;

	        base.Update(obj);
	        lazy = item.lazy;
	    }

	    private Lazy<IList<Part>> lazy;

		public IList<Part> Parts {
			get {
                return lazy.Value;
            }
		}

        private static Alloy defValue;
        public static Alloy Default {
            get {
                return defValue ?? (defValue = new Alloy(1, "(None)"));
            }
        }

        public static Alloy Empty {
            get {
                return new Alloy(null);
            }
        }
    }
}

