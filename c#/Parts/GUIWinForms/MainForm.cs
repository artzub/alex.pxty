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

        public MainForm() {
            InitializeComponent();
            dm = DataManager.Instance;

            /*surfaceBindingSource.DataSource = dm.Surfaces;
            stageBindingSource.DataSource = dm.Stages;
            partBindingSource.DataSource = dm.Parts;
            alloyBindingSource.DataSource = dm.Alloies;
            //departamentBindingSource.DataSource = dm.Departaments;*/

            curBs = partBindingSource;
        }

        private void Edit(object obj = null, Type type = null) {
			if (type == null && obj == null)
				return;
            
            if (type == null)
                type = obj.GetType();

			if (obj == null)
				obj = dm.GetNewItem(type);

            using (var ef = new EditForm(obj)) {
				if (ef.ShowDialog (this) == System.Windows.Forms.DialogResult.OK)
					try {
						dm.Save (ef.EditValue as IDomain);	
					} catch (Exception ex) {
						MessageBox.Show (ex.Message);
					}
            }
        }

        private void editToolStripButton_Click(object sender, EventArgs e) {
            Edit(curBs.Current, Types.Part);
        }
    }
}
