using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUIWinForms {
    public partial class SelectForm : Form {
        public SelectForm() {
            InitializeComponent();
        }

        public void InitColumns(IList<DataGridViewColumn> list) {
            dataGridMain.Columns.Clear();
            dataGridMain.Columns.AddRange(list.ToArray());
        }

        public object Current {
            get {
                return bsMain.Current;
            }
            set {
                var index = bsMain.IndexOf(value);
                if (index > -1)
                    bsMain.Position = index;
            }
        }

        public object DataSource {
            get {
                return bsMain.DataSource;
            }
            set {
                bsMain.DataSource = value;
            }
        }

        private void Selected() {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e) {
            Selected();
        }

        private void dataGridMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            Selected();
        }
    }
}
