using System;

namespace Db
{
	public interface IDomain : IComparable<IDomain>, IEquatable<IDomain> {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
		object Id {
			get;
			set;
		}

	    void Update(IDomain obj);
	}
}

