using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class Surface : DomainNamed
	{   
        private void init(Func<IList<Stage>> lazyFactory = null) {
            lazy = new Lazy<IList<Stage>>(lazyFactory ?? (() => new System.ComponentModel.BindingList<Stage>()));
        }

        public Surface(object id)
            : base(id) {
            init();
        }

		public Surface (object id = null, string name = null, Func<IList<Stage>> lazyFactory = null)
			: base(id, name) {
                init(lazyFactory);
		}

        public void InitLazyFactory(Func<IList<Stage>> lazyFactory) {
            init(lazyFactory);
        }

        private Lazy<IList<Stage>> lazy;

        public IList<Stage> Stages {
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

