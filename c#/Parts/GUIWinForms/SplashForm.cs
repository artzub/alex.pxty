using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUIWinForms {
    public partial class SplashForm : Form {
        public SplashForm() {
            InitializeComponent();
        }

        public int Max {
            get {
                return pb.Maximum;
            }
            set {
                pb.Maximum = value;
            }
        }

        public int Position {
            get {
                return pb.Value;
            }
            set {
                 pb.Value = value;
            }
        }

        public void Inc(int value = 1) {
            pb.Increment(value);
        }

        public string Label {
            get {
                return lb.Text;
            }
            set {
                lb.Text = value;
            }
        }
    }
}
