using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class Surface : DomainNamed
	{
		public Surface (object id = null, string name = null, Func<ICollection<Stage>> lazyFactory = null)
			: base(id, name) {
            lazy = new Lazy<ICollection<Stage>>(lazyFactory ?? (() => new HashSet<Stage>()));
		}

        private Lazy<ICollection<Stage>> lazy;

        public ICollection<Stage> Stages {
			get {
                return lazy.Value;
            }
        }
    }
}

