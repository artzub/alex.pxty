using System;
using System.Collections.Generic;

namespace db.Domains
{
	public interface ITypeDep : IDomain, INamed
	{
		ICollection<IDepartament> Departaments {
			get;
			set;
		}
	}
}

