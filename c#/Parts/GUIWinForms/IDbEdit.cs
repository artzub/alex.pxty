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
		void ApplyValue();
        event EventHandler<ValidateEventArgs> OnValidatingValue;
		event EventHandler<ValidateEventArgs> OnApplyValue;

        string Label {
            get;
            set;
        }

        string LastValidationErrorMessage {
            get;
        }
    }
}
