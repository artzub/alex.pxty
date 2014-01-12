using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controller;

namespace GUIWinForms {
    public partial class MainForm : Form {
        DataManager dm;

        public MainForm() {
            InitializeComponent();
            dm = new DataManager();

            surfaceBindingSource.DataSource = dm.Surfaces;
            stageBindingSource.DataSource = dm.Stages;
            partBindingSource.DataSource = dm.Parts;
            alloyBindingSource.DataSource = dm.Alloies;
            //departamentBindingSource.DataSource = dm.Departaments;
        }
    }
}
