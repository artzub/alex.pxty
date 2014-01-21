using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db;
using Db.Domains;
using System.Windows.Forms;

namespace GUIWinForms {
    public static class EditRowController {
        public static IList<DbEdit> GetRowEditors(object obj) {

            var list = new List<DbEdit>();

            if (obj == null)
                return list;

            var type = obj.GetType();

            if (obj is INamed) {
                var te = new DbTextEdit() {
                    Label = "Наименование:",
                    EditValue = (obj as INamed).Name,
                    Width = 300
                };
                te.OnValidatingValue += new EventHandler<ValidateEventArgs>(te_OnValidatingValue);
                
                list.Add(te);
            }

            if (Types.Departament.IsAssignableFrom(type)) {
                try {
                    var item = obj as Departament;

                    list.Add(new DbSpinEdit() {
                        Label = "Номер:",
                        Value = item.Num,
                        Minimum = 0,
                        EditValue = item.Num,
                        Width = 300
                    });
                    var be = new DbButtonsEdit() {
                        Dock = System.Windows.Forms.DockStyle.Top,
                        Label = "Цех:",
                        EditValue = item.TypeDep ?? TypeDep.Default,
                        Tag = Types.TypeDep
                    };
                    be.AddButton();

                    be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
                    be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);

                    list.Add(be);
                }
                finally {
                }
            }
            else if (Types.Part.IsAssignableFrom(type)) {
                var item = obj as Part;

                var be = new DbButtonsEdit() {
                    Dock = System.Windows.Forms.DockStyle.Top,
                    Label = "Сплав:",
                    EditValue = item.Alloy ?? Alloy.Default,
                    Tag = Types.Alloy
                };
                be.AddButton();
                be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
                be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);
                list.Add(be);

                list.Add(new DbSpinEdit() {
                    Dock = System.Windows.Forms.DockStyle.Top,
                    Label = "Цена:",
                    Value = item.Cost,
                    Minimum = 0,
                    DecimalPlaces = 2,
                    EditValue = item.Cost
                });
            }
            else if (Types.Stage.IsAssignableFrom(type)) {
                var item = obj as Stage;

                var be = new DbButtonsEdit() {
                    Width = 300,
                    Label = "Деталь:",
                    EditValue = item.Part,
                    Tag = Types.Part
                };
                be.AddButton();
                be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
                be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);
                list.Add(be);

                be = new DbButtonsEdit() {
                    Dock = System.Windows.Forms.DockStyle.Top,
                    Label = "Поверхность:",
                    EditValue = item.Surface ?? Surface.Default,
                    Tag = Types.Surface
                };                
                be.AddButton();
                be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
                be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);
                list.Add(be);

                be = new DbButtonsEdit() {
                    Dock = System.Windows.Forms.DockStyle.Top,
                    Label = "Предыдущий этап:",
                    EditValue = item.StagePrev ?? Stage.Default,
                    Tag = Types.Stage
                };
                be.AddButton();
                be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
                be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);
                list.Add(be);

                be = new DbButtonsEdit() {
                    Dock = System.Windows.Forms.DockStyle.Top,
                    Label = "Следующий этап:",
                    EditValue = item.StageNext ?? Stage.Default,
                    Tag = Types.Stage
                };
                be.AddButton();
                be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
                be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);
                list.Add(be);
            }

            return list;
        }

        public static IList<DataGridViewColumn> GetDataColumns(Type type) {
            var list = new List<DataGridViewColumn>();

            if (type == null)
                return list;

            if (Types.Domain.IsAssignableFrom(type)) {
                list.Add(new DataGridViewTextBoxColumn() {
                    DataPropertyName = "Id",
                    HeaderText = "Id"
                });
            }

            if (Types.Named.IsAssignableFrom(type)) {
                list.Add(new DataGridViewTextBoxColumn() {
                    DataPropertyName = "Name",
                    HeaderText = "Наименование"
                });
            }

            if (Types.Departament.IsAssignableFrom(type)) {
                list.Add(new DataGridViewTextBoxColumn() {
                    DataPropertyName = "Num",
                    HeaderText = "Номер"
                });
                list.Add(new DataGridViewTextBoxColumn() {
                    DataPropertyName = "TypeDep",
                    HeaderText = "Цех"
                });
            }
            else if (Types.Departament.IsAssignableFrom(type)) {
                list.Add(new DataGridViewTextBoxColumn() {
                    DataPropertyName = "Сплав",
                    HeaderText = "Alloy"
                });
            }

            return list;
        }

        private static void be_ButtonClick(object sender, EventArgsButtonsClick e) {
            using (var sf = new SelectForm()) {
                var de = sender as DbEdit;                
                if (de == null)
                    return;

                var curType = de.Tag as Type;

                sf.InitColumns(GetDataColumns(curType));
                sf.DataSource = Controller.DataManager.Instance.GetAllByType(curType);
                sf.Current = de.EditValue;

                if (sf.ShowDialog((sender as Control)) == DialogResult.OK) {
                    de.EditValue = sf.Current ?? de.EditValue;
                }
            }
        }

        private static void be_OnValidatingValue(object sender, ValidateEventArgs e) {
            e.Cancel = e.EditValue == null;
            if (e.Cancel)
                e.Message = "Значение не может быть пустым!";
        }

        private static void te_OnValidatingValue(object sender, ValidateEventArgs e) {
            e.Cancel = string.IsNullOrWhiteSpace(string.Format("{0}", e.Value));
            if (e.Cancel)
                e.Message = "Значение не может быть пустым!";
        }
    }
}
