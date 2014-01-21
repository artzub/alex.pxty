using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUIWinForms {
    public interface IDbEdit {
        object EditValue {
            get;
            set;
        }
        bool ValidateValue();
        event EventHandler<ValidateEventArgs> OnValidatingValue;
        string Label {
            get;
            set;
        }

        string LastValidationErrorMessage {
            get;
        }
    }
}
