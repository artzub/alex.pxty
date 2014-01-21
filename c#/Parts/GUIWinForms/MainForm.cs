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
            dm = new DataManager();

            /*surfaceBindingSource.DataSource = dm.Surfaces;
            stageBindingSource.DataSource = dm.Stages;
            partBindingSource.DataSource = dm.Parts;
            alloyBindingSource.DataSource = dm.Alloies;
            //departamentBindingSource.DataSource = dm.Departaments;

            curBs = partBindingSource;*/
        }

        private void Edit(object obj = null, Type type = null) {
            var item = obj as IDomain;
            if (item == null && type == null)
                return;

            if (type == null)
                type = item.GetType();

            if (type != item.GetType())
                return;
        }

        private void editToolStripButton_Click(object sender, EventArgs e) {
            //Edit(curBs.Current);

            using (var ef = new EditForm(dm.GetNewStage())) {
                if (ef.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    MessageBox.Show("Test");
            }
        }

        void dbbe_ButtonClick(object sender, EventArgsButtonsClick e) {
            MessageBox.Show(string.Format("{0}: {1}", e.IndexButton, e.Type));
        }
    }
}
