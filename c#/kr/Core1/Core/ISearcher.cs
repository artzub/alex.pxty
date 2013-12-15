using System;

namespace Core
{
	public interface ISearcher {
		int AddItem (int item);
		bool Fix ();
		string ItemsToString();
		int Step { get; set; }
	}
}

