using System;
using Core;
using ComPlusServer;

namespace ComPlusClient
{
	public class Client 
	{
		ISearcher sch;
		public bool Enable{ get; private set; }  

		public Client (int step = 2)
		{
			sch = new SearcherComp();
			sch.Step = step;

			Enable = sch != null;
		}

		public void InitStep(int step) 
		{
			sch.Step = step;
		}

		public bool Add (int item)
		{
			return Enable && sch.AddItem(item) > -1;
		}

		public bool Fix ()
		{
			return Enable && sch.Fix();
		}

		public override string ToString ()
		{
			return string.Format ("{0}", Enable ? sch.ItemsToString() : "Not initialized");
		}

	}
}