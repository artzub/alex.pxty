using System;

namespace db.Domains
{
    public class Surface : DomainNamed, ISurface
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="db.Surface"/> class.
		/// </summary>
		/// <param name='id'>
		/// Identifier.
		/// </param>
		/// <param name='name'>
		/// Name.
		/// </param>
		public Surface (object id = null, string name = null)
			: base(id, name) {
		}

        public System.Collections.Generic.ICollection<IStage> Stages {
			get;
			private set;
        }
    }
}

