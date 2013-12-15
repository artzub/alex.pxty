using System;

namespace db
{
	public class Surface : ISurface
	{
		public Surface (object id, string name)
		{
			Id = id;
			Name = name;
		}

		#region IDomain implementation
		public object Id {
			get;
			set;
		}
		#endregion

		#region INamed implementation
		public string Name {
			get;
			set;
		}
		#endregion

		public override string ToString ()
		{
			return string.Format("ID={0};NAME={1}", Id, Name);
		}
	}
}

