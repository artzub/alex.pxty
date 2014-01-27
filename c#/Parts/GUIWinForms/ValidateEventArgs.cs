using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUIWinForms {
    public class ValidateEventArgs : System.ComponentModel.CancelEventArgs {
        public object Value {
            get;
            set;
        }

        public object EditValue {
            get;
            set;
        }

        public string Message {
            get;
            set;
        }

        public ValidateEventArgs(object value, object editValue) : base(false) {
            Value = value;
            EditValue = editValue;
        }
    }
}
