using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

namespace GUIWinForms {
    static class Program {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			var bw = new BackgroundWorker();
			bw.WorkerReportsProgress = true;
			bw.WorkerSupportsCancellation = true;
			bw.DoWork += HandleDoWork;
			bw.ProgressChanged += HandleProgressChanged;
			bw.RunWorkerCompleted += HandleRunWorkerCompleted;

			while (true) {
				using (fr = new SplashForm()) {				

					fr.Show ();

					fr.Label = "Подключение к бд";

					bw.RunWorkerAsync ();
					while (bw.IsBusy)
						Application.DoEvents ();

				}

				if (error == null) {
					Application.Run (new MainForm ());
					break;
				}
				else {
					error.ShowError ();
					if (error.Message.StartsWith ("ORA-01017")) {
						using(var ef = new EditForm()) {
							var ed = new DbTextEdit () {
								Width = 300,
								EditValue = oraSetting.Pass
							};
							ed.OnApplyValue += (object sender, ValidateEventArgs e) => {
								oraSetting.Pass = string.Format("{0}", e.EditValue);
							};
							ed.OnValidatingValue += EditRowController.be_OnValidatingValue;

							ef.Init (new List<DbEdit> () {
								ed
							});
							if (ef.ShowDialog() == DialogResult.Cancel)
								break;
						}
					}
					else
						break;
				}
			}
		}
		

        static void HandleRunWorkerCompleted (object sender, RunWorkerCompletedEventArgs e)  {
			//ex = e.Result as Exception;
        }

		static SplashForm fr;
		static Controller.ConnectionOraSetting oraSetting = new Controller.ConnectionOraSetting() {
			Pass = "ffff"
		};

        static void HandleProgressChanged (object sender, ProgressChangedEventArgs e) {
            switch (e.ProgressPercentage) {
                case -2 : 
                    fr.Max = Convert.ToInt32(e.UserState);
                    break;
                case -1 : 
                    fr.Position = Convert.ToInt32(e.UserState);
                    break;
                default:
                    fr.Inc();
                    break;
            }
		}

		static Exception error;

        static void HandleDoWork (object sender, DoWorkEventArgs e) {
			var bw = sender as BackgroundWorker;
			var i = 0;
			var c = 10;
            bw.ReportProgress(-2, c);
			try {
				while (true) {
					try {
						Controller.DataManager.Instance.Config(oraSetting);	
						break;
					} catch (Exception ex) {
						if (++i < c && !ex.Message.StartsWith("ORA-01017")) {
							bw.ReportProgress(i);
							System.Threading.Thread.Sleep(1000);
							continue;
						}
						throw ex;
					}
				}
			} catch (Exception ex) {
				error = ex;
			}
        }

        private static string GetExceptionMessage(this Exception ex, string msg) {
            return ex.InnerException == null ? string.Format("{0}{1}{2}", msg, "\r\n", ex.Message) : ex.InnerException.GetExceptionMessage(msg);
        }

        public static void ShowError(this Exception ex, IWin32Window owner = null) {
            MessageBox.Show(owner, ex.GetExceptionMessage(""), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
