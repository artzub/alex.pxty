using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controller;
using Db;

namespace GUIWinForms {
    public partial class MainForm : Form {
        DataManager dm;
        BindingSource curBs;
		DataGridView curDgv;

        public MainForm() {
            InitializeComponent();
			this.Shown += HandleShown;
        }

        void HandleShown (object sender, EventArgs e) {
			dm = DataManager.Instance;

			partBindingSource.PositionChanged += HandlePositionChanged;
			surfaceBindingSource.PositionChanged += HandlePositionChanged;
			stageBindingSource.PositionChanged += HandlePositionChanged;
			alloyBindingSource.PositionChanged += HandlePositionChanged;
			departamentBindingSource.PositionChanged += HandlePositionChanged;

			surfaceBindingSource.DataSource = dm.Surfaces;

			partBindingSource.DataSource = dm.Parts;
			/*stageBindingSource.DataSource = partBindingSource;
			stageBindingSource.DataMember = "Stages";
*/
			alloyBindingSource.DataSource = dm.Alloies;
			departamentBindingSource.DataSource = dm.Departaments;

			curBs = partBindingSource;
			curDgv = partDataGridView;

			partDataGridView.Enter += HandleEnter;
			dataGridView1.Enter += HandleEnter;
			departamentDataGridView.Enter += HandleEnter;
			alloyDataGridView.Enter += HandleEnter;
			surfaceDataGridView.Enter += HandleEnter;

			stageDataGridView.Enter += HandleEnter;
			partDataGridView1.Enter += HandleEnter;
			dataGridView2.Enter += HandleEnter;
			dataGridView3.Enter += HandleEnter;

			partDataGridView.Tag = Types.Part;
			dataGridView1.Tag = Types.Stage;
			departamentDataGridView.Tag = Types.Departament;
			alloyDataGridView.Tag = Types.Alloy;
			surfaceDataGridView.Tag = Types.Surface;

			tabControl1.SelectedIndexChanged += HandleTabIndexChanged;

			addToolStripButton.Click += editToolStripButton_Click;

			deleteToolStripButton.Click += HandleClick;
			tabControl1.SelectTab(tabPage1);
			//HandleTabIndexChanged (tabControl1, null);
			//HandleEnter (partDataGridView, null);
        	
        }

        void HandlePositionChanged (object sender, EventArgs e)
        {
			ChangeButton(curDgv);
        }

		void HandleTabIndexChanged (object sender, EventArgs e) {
			var tc = sender as TabControl;
			switch (tc.SelectedIndex) {
				case 0:	
					if (partDataGridView.CanFocus)
						partDataGridView.Focus();
					break;
				case 1:	
					if (dataGridView1.CanFocus)
						dataGridView1.Focus();
					break;
				case 2:	
					if (departamentDataGridView.CanFocus)
						departamentDataGridView.Focus();
					break;
				case 3:	
					if (alloyDataGridView.CanFocus)
						alloyDataGridView.Focus();
					break;
				case 4:	
					if (surfaceDataGridView.CanFocus)
						surfaceDataGridView.Focus();
					break;
			}        	
        }

        void HandleEnter (object sender, EventArgs e) {
			curDgv = sender as DataGridView;
			curBs = curDgv.DataSource as BindingSource;
			ChangeButton(curDgv);
        }

		void ChangeButton(DataGridView gv) {
			var add = false;
			var edit = false;
			var del = false;

			add = curBs != null && gv != null && gv.Tag is Type;
			if (add) {
				edit = curBs.Current != null && !(curBs.Current as IDomain).Id.Equals((decimal)1);
				del = edit;
			}

			addToolStripButton.Enabled = add;
			addToolStripMenuItem.Enabled = add;

			editToolStripButton.Enabled = edit;
			editToolStripMenuItem.Enabled = edit;

			deleteToolStripButton.Enabled = del;
			deleteToolStripMenuItem.Enabled = del;
		}

		object Current {
			get {
				return curBs.Current;
			}
			set {
				var index = curBs.IndexOf(value);
				if (index > -1)
					curBs.Position = index;
			}
		}

        private void editToolStripButton_Click(object sender, EventArgs e) {
			var ev = new EventArgsEdit () {
				EditValue = editToolStripButton == sender ? curBs.Current : null,
				Type = curDgv.Tag as Type
			};
			EditRowController.sf_OnAdd (this, ev);
			Current = ev.EditValue;
        }

		void HandleClick (object sender, EventArgs e) {
			EditRowController.sf_OnDelete(this, new EventArgsEdit () {
				EditValue = curBs.Current,
				Type = curDgv.Tag as Type
			});
		}
    }
}
