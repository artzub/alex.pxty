using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUIWinForms {
    public partial class DbButtonsEdit : DbEdit {
        public DbButtonsEdit() {
            InitializeComponent();
        }

        public string EditText {
            get {
                return textBox1.Text;
            }
            set {
                textBox1.Text = value;
            }
        }

        protected override string getLabel() {
            return label.Text;
        }

        protected override void setLabel(string value) {
            label.Text = value;
        }

        protected override void EditValueChange(object value) {
            EditText = string.Format("{0}", value);
        }

        private Dictionary<Button, TypeButton> buttonsTypes;
        private Dictionary<Button, TypeButton> ButtonsTypes {
            get {
                if (buttonsTypes == null)
                    buttonsTypes = new Dictionary<Button,TypeButton>();
                return buttonsTypes;
            }
        }

        public Button AddButton(TypeButton type = TypeButton.Ellipsis, string label = default(string)) {
            var but = new Button();

            but.AutoSize = true;
            but.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            but.Click +=new EventHandler(but_Click);
            but.Margin = new Padding(0);
            //but.Dock = DockStyle.Left;

            ButtonsTypes[but] = type;

            if (string.IsNullOrWhiteSpace(label))
                switch (type) {
                    case TypeButton.Ellipsis:
                        label = "...";
                        break;
                    case TypeButton.Plus:
                        label = "+";
                        break;
                    case TypeButton.Minus:
                        label = "–";
                        break;
                    case TypeButton.Up:
                        label = "▲";
                        break;
                    case TypeButton.Down:
                        label = "▼";
                        break;
                    case TypeButton.Right:
                        label = "►";
                        break;
                    case TypeButton.Left:
                        label = "◄";
                        break;
                }
            but.Text = label;

            flowLayoutPanel1.Controls.Add(but);

            return but;
        }

        void but_Click(object sender, EventArgs e) {

            if (ButtonClick == null)
                return;

            var ea = new EventArgsButtonsClick() {
                Button = sender as Button
            };

            ea.IndexButton = flowLayoutPanel1.Controls.IndexOf(ea.Button);
            ea.Type = ButtonsTypes[ea.Button];

            ButtonClick(this, ea);
        }

        public event EventHandler<EventArgsButtonsClick> ButtonClick;


    }

    public enum TypeButton {
        Ellipsis,
        Plus,
        Minus,
        Up,
        Down,
        Right,
        Left
    }

    public class EventArgsButtonsClick : EventArgs {
        public Button Button {
            get;
            set;
        }

        public int IndexButton {
            get;
            set;
        }

        public TypeButton Type {
            get;
            set;
        }
    }
}
