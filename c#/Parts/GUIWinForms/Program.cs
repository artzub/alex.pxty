using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Configuration;
using Controller;

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
                try {
                    using (fr = new SplashForm()) {
                        LoadOraSetting();

                        fr.Show();

                        fr.Label = "Подключение к бд";

                        bw.RunWorkerAsync();
                        while (bw.IsBusy)
                            Application.DoEvents();
                    }
                    if (error == null)
                        break;
                    throw error;
                }
                catch (Exception ex) {
                    ex.ShowError();
                    if (EditOraSettings())
                        break;
                }

			    try {
                    if (error == null)
                        SaveOraSetting();
                        Application.Run(new MainForm());
			    }
			    catch (Exception ex) {
                    ex.ShowError();
			    }
			}
		}

        private static bool EditOraSettings() {
            using (var ef = new EditForm()) {
                var list = new List<DbEdit>();
                var ed = new DbTextEdit() {
                    Width = 300,
                    EditValue = oraSetting.User
                };
                ed.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    oraSetting.User = string.Format("{0}", e.EditValue);
                };
                ed.OnValidatingValue += EditRowController.be_OnValidatingValue;
                list.Add(ed);

                ed = new DbTextEdit() {
                    Width = 300,
                    EditValue = oraSetting.Pass
                };
                ed.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    oraSetting.Pass = string.Format("{0}", e.EditValue);
                };
                ed.OnValidatingValue += EditRowController.be_OnValidatingValue;
                list.Add(ed);

                ed = new DbTextEdit() {
                    Width = 300,
                    EditValue = oraSetting.Service
                };
                ed.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    oraSetting.Service = string.Format("{0}", e.EditValue);
                };
                ed.OnValidatingValue += EditRowController.be_OnValidatingValue;
                list.Add(ed);

                ed = new DbTextEdit() {
                    Width = 300,
                    EditValue = oraSetting.Host
                };
                ed.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    oraSetting.Host = string.Format("{0}", e.EditValue);
                };
                ed.OnValidatingValue += EditRowController.be_OnValidatingValue;
                list.Add(ed);

                var se = new DbSpinEdit() {
                    Width = 300,
                    Minimum = 1,
                    Maximum = Int16.MaxValue,
                    EditValue = string.IsNullOrWhiteSpace(oraSetting.Port) ? 1521 : Convert.ToInt32(oraSetting.Port)
                };
                se.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    oraSetting.Port = string.Format("{0}", e.EditValue);
                };
                se.OnValidatingValue += EditRowController.be_OnValidatingValue;
                list.Add(se);

                ef.Init(list);
                if (ef.ShowDialog() == DialogResult.Cancel) {
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
            var config = 
				ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				var section = (AppSettingsSection)config.GetSection("oraSetting");

                oraSetting = new ConnectionOraSetting();

				if (section != null) {
					oraSetting.Host = section.Settings["Host"].Value;
                    oraSetting.Port = section.Settings["Port"].Value;
					oraSetting.Service = section.Settings["ServiceName"].Value;
                    oraSetting.User = section.Settings["User"].Value;
                    if (section.Settings["Password"] == null || string.IsNullOrWhiteSpace(section.Settings["Password"].Value))
                        throw new Exception("Пароль пуст");
				    oraSetting.Pass = section.Settings["Password"].Value;
				}
        }

        static void SaveOraSetting() {
            var config =
                ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);
            var section = (AppSettingsSection)config.GetSection("oraSetting");

            if (section != null) {
                config.Sections.Remove("oraSetting");
            }
            else {
                section = new AppSettingsSection();
            }

            section.Settings.Clear();
            section.Settings.Add("Host", oraSetting.Host);
            section.Settings.Add("Port", oraSetting.Port);
            section.Settings.Add("Service", oraSetting.Service);
            section.Settings.Add("User", oraSetting.User);
            section.Settings.Add("Password", oraSetting.Pass);
            section.SectionInformation.AllowDefinition = ConfigurationAllowDefinition.Everywhere;
            section.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToApplication;
            section.SectionInformation.ForceSave = true;
            section.SectionInformation.RestartOnExternalChanges = true;
            config.Sections.Add("oraSetting", section);

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("oraSetting");
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
