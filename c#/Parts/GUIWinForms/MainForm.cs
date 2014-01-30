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

        private void editToolStripButton_Click(object sender, EventArgs e) {
            EditRowController.Edit(curBs.Current, Types.Part);
        }
    }
}
