using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class Surface : DomainNamed
	{   
        public Surface(object id)
            : this(id, string.Empty) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Surface"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="lazyFactory">The lazy factory. <seealso cref="Lazy{T}"/></param>
        public Surface(object id = null, string name = null, Func<IList<Stage>> lazyFactory = null)
			: base(id, name) {
                InitLazyFactory(lazyFactory);
		}

        public void InitLazyFactory(Func<IList<Stage>> lazyFactory) {
            lazy = new Lazy<IList<Stage>>(lazyFactory ?? (() => new System.ComponentModel.BindingList<Stage>()));
        }

        private Lazy<IList<Stage>> lazy;

        public IList<Stage> Stages {
			get {
                return lazy.Value;
            }
        }

        public override void Update(IDomain obj) {
            var item = obj as Surface;
            if (item == null)
                return;

            base.Update(obj);
            lazy = item.lazy;
        }

        private static Surface defValue;
        public static Surface Default {
            get {
                return defValue ?? (defValue = new Surface(1, "(None)"));
            }
        }

        public static Surface Empty {
            get {
                return new Surface(null);
            }
        }
    }
}

