using System;

namespace Db.Domains
{
	public interface IDepartament : IDomain
	{
		long Num {
			get;
			set;
		}

		object IdTypeDep {
			get;
			set;
		}

        ITypeDep TypeDep {
            get;
            set;
        }

        System.Collections.Generic.ICollection<IStage> Stages {
            get;
            set;
        }
	}
}

