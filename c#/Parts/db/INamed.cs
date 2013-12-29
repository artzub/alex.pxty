using System;

namespace Db
{
	public interface INamed
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name {
			get;
			set;
		}
	}
}

