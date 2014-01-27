using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUIWinForms {
    public partial class DbComboBox : DbEdit {
        public DbComboBox() {
            InitializeComponent();
        }

        public string EditText {
            get {
                return comboBox1.Text;
            }
            set {
                comboBox1.Text = value;
            }
        }

        public int SelectedIndex {
            get {
                return comboBox1.SelectedIndex;
            }
            set {
                comboBox1.SelectedIndex = value;
            }
        }

        public object SelectedItem {
            get {
                return comboBox1.SelectedItem;
            }
            set {
                comboBox1.SelectedItem = value;
            }
        }

        public System.Windows.Forms.ComboBox.ObjectCollection Items {
            get {
                return comboBox1.Items;
            }
        }

        public object DataSource {
            get {
                return comboBox1.DataSource;
            }
            set {
                comboBox1.DataSource = value;
            }
        }

        public string DisplayMember {
            get {
                return comboBox1.DisplayMember;
            }
            set {
                comboBox1.DisplayMember = value;
            }
        }

        protected override string getLabel() {
            return label.Text;
        }

        protected override void setLabel(string value) {
            label.Text = value;
        }
    }
}
