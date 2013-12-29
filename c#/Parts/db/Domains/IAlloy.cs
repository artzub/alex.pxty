using System;
using System.Collections.Generic;

namespace Db.Domains
{
	public interface IAlloy : IDomain, INamed
	{
		ICollection<IPart> Parts {
			get;
			set;
        }
	}
}

