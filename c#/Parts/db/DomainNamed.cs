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

	    public override void Update(IDomain obj) {
	        var newItem = obj as DomainNamed;
            if (newItem == null)
                return;
	        Name = newItem.Name;
	    }

	    public override string ToString () {
			return Name;
		}
	}
}

