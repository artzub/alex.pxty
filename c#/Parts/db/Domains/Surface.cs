using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class Surface : DomainNamed
	{   
        private void init(Func<ICollection<Stage>> lazyFactory = null) {
            lazy = new Lazy<ICollection<Stage>>(lazyFactory ?? (() => new HashSet<Stage>()));
        }

        public Surface(object id)
            : base(id) {
            init();
        }

		public Surface (object id = null, string name = null, Func<ICollection<Stage>> lazyFactory = null)
			: base(id, name) {
                init(lazyFactory);
		}

        public void InitLazyFactory(Func<ICollection<Stage>> lazyFactory) {
            init(lazyFactory);
        }

        private Lazy<ICollection<Stage>> lazy;

        public ICollection<Stage> Stages {
			get {
                return lazy.Value;
            }
        }

        private static Surface defValue;
        public static Surface Default {
            get {
                if (defValue == null)
                    defValue = new Surface(1, "(None)");
                return defValue;
            }
        }

        public static Surface Empty {
            get {
                return new Surface(null);
            }
        }
    }
}

