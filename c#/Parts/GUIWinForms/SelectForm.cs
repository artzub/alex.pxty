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

        public Type CurrentType {
            get {
                Type type = null;
                if (Current != null)
                    type = Current.GetType();
                return type;
            }
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

        public event EventHandler<EventArgsEdit> OnEdit;
        public event EventHandler<EventArgsEdit> OnAdd;
        public event EventHandler<EventArgsEdit> OnDelete;

        private void toolStripButton1_Click(object sender, EventArgs e) {
            if (OnAdd != null) {
                var ee = new EventArgsEdit() {
                    Type = CurrentType
                };
                OnAdd(this, ee);
                if (ee.EditValue != null) {
                    Current = ee.EditValue;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e) {
            if (OnEdit != null) {
                var ee = new EventArgsEdit() {
                    EditValue = Current,
                    Type = CurrentType
                };
                OnEdit(this, ee);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e) {
            if (OnDelete != null) {
                var ee = new EventArgsEdit() {
                    EditValue = Current,
                    Type = CurrentType
                };
                OnDelete(this, ee);
            }
        }
    }

    public class EventArgsEdit : EventArgs {
        public object EditValue {
            get;
            set;
        }

        public Type Type {
            get;
            set;
        }
    }
}
