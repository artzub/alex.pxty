using System;
using System.Collections.Generic;

namespace Db.Domains
{
	public interface ITypeDep : IDomain, INamed
	{
		ICollection<IDepartament> Departaments {
			get;
			set;
		}
	}
}

