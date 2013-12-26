using System;
using System.Collections.Generic;
using System.Text;


namespace Core
{

	public class Searcher : MarshalByRefObject, ISearcher
	{
		private LinkedList<int> items;
		/// <summary>
		/// Публичное свойство типа LinkedList для приватного члена items
		/// Публичное получение с отложенной инцилизацией и закрытым присвоением
		/// (только для чтения)
		/// </summary>
		/// <value>
		/// The items.
		/// </value>
		public LinkedList<int> Items {
		
			get {
				if (items == null) 
					items = new LinkedList<int> ();

				return items;
			}
			private set {
				items = value;
			}
		}
		/// <summary>
		/// Gets the step.
		/// </summary>
		/// <value>
		/// The step.
		/// </value>
		private int step;
		public int Step {
			get {
				return step;
			}
			set {
				step = value;
			}
		}

		public int AddItem (int item)
		{
			var i = -1;
			var first = Items.First;
			if (first == null
			    || (Validate (first.Value, Step, item)
			    	&& !Items.Contains(item))) {
				Items.AddLast(item);
				i = Items.Count - 1;
			}
			return i; 
		}

		public Searcher () {
			Console.WriteLine("Created {0}", DateTime.Now);
		}

		public Searcher (int step) {
			Step = step;
		}

		public static bool Validate (int first, int step, int value){
			var n = (value - first) % step;
			return n == 0;
		}

		public bool Fix () {
			if (items == null || 
				Items.Count < 1 || 
				Items.First == null) 
				return false;

            var item = Items.First;
			while (item.Next != null) {
                while (Math.Abs(item.Next.Value - item.Value) > Math.Abs(Step)) {
                    items.AddBefore(item.Next, item.Next.Value - Step);
                }                
                item = item.Next;
			}
			return true;
		}

		public string ItemsToString ()
		{
			var arr = string.Empty; //""
			foreach (var item in Items) {
				arr += item.ToString();
				if (Items.Last.Value != item)
					arr += ",";
			}
			return arr;
		}



		public override string ToString ()
		{
			return string.Format ("[Searcher: Items={0}, Step={1}]", ItemsToString(), Step);
		}
	}

	public class RemoteTime : MarshalByRefObject, IRemoteTime 
	{
	  public DateTime CurrentTime 
	  {
	    get { return DateTime.Now; }
	  }
	}

	public interface IRemoteTime 
	{
	  DateTime CurrentTime {get;}
	}
}

//2,4,..,8,..,12