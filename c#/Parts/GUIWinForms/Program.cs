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

			using (var fr = new Form()) {

				var fl = new FlowLayoutPanel ();
				fl.Dock = DockStyle.Fill;
				fl.AutoSize = true;
				fl.WrapContents = false;



				fl.Controls.Add (new Label () {
					Text = "Подключение к бд. Попытка: ",
					AutoSize = true
				});

				st = new Label () {
					Text = "",
					AutoSize = true
				};
				st.SendToBack ();

				fl.Controls.Add (st);

				fr.Controls.Add (fl);

				fr.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				fr.AutoSize = true;



				fr.StartPosition = FormStartPosition.CenterScreen;

				fr.FormBorderStyle = FormBorderStyle.None;

				fr.Show ();

				bw.RunWorkerAsync ();
				while(bw.IsBusy)
					Application.DoEvents();

			}

			if (ex == null) 
				Application.Run (new MainForm ());
			else
				ex.ShowError ();
		}

        static void HandleRunWorkerCompleted (object sender, RunWorkerCompletedEventArgs e)  {
			//ex = e.Result as Exception;
        }

		static Label st;

        static void HandleProgressChanged (object sender, ProgressChangedEventArgs e) {
			if (st != null)
				st.Text = string.Format ("{0}", e.ProgressPercentage);
        }

		static Exception ex;

        static void HandleDoWork (object sender, DoWorkEventArgs e) {
			var bw = sender as BackgroundWorker;
			var i = 0;
			try {
				while (true) {
					try {
						Controller.DataManager.Instance.Equals(0);	
						break;
					} catch (Exception ex) {
						if (++i < 11) {
							bw.ReportProgress(i);
							System.Threading.Thread.Sleep(1000);
							continue;
						}
						throw ex;
					}
				}
			} catch (Exception ex1) {
				ex = ex1;
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
