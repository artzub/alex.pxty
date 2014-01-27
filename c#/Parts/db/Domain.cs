using System;

namespace Db
{
	public class Domain : IDomain
	{
		public Domain (object id = null)
		{
			Id = id;
		}

		public object Id {
			get;
			set;
		}

		public override string ToString ()
		{
			return string.Format ("[{0}: Id={1}]", this.GetType().Name, Id);
		}
	}
}

