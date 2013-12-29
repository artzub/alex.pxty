using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using db.DataAccess;
using db.Mapping;

namespace Parts {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            var conn = "User ID=PARTS;" +
				"Password=Zelda;" +
				"Data Source=(" +
				"DESCRIPTION=(" +
				"ADDRESS=(PROTOCOL=TCP)(HOST=172.22.3.128)(PORT=1521))" +
				"(CONNECT_DATA=(SERVER=DEDICATED)" +
				"(SERVICE_NAME=XE)))";
            OracleConnection.Instance.Initialize(conn);
            Provider.Initialize(OracleConnection.Instance);
			OracleConnection.Instance.Open ();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView2.AutoGenerateColumns = true;
            bindingSource2.DataSource = bindingSource1;
            bindingSource2.DataMember = "Stages";
            bindingSource1.DataSource = new SurfaceMapper().GetAll();
            OracleConnection.Instance.Close();
        }
    }
}
