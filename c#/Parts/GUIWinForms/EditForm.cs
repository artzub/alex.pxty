using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUIWinForms {
    public partial class EditForm : Form {
        public EditForm() {
            InitializeComponent();
        }

        public EditForm(object editValue, bool initEdit = true) {
            InitializeComponent();
            EditValue = editValue;
			if (initEdit)
            	Init(EditRowController.GetRowEditors(editValue));
        }

        public void Init(IList<DbEdit> list) {
            flp.Controls.AddRange(list.ToArray());

            var w = list.Max(x => x.Width) + 10;
            var h = list.Sum(x => x.Height);
			AutoSize = false;

			Height = flp1.Height + h + 68;
			Width += (Math.Max (Math.Max (w, flp1.Width), Width) - Width); 
			//AutoSize = true;
        }

        public object EditValue {
            get;
            private set;
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e) {
			if ((e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing) && DialogResult == System.Windows.Forms.DialogResult.OK) {
                foreach (IDbEdit item in flp.Controls) {
                    e.Cancel = !item.ValidateValue();
                    if (e.Cancel) {
                        MessageBox.Show(item.LastValidationErrorMessage, item.Label, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        (item as Control).Focus();
                        break;
                    }
					item.ApplyValue();
                }
            }
        }
    }
}
