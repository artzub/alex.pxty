﻿using System;
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
            Init(EditRowController.GetRowEditors(editValue));
        }

        public void Init(IList<DbEdit> list) {
            flp.Controls.AddRange(list.ToArray());
        }

        public object EditValue {
            get;
            private set;
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.None && DialogResult == System.Windows.Forms.DialogResult.OK) {
                foreach (IDbEdit item in flp.Controls) {
                    e.Cancel = !item.ValidateValue();
                    if (e.Cancel) {
                        MessageBox.Show(item.LastValidationErrorMessage, item.Label, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        (item as Control).Focus();
                        break;
                    }
                }
            }
        }
    }
}