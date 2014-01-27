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

		public override string ToString () {
			return Name;
		}
	}
}

