using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUIWinForms {
    public partial class DbSpinEdit : DbEdit {
        public DbSpinEdit() {
            InitializeComponent();

			numericUpDown1.ValueChanged += HandleValueChanged;
        }

        void HandleValueChanged (object sender, EventArgs e)
        {
			EditValue = numericUpDown1.Value;
        }

        protected override string getLabel() {
            return label.Text;
        }

        protected override void setLabel(string value) {
            label.Text = value;
        }

        public decimal Value {
            get {
                return numericUpDown1.Value;
            }
            set {
                numericUpDown1.Value = value;
            }
        }

        public int DecimalPlaces {
            get {
                return numericUpDown1.DecimalPlaces;
            }
            set {
                numericUpDown1.DecimalPlaces = value;
            }
        }

        public decimal Increment {
            get {
                return numericUpDown1.Increment;
            }
            set {
                numericUpDown1.Increment = value;
            }
        }

        public decimal Minimum {
            get {
                return numericUpDown1.Minimum;
            }
            set {
                numericUpDown1.Minimum = value;
            }
        }

        public decimal Maximum {
            get {
                return numericUpDown1.Maximum;
            }
            set {
                numericUpDown1.Maximum = value;
            }
        }
    }
}
