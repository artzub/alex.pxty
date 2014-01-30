using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUIWinForms {
    static class Program {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static string GetExceptionMessage(this Exception ex, string msg) {
            return ex.InnerException == null ? string.Format("{0}{1}{2}", msg, "\r\n", ex.Message) : ex.InnerException.GetExceptionMessage(msg);
        }

        public static void ShowError(this Exception ex, IWin32Window owner = null) {
            MessageBox.Show(owner, ex.GetExceptionMessage(""), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
