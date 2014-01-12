using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class Part : DomainNamed
    {
		private void init (long cost = 0, object idAlloy = null, Func<ICollection<Stage>> lazyFactory = null)	{
			Cost = cost;
			IdAlloy = idAlloy;
            lazy = new Lazy<ICollection<Stage>>(lazyFactory ?? (() => new HashSet<Stage>()));
		}

		public Part (object id = null, string name = null, long cost = 0, object idAlloy = null, Func<ICollection<Stage>> lazyFactory = null)
			: base(id, name) {
			init (cost, idAlloy, lazyFactory);
		}

        public Part(object id = null, string name = null, long cost = 0, Alloy alloy = null, Func<ICollection<Stage>> lazyFactory = null)
			: base(id, name) {
			init (cost, null, lazyFactory);
			Alloy = alloy;
			if (Alloy != null)
				IdAlloy = Alloy.Id;
		}

        public object IdAlloy {
            get;
            set;
        }

        public long Cost {
			get;
			set;
        }

        public Alloy Alloy {
			get;
			set;
        }

        private Lazy<ICollection<Stage>> lazy;

        public ICollection<Stage> Stages {
			get {
                return lazy.Value;
            }
        }
    }
}
