using System;

namespace db.Domains
{
	public interface ISurface : IDomain, INamed
	{
        System.Collections.Generic.ICollection<IStage> Stages {
            get;
            set;
        }
	}
}

