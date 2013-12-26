using System;
using System.Collections.Generic;

namespace db.Domains
{
	public class Alloy : DomainNamed, IAlloy
	{
		public Alloy (object id = null, string name = default(string)) 
			: base(id, name) {
		}

		public ICollection<IPart> Parts {
			get;
			set;
		}

	}
}

