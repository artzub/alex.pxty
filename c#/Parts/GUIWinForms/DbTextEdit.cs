using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUIWinForms {
    public partial class DbTextEdit : DbEdit {
        public DbTextEdit() {
            InitializeComponent();
        }

        public string EditText {
            get {
                return textBox1.Text;
            }
            set {
                textBox1.Text = value;
            }
        }

        protected override string getLabel() {
            return label.Text;
        }

        protected override void setLabel(string value) {
            label.Text = value;
        }

        protected override void EditValueChange(object value) {
            EditText = string.Format("{0}", value);
        }

        protected override object GetValue() {
            return textBox1.Text;
        }
    }
}
