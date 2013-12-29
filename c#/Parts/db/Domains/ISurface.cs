using System;

namespace Db.Domains
{
	public interface ISurface : IDomain, INamed
	{
        System.Collections.Generic.ICollection<IStage> Stages {
            get;
            set;
        }
	}
}

