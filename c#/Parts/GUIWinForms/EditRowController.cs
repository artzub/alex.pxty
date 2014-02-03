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
                // используем замыкание переменной.
				te.OnApplyValue += (object sender, ValidateEventArgs e) => {
					(obj as INamed).Name = string.Format("{0}", e.Value);
				};
                te.OnValidatingValue += new EventHandler<ValidateEventArgs>(te_OnValidatingValue);
                
                list.Add(te);
            }

            if (Types.Departament.IsAssignableFrom(type)) {
                try {
                    var item = obj as Departament;
                    var se = new DbSpinEdit() {
                        Label = "Номер:",
                        Value = item.Num,
                        Minimum = 0,
                        EditValue = item.Num,
                        Width = 300 
                    };

                    se.OnApplyValue += (object sender, ValidateEventArgs e) => {
                        item.Num = Convert.ToInt64(e.EditValue);
                    };
                    list.Add(se);

                    var be = new DbButtonsEdit() {
                        //Dock = System.Windows.Forms.DockStyle.Top,
                        Label = "Цех:",
                        EditValue = item.TypeDep ?? TypeDep.Default,
                        Tag = Types.TypeDep,
						Width = 300
                    };
                    be.AddButton();

					be.OnApplyValue += (object sender, ValidateEventArgs e) => {
						item.TypeDep = e.EditValue as TypeDep;
					};
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
					Width = 300,
                    Label = "Сплав:",
                    EditValue = item.Alloy ?? Alloy.Default,
                    Tag = Types.Alloy
                };
                be.AddButton();

                be.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    item.Alloy = e.EditValue as Alloy;
                };
                be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
                be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);
                list.Add(be);

                var se = new DbSpinEdit() {
                    Width = 300,
                    Label = "Цена:",
                    Value = item.Cost,
                    Minimum = 0,
                    DecimalPlaces = 2,
                    EditValue = item.Cost
                };

                se.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    item.Cost = Convert.ToInt64(e.EditValue);
                };

                list.Add(se);

				var te = new DbTextEdit() {
					Label = "БЛ номер:",
					EditValue = item.BLNumber,
					Width = 300
				};
				// используем замыкание переменной.
				te.OnApplyValue += (object sender, ValidateEventArgs e) => {
					item.BLNumber = string.Format("{0}", e.Value);
				};
				te.OnValidatingValue += new EventHandler<ValidateEventArgs>(te_OnValidatingValue);

				list.Add(te);
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
                be.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    item.Part = e.EditValue as Part;
                };
                be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
                be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);
                list.Add(be);

				be = new DbButtonsEdit() {
					Width = 300,
					Label = "Цех:",
					EditValue = item.Departament ?? Departament.Default,
					Tag = Types.Departament
				};                
				be.AddButton();
				be.OnApplyValue += (object sender, ValidateEventArgs e) => {
					item.Departament = e.EditValue as Departament;
				};
				be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
				be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);
				list.Add(be);

                be = new DbButtonsEdit() {
					Width = 300,
                    Label = "Поверхность:",
                    EditValue = item.Surface ?? Surface.Default,
                    Tag = Types.Surface
                };                
                be.AddButton();
                be.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    item.Surface = e.EditValue as Surface;
                };
                be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
                be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);
                list.Add(be);

                be = new DbButtonsEdit() {
					Width = 300,
                    Label = "Предыдущий этап:",
                    EditValue = item.StagePrev ?? Stage.Default,
                    Tag = Types.Stage
                };
                be.AddButton();

                be.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    item.StagePrev = e.EditValue as Stage;
                };
                be.OnValidatingValue += new EventHandler<ValidateEventArgs>(be_OnValidatingValue);
                be.ButtonClick += new EventHandler<EventArgsButtonsClick>(be_ButtonClick);
                list.Add(be);

                be = new DbButtonsEdit() {
					Width = 300,
                    Label = "Следующий этап:",
                    EditValue = item.StageNext ?? Stage.Default,
                    Tag = Types.Stage
                };
                be.AddButton();
                be.OnApplyValue += (object sender, ValidateEventArgs e) => {
                    item.StageNext = e.EditValue as Stage;
                };
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

            /*if (Types.Domain.IsAssignableFrom(type)) {
                list.Add(new DataGridViewTextBoxColumn() {
                    DataPropertyName = "Id",
                    HeaderText = "Id"
                });
            }*/

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
            else if (Types.Part.IsAssignableFrom(type)) {
				list.Add(new DataGridViewTextBoxColumn() {
					DataPropertyName = "BLNumber",
					HeaderText = "БЛ номер"
				});
                list.Add(new DataGridViewTextBoxColumn() {
					DataPropertyName = "Alloy",
                    HeaderText = "Сплав"
                });
				list.Add(new DataGridViewTextBoxColumn() {
					DataPropertyName = "Cost",
					HeaderText = "Цена"
				});
            }
			else if (Types.Stage.IsAssignableFrom(type)) {
				list.Add(new DataGridViewTextBoxColumn() {
					DataPropertyName = "Id",
					HeaderText = "Номер"
				});
				list.Add(new DataGridViewTextBoxColumn() {
					DataPropertyName = "Departament",
					HeaderText = "Цех"
				});
				list.Add(new DataGridViewTextBoxColumn() {
					DataPropertyName = "Part",
					HeaderText = "Деталь"
				});
				list.Add(new DataGridViewTextBoxColumn() {
					DataPropertyName = "Surface",
					HeaderText = "Поверхноть"
				});
			}

            return list;
        }

        public static object Edit(object obj = null, Type type = null, IWin32Window owner = null) {
            if (type == null && obj == null)
                return null;

            object result = null;

            if (type == null)
                type = obj.GetType();

            if (obj == null)
                obj = Controller.DataManager.Instance.GetNewItem(type);

            using (var ef = new EditForm(obj)) {
                if (ef.ShowDialog(owner) == System.Windows.Forms.DialogResult.OK) {
					result = Controller.DataManager.Instance.Save(ef.EditValue as IDomain);
					if (result == null)
						new Exception ("Запись не найдена в бд!").ShowError (owner);
                }
            }
            return result;
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
                sf.OnAdd += new EventHandler<EventArgsEdit>(sf_OnAdd);
                sf.OnEdit += new EventHandler<EventArgsEdit>(sf_OnAdd);
                sf.OnDelete += new EventHandler<EventArgsEdit>(sf_OnDelete);

                if (sf.ShowDialog((sender as Control)) == DialogResult.OK) {
                    de.EditValue = sf.Current ?? de.EditValue;
                }
            }
        }

		public static void sf_OnDelete(object sender, EventArgsEdit e) {
            try {
                if (MessageBox.Show(
                    sender as IWin32Window,
                    "Удалить выбранный элемент?",
                    "Удаление",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1
                ) == DialogResult.Yes) {
                    var res = Controller.DataManager.Instance.Delete(e.EditValue as IDomain);
                    if (res == null || res.ToString().Equals("0"))
                        throw new Exception("Запись не удалена!");
					e.EditValue = null;
                }
            }
            catch (Exception ex) {
                ex.ShowError(sender as IWin32Window);
            }
        }

        public static void sf_OnAdd(object sender, EventArgsEdit e) {
            try {
                e.EditValue = Edit(e.EditValue, e.Type, sender as IWin32Window);
            }
            catch (Exception ex) {
                ex.ShowError(sender as IWin32Window);
            }
        }

        public static void be_OnValidatingValue(object sender, ValidateEventArgs e) {
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
