using System;

namespace db
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

