using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using Controller;

namespace GUIWinForms {
    static class Program {
        // TODO 1 Ошибка No digits found.
        // TODO 5 При добавление маршрута входим в рекурсию. А ведь дожен быть начальный и конечный цех


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

			try {

	            using (fr = new SplashForm()) {
	                fr.Show();

					fr.Label = "Чтение настроек";

					LoadOraSetting();

					while (true) {
						try {
	                        fr.Label = "Подключение к бд";

	                        bw.RunWorkerAsync();
	                        while (bw.IsBusy)
	                            Application.DoEvents();
	                    
		                    if (error == null)
		                        break;
		                    throw error;
		                }
		                catch (Exception ex) {
		                    ex.ShowError();
		                    if (!EditOraSettings(fr)) {
		                        break;
							}
							error = null;
		                }
					}
				}

            
                if (error == null) {
                    SaveOraSetting();
                    Application.Run(new MainForm());
                }
            }
            catch (Exception ex) {
                ex.ShowError();
            }
		}

        private static bool EditOraSettings(IWin32Window owner) {
            using (var ef = new EditForm()) {

                var list = new List<DbEdit>();
                var ed = new DbTextEdit() {
					Label = "User",
                    Width = 300,
                    EditValue = oraSetting.User
                };
                ed.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    oraSetting.User = string.Format("{0}", e.Value);
                };
                ed.OnValidatingValue += EditRowController.be_OnValidatingValue;
                list.Add(ed);

                ed = new DbTextEdit() {
					Label = "Password",
                    Width = 300,
					PasswordChar = '*',
                    EditValue = oraSetting.Pass
                };
                ed.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    oraSetting.Pass = string.Format("{0}", e.Value);
                };
                ed.OnValidatingValue += EditRowController.be_OnValidatingValue;
                list.Add(ed);

                ed = new DbTextEdit() {
					Label = "Service",
                    Width = 300,
                    EditValue = oraSetting.Service
                };
                ed.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    oraSetting.Service = string.Format("{0}", e.Value);
                };
                ed.OnValidatingValue += EditRowController.be_OnValidatingValue;
                list.Add(ed);

                ed = new DbTextEdit() {
					Label = "Host",
                    Width = 300,
                    EditValue = oraSetting.Host
                };
                ed.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    oraSetting.Host = string.Format("{0}", e.Value);
                };
                ed.OnValidatingValue += EditRowController.be_OnValidatingValue;
                list.Add(ed);

                var se = new DbSpinEdit() {
					Label = "Port",
                    Width = 300,
                    Minimum = 1,
                    Maximum = Int16.MaxValue,
					Value = string.IsNullOrWhiteSpace(oraSetting.Port) ? 1521 : Convert.ToInt32(oraSetting.Port),
                    EditValue = string.IsNullOrWhiteSpace(oraSetting.Port) ? 1521 : Convert.ToInt32(oraSetting.Port)
                };
                se.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    oraSetting.Port = string.Format("{0}", e.EditValue);
                };
                se.OnValidatingValue += EditRowController.be_OnValidatingValue;
                list.Add(se);

                ef.Init(list);
                if (ef.ShowDialog(owner) == DialogResult.OK) {
                    return true;
                }
            }
            return false;
        }

        static void HandleRunWorkerCompleted (object sender, RunWorkerCompletedEventArgs e)  {
			//ex = e.Result as Exception;
        }

		static SplashForm fr;
		static ConnectionOraSetting oraSetting;

        static void LoadOraSetting() {
            var settings = Properties.Settings.Default;
            settings.ReloadOur();

            oraSetting = new ConnectionOraSetting();
            oraSetting.Host = settings.Host;
            oraSetting.Port = settings.Port;
            oraSetting.Service = settings.Service;
            oraSetting.User = settings.User;
            if (string.IsNullOrWhiteSpace(settings.Password))
                throw new Exception("Пароль пуст");
			oraSetting.Pass = settings.Password;
        }

        static void SaveOraSetting() {
            var settings = Properties.Settings.Default;
            settings.Host = oraSetting.Host;
            settings.Port = oraSetting.Port;
            settings.Service = oraSetting.Service;
            settings.User = oraSetting.User;
            settings.Password = oraSetting.Pass;

			settings.SaveOur();
        }

		static void SaveOur(this Properties.Settings ss) {
			var conf = new System.IO.FileInfo ("app.ini");
			if (conf.Exists)
				conf.Delete ();
			conf.Refresh ();
			using (var wr = conf.AppendText()) {
				wr.WriteLine ("{0}={1}", "host", ss.Host);
				wr.WriteLine ("{0}={1}", "port", ss.Port);
				wr.WriteLine ("{0}={1}", "service", ss.Service);
				wr.WriteLine ("{0}={1}", "user", ss.User);
				wr.WriteLine ("{0}={1}", "password", ss.Password);
			}
		}

		static void ReloadOur(this Properties.Settings ss) {
			var conf = new System.IO.FileInfo ("app.ini");
			if (!conf.Exists)
				return;

			using (var tr = conf.OpenText()) {
				var str = string.Empty;
				while (!tr.EndOfStream) {				
					str = tr.ReadLine ();
					var arr = str.Split ('=');
					if (arr.Length > 0) {
						str = string.Empty;
						if (arr.Length > 0)
							str = arr.Skip (1).Aggregate ((x, y) => x + y);
						switch (arr [0].ToLower ()) {
						case "host":
							ss.Host = str;
							break;
						case "port":
							ss.Port = str;
							break;
						case "service":
							ss.Service = str;
							break;
						case "user":
							ss.User = str;
							break;
						case "password":
							ss.Password = str;
							break;
						}
					}
				}
			}
		}

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
						//oraSetting.Pass="jjjj";
						Controller.DataManager.Instance.Config(oraSetting);	
						break;
					} catch (Exception ex) {
						if (++i < c && !(ex.Message.StartsWith("ORA-01017") || 
						                 ex.Message.StartsWith("ORA-28000") || 
						                 ex.Message.StartsWith("libclntsh.so"))) {
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
            return ex.InnerException == null 
                ? string.Format("{0}\r\n{1}\r\n", msg, ex.Message/*, ex.StackTrace*/) 
                : ex.InnerException.GetExceptionMessage(msg);
        }

        public static void ShowError(this Exception ex, IWin32Window owner = null) {

			var msg = ex.GetExceptionMessage("").Trim();
            if (msg.StartsWith("ORA-"))
                switch (msg.Substring(4, 5)) {
                    case "02292":
						msg = "Существуют дочернии записи!"; 
                        break;
					case "28000":
						msg = "Учетная запись заблокированна. Обратитесь к администратору."; 
						break;
                }
			if (msg.StartsWith ("libclntsh")) {
				msg = "Не найдена нативная библиотека" +
					" Oracle";
				MessageBox.Show(owner, msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(0);
			} 

            MessageBox.Show(owner, msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
