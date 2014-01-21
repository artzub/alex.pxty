using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class Part : DomainNamed
    {
		private void init (decimal cost = 0, object idAlloy = null, string blNumber = null, Func<ICollection<Stage>> lazyFactory = null)	{
			Cost = cost;
			IdAlloy = idAlloy;
			BLNumber = blNumber;
            lazy = new Lazy<ICollection<Stage>>(lazyFactory ?? (() => new HashSet<Stage>()));
		}

        public Part(object id)
            : base(id) {
            init();
        }

		public Part (object id = null, string name = null, decimal cost = 0, string blNumber = null, object idAlloy = null, Func<ICollection<Stage>> lazyFactory = null)
			: base(id, name) {
			init (cost, idAlloy, lazyFactory);
		}

		public Part(object id = null, string name = null, decimal cost = 0, string blNumber = null, Alloy alloy = null, Func<ICollection<Stage>> lazyFactory = null)
			: base(id, name) {
			init (cost, null, blNumber, lazyFactory);
			Alloy = alloy;
			if (Alloy != null)
				IdAlloy = Alloy.Id;
		}

        public object IdAlloy {
            get;
            set;
        }

        public decimal Cost {
			get;
			set;
        }

		public string BLNumber {
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
