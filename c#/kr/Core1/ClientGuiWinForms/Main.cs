using Core;
using Core.Tcp;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Winforms {
	public class Main : Form {

		/// <summary>
		/// Шаг прогресси
		/// </summary>
	    private int step = 0;

		/// <summary>
		/// Этап 
		/// </summary>
	    private int stage = 0;
		//Форма Winforms 
		#region Initialization
	    private TableLayoutPanel tableLayoutPanel1;
	    private Label label1;
	    private Label label2;
	    private TextBox textBox1;
	    private TextBox textBox2;
	    private Button button1;
	    private Label label3;
	    private TextBox textBox3;
	    private Label label4;
	    private Button button2;
	    private Label label5;
	    private Button button3;
	    private Button button4;
	    private Button button5;
	    private ListView listView1;
	    private NumericUpDown numericUpDown1;
	    private NumericUpDown numericUpDown2;
	    private Client client;
	    private BackgroundWorker bg;
		public Main() {		
			this.InitializeComponent();
		    this.bg = new BackgroundWorker();
		    this.bg.WorkerReportsProgress = true;
		    this.bg.WorkerSupportsCancellation = true;
		    this.bg.ProgressChanged += new ProgressChangedEventHandler(this.HandleProgressChanged);
		    this.bg.DoWork += new DoWorkEventHandler(this.HandleDoWork);
		    this.numericUpDown1.CausesValidation = false;
		    this.numericUpDown2.CausesValidation = false;
		    this.button1.Click += new EventHandler(this.clickConnect);
		    this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
		    this.numericUpDown2.ValueChanged += new EventHandler(this.HandleValueChanged);
		    this.button2.Click += new EventHandler(this.HandleClick);
		    this.button3.Click += new EventHandler(this.HandleClick);
		    this.button4.Click += new EventHandler(this.HandleClick);
		    this.button5.Click += new EventHandler(this.HandleClick);
		}

		private void InitializeComponent(){
			this.tableLayoutPanel1 = new TableLayoutPanel();
		    this.label1 = new Label();
		    this.label2 = new Label();
		    this.textBox1 = new TextBox();
		    this.textBox2 = new TextBox();
			this.button1 = new Button();
			this.label3 = new Label();
			this.textBox3 = new TextBox();
			this.label4 = new Label();
			this.button2 = new Button();
			this.label5 = new Label();
			this.button3 = new Button();
			this.button4 = new Button();
			this.button5 = new Button();
			this.listView1 = new ListView();
			this.numericUpDown1 = new NumericUpDown();
			this.numericUpDown2 = new NumericUpDown();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			this.tableLayoutPanel1.ColumnCount = 6;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add((Control) this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add((Control) this.label2, 2, 0);
			this.tableLayoutPanel1.Controls.Add((Control) this.textBox1, 1, 0);
			this.tableLayoutPanel1.Controls.Add((Control) this.textBox2, 3, 0);
			this.tableLayoutPanel1.Controls.Add((Control) this.button1, 4, 0);
			this.tableLayoutPanel1.Controls.Add((Control) this.label3, 5, 0);
			this.tableLayoutPanel1.Controls.Add((Control) this.textBox3, 5, 1);
			this.tableLayoutPanel1.Controls.Add((Control) this.label4, 0, 1);
			this.tableLayoutPanel1.Controls.Add((Control) this.numericUpDown2, 1, 1);
			this.tableLayoutPanel1.Controls.Add((Control) this.button2, 2, 1);
			this.tableLayoutPanel1.Controls.Add((Control) this.numericUpDown1, 1, 2);
			this.tableLayoutPanel1.Controls.Add((Control) this.label5, 0, 2);
			this.tableLayoutPanel1.Controls.Add((Control) this.button3, 2, 2);
			this.tableLayoutPanel1.Controls.Add((Control) this.button4, 4, 3);
			this.tableLayoutPanel1.Controls.Add((Control) this.button5, 4, 5);
			this.tableLayoutPanel1.Controls.Add((Control) this.listView1, 0, 3);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 28f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 28f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 28f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 28f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 58f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new Size(719, 254);
			this.tableLayoutPanel1.TabIndex = 0;
			this.label1.Dock = DockStyle.Fill;
			this.label1.Location = new Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(44, 28);
			this.label1.TabIndex = 0;
			this.label1.Text = "host:";
			this.label1.TextAlign = ContentAlignment.MiddleCenter;
			this.label2.Dock = DockStyle.Fill;
			this.label2.Location = new Point(203, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(44, 28);
			this.label2.TabIndex = 1;
			this.label2.Text = "port:";
			this.label2.TextAlign = ContentAlignment.MiddleCenter;
			this.textBox1.Dock = DockStyle.Fill;
			this.textBox1.Location = new Point(53, 3);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(144, 20);
			this.textBox1.TabIndex = 2;
			this.textBox2.Dock = DockStyle.Fill;
			this.textBox2.Location = new Point(253, 3);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new Size(144, 20);
			this.textBox2.TabIndex = 3;
			this.button1.Dock = DockStyle.Fill;
			this.button1.Enabled = false;
			this.button1.Location = new Point(403, 3);
			this.button1.Name = "button1";
			this.button1.Size = new Size(74, 22);
			this.button1.TabIndex = 4;
			this.button1.Text = "Connect";
			this.button1.UseVisualStyleBackColor = true;
			this.label3.Dock = DockStyle.Fill;
			this.label3.Location = new Point(483, 0);
			this.label3.Name = "label3";
			this.label3.Size = new Size(233, 28);
			this.label3.TabIndex = 5;
			this.label3.Text = "Server Client Log";
			this.label3.TextAlign = ContentAlignment.MiddleCenter;
			this.textBox3.Dock = DockStyle.Fill;
			this.textBox3.Location = new Point(483, 31);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.tableLayoutPanel1.SetRowSpan((Control) this.textBox3, 6);
			this.textBox3.Size = new Size(233, 220);
			this.textBox3.TabIndex = 6;
			this.label4.Dock = DockStyle.Fill;
			this.label4.Location = new Point(3, 28);
			this.label4.Name = "label4";
			this.label4.Size = new Size(44, 28);
			this.label4.TabIndex = 7;
			this.label4.Text = "Step:";
			this.label4.TextAlign = ContentAlignment.MiddleCenter;
			this.button2.Dock = DockStyle.Fill;
			this.button2.Enabled = false;
			this.button2.Location = new Point(203, 31);
			this.button2.Name = "button2";
			this.button2.Size = new Size(44, 22);
			this.button2.TabIndex = 9;
			this.button2.Text = "Init";
			this.button2.UseVisualStyleBackColor = true;
			this.label5.AutoSize = true;
			this.label5.Dock = DockStyle.Fill;
			this.label5.Location = new Point(3, 56);
			this.label5.Name = "label5";
			this.label5.Size = new Size(44, 28);
			this.label5.TabIndex = 11;
			this.label5.Text = "Item:";
			this.label5.TextAlign = ContentAlignment.MiddleCenter;
			this.button3.Dock = DockStyle.Fill;
			this.button3.Enabled = false;
			this.button3.Location = new Point(203, 59);
			this.button3.Name = "button3";
			this.button3.Size = new Size(44, 22);
			this.button3.TabIndex = 12;
			this.button3.Text = "Add";
			this.button3.UseVisualStyleBackColor = true;
			this.button4.Enabled = false;
			this.button4.Location = new Point(403, 87);
			this.button4.Name = "button4";
			this.button4.Size = new Size(74, 22);
			this.button4.TabIndex = 13;
			this.button4.Text = "Send";
			this.button4.UseVisualStyleBackColor = true;
			this.button5.Enabled = false;
			this.button5.Location = new Point(403, 173);
			this.button5.Name = "button5";
			this.button5.Size = new Size(74, 23);
			this.button5.TabIndex = 14;
			this.button5.Text = "Run";
			this.button5.UseVisualStyleBackColor = true;
			this.tableLayoutPanel1.SetColumnSpan((Control) this.listView1, 4);
			this.listView1.Dock = DockStyle.Fill;
			this.listView1.Enabled = true;
			this.listView1.Location = new Point(3, 87);
			this.listView1.Name = "listView1";
			this.tableLayoutPanel1.SetRowSpan((Control) this.listView1, 2);
			this.listView1.Size = new Size(394, 80);
			this.listView1.TabIndex = 15;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = View.List;
			this.numericUpDown1.Dock = DockStyle.Fill;
			this.numericUpDown1.Location = new Point(53, 59);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new Size(144, 20);
			this.numericUpDown1.TabIndex = 17;
			this.numericUpDown1.Enabled = false;
			this.numericUpDown2.Dock = DockStyle.Fill;
			this.numericUpDown2.Location = new Point(53, 31);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new Size(144, 20);
			this.numericUpDown2.TabIndex = 18;
			this.numericUpDown2.Maximum = int.MaxValue;
			this.numericUpDown2.Minimum = int.MinValue;
			this.numericUpDown2.Enabled = false;
			this.AutoScaleDimensions = new SizeF(6f, 13f);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(719, 254);
			this.Controls.Add((Control) this.tableLayoutPanel1);
			this.Name = "Main";
			this.Text = "Client";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
		}
		#endregion
		// Обрабатываем нажатие кнопок
		private void HandleClick(object sender, EventArgs e) {
			stage = -1;
			if (sender == this.button2)
        	stage = 1;
      		else if (sender == this.button3)
				stage = 2;
			else if (sender == this.button4)
				stage = 3;
			else if (sender == this.button5)
				stage = 4;
			switch (stage) {
			case 1:
				this.client.SendText(":0:" + this.step.ToString());
				this.button2.Enabled = false;
				break;
			case 2:
				if (this.listView1.Items.Count > 0 && (!Searcher.Validate(Convert.ToInt32(this.listView1.Items.Count <= 0 ? this.numericUpDown1.Value.ToString() : this.listView1.Items[0].Text), this.step, Convert.ToInt32(this.numericUpDown1.Value)) || this.listView1.Items[this.listView1.Items.Count - 1].Text == this.numericUpDown1.Value.ToString())) {
					MessageBox.Show(string.Format("This value isn't correct. Correct is {0}", (object) (Convert.ToInt32(this.listView1.Items[this.listView1.Items.Count - 1].Text) + this.step)));
					break;
				}
				else {
					listView1.Items.Add(this.numericUpDown1.Value.ToString());
            		numericUpDown1.Minimum = Convert.ToInt32(this.numericUpDown1.Value);
            		this.button4.Enabled = this.listView1.Items.Count > 0;
					break;
				}
			case 3:
				string str = "";
				IEnumerator enumerator = this.listView1.Items.GetEnumerator();
				try {
					while (enumerator.MoveNext()) {
						ListViewItem listViewItem = (ListViewItem) enumerator.Current;
              			str = str + listViewItem.Text + ",";
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = enumerator as IDisposable) != null)
						disposable.Dispose();
				}
				client.SendText(string.Format(":1:{0}", (object) str));
          		break;
			case 4:
				client.SendText(":2:");
				break;
			}
		}
		private void HandleValueChanged(object sender, EventArgs e) {
			if (step == Convert.ToInt32(this.numericUpDown2.Value))
				return;
			button2.Enabled = true;
      		listView1.Items.Clear();
      		step = Convert.ToInt32(this.numericUpDown2.Value);
		}
		private void AppendLog(object obj) {
			textBox3.Text = string.Format("{0}\r\n{1}", (object) this.textBox3.Text, obj);
		}
		private void textBox1_TextChanged(object sender, EventArgs e) {
			button1.Enabled = !string.IsNullOrEmpty(this.textBox1.Text);
		}
		private void clickConnect(object sndr, EventArgs e) {
			int result = 80;
			if (string.IsNullOrEmpty(this.textBox2.Text))
				this.textBox2.Text = "80";
      		if (!int.TryParse(this.textBox2.Text, out result))
				return;
			this.client = new Client(this.textBox1.Text, result);
      		if (!client.Connect())
				return;
      		AppendLog(string.Format("Connected to {0}", this.client.SocketEndPoint));
      		bg.RunWorkerAsync();
			button1.Enabled = false;
			numericUpDown2.Enabled = true;
			numericUpDown2.Focus();
		}
		private void HandleDoWork(object sender, DoWorkEventArgs e) {
//			string str1= string.Empty;
            while (client != null && this.client.Connected) {
				string str2 = client.Receive();
        		if (!string.IsNullOrEmpty(str2))
					bg.ReportProgress(0,str2);
			}
		}
		private void HandleProgressChanged (object sender, ProgressChangedEventArgs e)
		{
			string str = e.UserState.ToString ();
			this.AppendLog ((object)str);
			if (str == "1") {
				this.AppendLog ("Error: Seacher is not initialized");
			} 
			else {
				switch (stage) {
				case 1:
					numericUpDown1.Enabled = true;
					numericUpDown1.Maximum = step < 0 ? int.MinValue : int.MaxValue;
					numericUpDown1.Minimum = step < 0 ? int.MaxValue : int.MinValue;
					numericUpDown1.Increment = step;
					button3.Enabled = true;
					numericUpDown1.Focus ();
					break;
				case 3:
					button5.Enabled = true;
					break;
				}
			}
		}
	}
}