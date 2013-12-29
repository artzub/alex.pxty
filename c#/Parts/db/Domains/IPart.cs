using System;
using System.Collections.Generic;

namespace db.Domains
{
	public interface IPart : IDomain, INamed
	{
		object IdAlloy {
			get;
			set;
		}

		long Cost {
			get;
			set;
		}

        IAlloy Alloy {
            get;
            set;
        }

        ICollection<IStage> Stages {
            get;
            set;
        }
	}
}
