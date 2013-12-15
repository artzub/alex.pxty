using System;
using Core.Messaging;

namespace msg_server
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			(new Server()).Start ();
			Console.WriteLine("Press Enter to exit...");
			Console.ReadLine();
		}
	}
}
