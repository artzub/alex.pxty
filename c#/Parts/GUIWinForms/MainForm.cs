using System;
using System.Windows.Forms;
using Controller;
using Db;
using Db.Domains;

namespace GUIWinForms {
    public partial class MainForm : Form {
        DataManager dm;
        BindingSource curBs;
		DataGridView curDgv;

        public MainForm() {
            InitializeComponent();
			Shown += HandleShown;
			StartPosition = FormStartPosition.CenterScreen;
        }

        private void SetEnterHandle(Control c) {
            var dg = c as DataGridView;
            if (dg != null) {
                dg.Enter += HandleEnter;
            }
            else {
                foreach (var cc in c.Controls)
                    SetEnterHandle(cc as Control);
            }
        }
                
        void HandleShown (object sender, EventArgs e) {
			dm = DataManager.Instance;

			curBs = partBindingSource;
			curDgv = partDataGridView;

            SetEnterHandle(this);

			partDataGridView.Tag = Types.Part;
			stageDataGridView.Tag = Types.Stage;
			departamentDataGridView.Tag = Types.Departament;
			alloyDataGridView.Tag = Types.Alloy;
			surfaceDataGridView.Tag = Types.Surface;

            addAlloyToolStripMenuItem.Tag = Types.Alloy;
            addSurfaceToolStripMenuItem.Tag = Types.Surface;
            addPartToolStripMenuItem.Tag = Types.Part;
            addDepToolStripMenuItem.Tag = Types.Departament;
            addStageToolStripMenuItem.Tag = Types.Stage;

			tabControl1.SelectedIndexChanged += HandleTabIndexChanged;

			addToolStripButton.Click += editToolStripButton_Click;

			deleteToolStripButton.Click += HandleClick;

			//var tt = dm.Parts[0].Stages;
			try {
				partBindingSource.DataSource = dm.Parts;
				stageBindingSource.DataSource = dm.Stages;
				departamentBindingSource.DataSource = dm.Departaments;
				surfaceBindingSource.DataSource = dm.Surfaces;
				alloyBindingSource.DataSource = dm.Alloys;
			} catch (Exception ex) {
				ex.ShowError (this);
			}

			partBindingSource.PositionChanged += HandlePositionChanged;
			surfaceBindingSource.PositionChanged += HandlePositionChanged;
			stageBindingSource.PositionChanged += HandlePositionChanged;
			alloyBindingSource.PositionChanged += HandlePositionChanged;
			departamentBindingSource.PositionChanged += HandlePositionChanged;


			HandleTabIndexChanged (tabControl1, null);
			//HandleEnter (partDataGridView, null);
        }

        void HandlePositionChanged (object sender, EventArgs e) {
			ChangeButton(curDgv);

            var type = curDgv.Tag as Type;

            if(type == null)
                return;

            try {
                if (Types.Alloy.IsAssignableFrom(type)) {
                    try {
                        var item = (curBs.Current as Alloy);
                        if (item == null)
                            return;

                        //alloyPartsBindingSource.DataSource = item.Parts;

						dataGridView2.DataSource = item.Parts;

                    }
                    catch(Exception ex) {
						throw ex;
                    }
                }
                else if (Types.Surface.IsAssignableFrom(type)) {
                    try {
                        var item = (curBs.Current as Surface);
                        if (item == null)
                            return;

                        //sufaceStagesBindingSource.DataSource = item.Stages;
						dataGridView3.DataSource = item.Stages;
                    }
                    finally {
                    }
                }
                else if (Types.Departament.IsAssignableFrom(type)) {
                    try {
                        var item = (curBs.Current as Departament);
                        if (item == null)
                            return;

					depStagePartDataGridView.DataSource = item.Stages;
                    }
                    finally {
                    }
                }
				else if (Types.Part.IsAssignableFrom(type)) {
					try {
						var item = (curBs.Current as Part);
						if (item == null)
							return;

						partStagesDataGridView.DataSource = item.Stages;
					}
					finally {
					}
				}
            }
            catch (Exception ex) {
                ex.ShowError(this);
            }
        }

		void HandleTabIndexChanged (object sender, EventArgs e) {
			var tc = sender as TabControl;
		    if (tc == null)
		        return;

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
			HandlePositionChanged (sender, e);
			//ChangeButton(curDgv);
        }

		void ChangeButton(DataGridView gv) {
		    var edit = false;
            var del = false;

			var add = curBs != null && gv != null && gv.Tag is Type;
			if (add) {
			    var item = curBs.Current as IDomain;
				edit = item != null && Convert.ToInt64(item.Id) > 1;
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
			/*get {
				return curBs.Current;
			}*/
			set {
				var index = curBs.IndexOf(value);
				if (index > -1 && index < curBs.Count) 
					curBs.Position = index;
			}
		}

        private void editToolStripButton_Click(object sender, EventArgs e) {
			var ev = new EventArgsEdit() {
				EditValue = editToolStripButton == sender ? curBs.Current : null,
				Type = curDgv.Tag as Type
			};
			EditRowController.sf_OnAdd (this, ev);
			if (ev.EditValue != null)
				Current = ev.EditValue;
        }

		void HandleClick (object sender, EventArgs e) {

			var del = curBs.Current;

			var i = curBs.List.IndexOf(del);

            curBs.CurrencyManager.Position = i > 1 ? i - 1 : 0;

			var ev = new EventArgsEdit() {
				EditValue = del,
				Type = curDgv.Tag as Type
			};

			EditRowController.sf_OnDelete(this, ev);
			if (ev.EditValue != null)
				curBs.CurrencyManager.Position = i;
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
				editToolStripButton_Click(sender, e);
            }
            finally {
                curBs = tcb;
                curDgv = tcdgv;
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(this,
                "© 2013 - 2014 alex.pxty",
                "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }
    }
}
