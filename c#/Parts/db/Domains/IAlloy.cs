using System;
using System.Collections.Generic;

namespace db.Domains
{
	public interface IAlloy : IDomain, INamed
	{
		ICollection<IPart> Parts {
			get;
			private set;
        }
	}
}

