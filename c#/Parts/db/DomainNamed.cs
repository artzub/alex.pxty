using System;

namespace Db
{
	public class DomainNamed : Domain, INamed 
	{
		public DomainNamed (object id = null, string name = default(string)) 
			: base(id) {
			Name = name;
		}

		public string Name {
			get;
			set;
		}

		public override string ToString ()
		{
			return string.Format ("[{0} Id={1}, Name={2}]", this.GetType().Name, Id, Name);
		}
	}
}

