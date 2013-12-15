using System;
using System.Windows.Forms;

namespace RemClientGuiWinForms
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
