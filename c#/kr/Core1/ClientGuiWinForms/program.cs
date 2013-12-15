using System;
using System.Windows.Forms;


namespace Winforms
{
	public static class program
	{
		[STAThread]
		public static void Main(string[] args){
			Application.EnableVisualStyles();
			Application.Run((Form) new Main());
		}
	}
}
