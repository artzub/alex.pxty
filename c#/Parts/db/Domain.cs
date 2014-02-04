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

        public virtual void Update(IDomain obj) {
        }

		public override string ToString ()
		{
			return string.Format ("[{0}: Id={1}]", this.GetType().Name, Id);
		}

        #region IComparable<IDomain> Members

        public virtual int CompareTo(IDomain other) {
            var result = other == null || other.Id == null ? 1 : -1;
            if (result < 0 && this.Id != null) {
                var x = Convert.ToInt64(this.Id) - Convert.ToInt64(other.Id);
                result = x > 0 ? 1 : x == 0 ? 0 : result;
            }
            return result;
        }

        #endregion

        #region IEquatable<IDomain> Members

		public override bool Equals (object obj)
		{
			return Equals(obj as IDomain);
		}

		public virtual bool Equals(IDomain other) {
            return CompareTo(other) == 0;
        }

        #endregion
    }
}

