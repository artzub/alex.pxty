using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUIWinForms {
    public partial class DbEdit : UserControl, IDbEdit {
        public DbEdit() {
            InitializeComponent();
            
            InitHelper();
            
            Validating += new CancelEventHandler(DbEdit_Validating);
        }

        protected virtual void InitHelper() {
        }

        private string lastErrorMessage;
        public string LastValidationErrorMessage {
            get {
                return lastErrorMessage;
            }
        }

        void DbEdit_Validating(object sender, CancelEventArgs e) {
            e.Cancel = !DoValidateValue(sender, true);
        }

        private bool DoValidateValue(object sender, bool showMessage = false) {
            var valid = true;
            if (OnValidatingValue != null) {
                var evn = new ValidateEventArgs(GetValue(), EditValue);
                OnValidatingValue(sender, evn);
                if (!(valid = !evn.Cancel)) {
                    lastErrorMessage = evn.Message;
                    if (showMessage)
                        MessageBox.Show(lastErrorMessage, Label, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return valid;
        }

        private object editValue;
        public object EditValue {
            get {
                return editValue;
            }
            set {
                editValue = value;
                EditValueChange(editValue);
            }
        }

        public bool ValidateValue() {
            return DoValidateValue(this);
        }

        public event EventHandler<ValidateEventArgs> OnValidatingValue;
                
        protected virtual void EditValueChange(object value) {            
        }

        protected virtual object GetValue() {
            return EditValue;
        }

        public string Label {
            get {
                return getLabel();
            }
            set {
                setLabel(value);
            }
        }

        protected virtual string getLabel() {
            return string.Empty;
        }

        protected virtual void setLabel(string value) {
        }
    }
}
