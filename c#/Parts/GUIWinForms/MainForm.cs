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

        private void setEnterHandle(Control c) {
            var dg = c as DataGridView;
            if (dg != null) {
                dg.Enter += HandleEnter;
            }
            else {
                foreach (var cc in c.Controls)
                    setEnterHandle(cc as Control);
            }
        }
                
        void HandleShown (object sender, EventArgs e) {
			dm = DataManager.Instance;

			partBindingSource.PositionChanged += HandlePositionChanged;
			surfaceBindingSource.PositionChanged += HandlePositionChanged;
			stageBindingSource.PositionChanged += HandlePositionChanged;
			alloyBindingSource.PositionChanged += HandlePositionChanged;
			departamentBindingSource.PositionChanged += HandlePositionChanged;			

			curBs = partBindingSource;
			curDgv = partDataGridView;

            setEnterHandle(this);

			/*partDataGridView.Enter += HandleEnter;
			stageDataGridView.Enter += HandleEnter;
			departamentDataGridView.Enter += HandleEnter;
			alloyDataGridView.Enter += HandleEnter;
			surfaceDataGridView.Enter += HandleEnter;

			partStagesDataGridView.Enter += HandleEnter;
			partsDataGridView1.Enter += HandleEnter;
			dataGridView2.Enter += HandleEnter;
			dataGridView3.Enter += HandleEnter;*/

			partDataGridView.Tag = Types.Part;
			stageDataGridView.Tag = Types.Stage;
			departamentDataGridView.Tag = Types.Departament;
			alloyDataGridView.Tag = Types.Alloy;
			surfaceDataGridView.Tag = Types.Surface;

            addAlloyToolStripMenuItem.Tag = Types.Alloy;
            addSurfaceToolStripMenuItem.Tag = Types.Surface;
            addPartToolStripMenuItem.Tag = Types.Part;
            addDepToolStripMenuItem.Tag = Types.Departament;
            addSurfaceToolStripMenuItem.Tag = Types.Stage;

			tabControl1.SelectedIndexChanged += HandleTabIndexChanged;

			addToolStripButton.Click += editToolStripButton_Click;

			deleteToolStripButton.Click += HandleClick;
			tabControl1.SelectTab(tabPage1);
			//HandleTabIndexChanged (tabControl1, null);
			//HandleEnter (partDataGridView, null);

            partBindingSource.DataSource = dm.Parts;
            stageBindingSource.DataSource = dm.Stages;
            departamentBindingSource.DataSource = dm.Departaments;
            surfaceBindingSource.DataSource = dm.Surfaces;
            alloyBindingSource.DataSource = dm.Alloies;
        }

        void HandlePositionChanged (object sender, EventArgs e) {
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
					if (stageDataGridView.CanFocus)
						stageDataGridView.Focus();
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

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e) {
            var item = sender as ToolStripMenuItem;
            if (item == null)
                return;

            var curType = item.Tag as Type;
            if (curType == null)
                return;

            var tcb = curBs;
            var tcdgv = curDgv;
            try {
                if (Types.Alloy.IsAssignableFrom(curType)) {
                    curBs = alloyBindingSource;
                    curDgv = alloyDataGridView;
                }
                else if (Types.Surface.IsAssignableFrom(curType)) {
                    curBs = surfaceBindingSource;
                    curDgv = surfaceDataGridView;
                }
                else if (Types.Part.IsAssignableFrom(curType)) {
                    curBs = partBindingSource;
                    curDgv = partDataGridView;
                }
                else if (Types.Departament.IsAssignableFrom(curType)) {
                    curBs = departamentBindingSource;
                    curDgv = departamentDataGridView;
                }
                else if (Types.Stage.IsAssignableFrom(curType)) {
                    curBs = stageBindingSource;
                    curDgv = stageDataGridView;
                }
            }
            finally {
                curBs = tcb;
                curDgv = tcdgv;
            }

        }
    }
}
