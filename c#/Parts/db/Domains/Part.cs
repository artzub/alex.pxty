﻿using System;
using System.Collections.Generic;

namespace Db.Domains
{
    public class Part : DomainNamed
    {
		private void init (decimal cost = 0, string blNumber = null, object idAlloy = null, Func<IList<Stage>> lazyFactory = null)	{
			Cost = cost;
			IdAlloy = idAlloy;
			BLNumber = blNumber;
            lazy = new Lazy<IList<Stage>>(lazyFactory ?? (() => new System.ComponentModel.BindingList<Stage>()));
		}

        public Part(object id)
            : base(id) {
            init();
        }

		public Part (object id = null, string name = null, decimal cost = 0, string blNumber = null, object idAlloy = null, Func<IList<Stage>> lazyFactory = null)
			: base(id, name) {
			init (cost, blNumber, idAlloy, lazyFactory);
		}

		public Part(object id = null, string name = null, decimal cost = 0, string blNumber = null, Alloy alloy = null, Func<IList<Stage>> lazyFactory = null)
			: base(id, name) {
			init (cost, blNumber, null, lazyFactory);
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

        private Lazy<IList<Stage>> lazy;

        public bool LazyValueCreated {
            get {
                return lazy.IsValueCreated;
            }
        }

        public IList<Stage> Stages {
			get {
                return lazy.Value;
            }
        }

        public override void Update(IDomain obj) {
            var item = obj as Part;
            if (item == null)
                return;

            base.Update(obj);

            Alloy = item.Alloy;
            if (Alloy != null)
                IdAlloy = Alloy.Id;

            BLNumber = item.BLNumber;
            Cost = item.Cost;

            lazy = item.lazy;
        }

        public override string ToString () {
			return string.Format ("{0} {1} {2}", BLNumber, Name, Alloy);
		}
    }
}
