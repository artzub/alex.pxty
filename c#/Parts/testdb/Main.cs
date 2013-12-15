using System;
using db;

namespace testdb
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var list = (new MyClass()).getSurface();

			foreach(var item in list)
				Console.WriteLine(item);
		}
	}
}
