using System;
using System.Reflection;
using System.EnterpriseServices;
using System.Runtime.InteropServices;
using Core;

[assembly: ApplicationName("SearcherComp")]
[assembly: Description("Sample of .NET serviced component for work with arithmetic progressions.")]
[assembly: ApplicationActivation(ActivationOption.Library)]
//[assembly: AssemblyKeyFile("key.snk")]

/**
 * для создания snk файла нужно выполнить
 * в консоли C:\Windows\System32\cmd.exe" /k ""C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\Tools\VsDevCmd.bat"
 * сd директория проекта
 * sn -k key.snk
 */ 


/**
 * проект можно скомпилировать в консоли
 * поэтапно
 * C:\Windows\System32\cmd.exe" /k ""C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\Tools\VsDevCmd.bat"
 * сd директория проекта
 * sn -k key.snk
 * csc /target:library /r:System.EnterpriseServices.dll SearcherComp.cs /keyfile:key.snk
 * regsrvs SearcherComp.dll
 */ 

[assembly: ComVisible(true)]
// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e23ff629-3487-4744-8788-2c28775eb93d")]

namespace ComPlusServer
{

	/**
	 * для регистрации библиотеки нужно выполнить в консоли 
	 * C:\Windows\System32\cmd.exe" /k ""C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\Tools\VsDevCmd.bat"
	 * regsrvs ComPlusServer.dll
	 */

	[JustInTimeActivation]
	//[ObjectPooling(Enabled=true, MinPoolSize=2, MaxPoolSize=10, CreationTimeout=2000)]
	[Transaction(TransactionOption.Required)]
	public class SearcherComp : ServicedComponent, ISearcher
	{
		private Searcher sch;	
		private Searcher Sch {
			get {
				if (sch == null)
					sch = new Searcher();
				return sch;
			}
		}

		#region ISearcher implementation
		[AutoComplete]
		public int AddItem (int item)
		{
			return Sch.AddItem(item);
		}

		[AutoComplete]
		public bool Fix ()
		{
			return Sch.Fix();
		}

		[AutoComplete]
		public string ItemsToString ()
		{
			return Sch.ItemsToString();
		}

		public int Step {
			get {
				return Sch.Step;
			}
			set {
				Sch.Step = value;
			}
		}
		#endregion


	}
}

