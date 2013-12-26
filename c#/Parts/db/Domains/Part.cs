using System;
using System.Collections.Generic;

namespace db.Domains
{
    public class Part : DomainNamed, IPart
    {
		private void init (long cost = 0, object idAlloy = null)	{
			Cost = cost;
			IdAlloy = idAlloy;			
		}

		public Part (object id = null, string name = null, long cost = 0, object idAlloy = null)
			: base(id, name) {
			init (cost, idAlloy);
		}

		public Part (object id = null, string name = null, long cost = 0, IAlloy alloy = null)
			: base(id, name) {
			init (cost, null);
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

        public IAlloy Alloy {
			get;
			set;
        }

        public ICollection<IStage> Stages {
			get;
			set;
        }
    }
}
