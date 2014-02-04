using System;
using System.Collections.Generic;
using Db.Domains;

namespace Db
{
	public static class Hashes
	{
		private static readonly Dictionary<object, Stage> stagesHash = new Dictionary<object, Stage>();
		public static Dictionary<object, Stage> StagesHash {
			get {
				return stagesHash;
			}
		}

		public static Stage GetStageById(object id) {
			Stage item = null;
			if (stagesHash.TryGetValue (id, out item))
				return item;
			return item;
		}
	}
}

