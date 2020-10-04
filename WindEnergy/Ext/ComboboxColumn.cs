using CommonLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindEnergy.UI.Ext
{
    /// <summary>
    /// столбец с выбором значений
    /// </summary>
    /// <typeparam name="T">тип перечисления для элементов открывающегося списка</typeparam>
    public class DataGridViewComboboxColumn<T> : DataGridViewColumn
    {
        public DataGridViewComboboxColumn()
            : base(new DataGridViewComboboxCell<T>())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a ComboboxCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewComboboxCell<T>)))
                {
                    throw new InvalidCastException("Must be a ComboboxCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    /// <summary>
    /// ячейка с выбором значений
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataGridViewComboboxCell<T> : DataGridViewTextBoxCell
    {
        private readonly List<object> items;

        public DataGridViewComboboxCell()
            : base()
        {
            if (typeof(T) == typeof(WindDirections16))
                items = WindDirections16.Undefined.GetItems().ConvertAll<object>((s) => s); // получение списка элементов перечисления
            else if (typeof(T) == typeof(StandartIntervals))
                items = StandartIntervals.H1.GetItems().ConvertAll<object>((s) => s); // получение списка элементов перечисления
            else
                throw new Exception("Конвертер типов для этого перечисления не реализован");

        }



        /// <summary>
        /// начало редактирования ячейки
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="initialFormattedValue"></param>
        /// <param name="dataGridViewCellStyle"></param>
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            DataGridViewComboBoxEditingControl ctl = DataGridView.EditingControl as DataGridViewComboBoxEditingControl;

            ctl.Items.Clear();
            ctl.Items.AddRange(items.ToArray());

            // Use the default row value when Value property is null.
            if (this.Value == null)
            {
                ctl.SelectedItem = this.DefaultNewRowValue;
            }
            else
            {
                ctl.SelectedItem = ((T)this.Value);
            }
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that ComboboxCell uses.
                return typeof(DataGridViewComboBoxEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that ComboboxCell contains.
                return typeof(T);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return Enum.GetValues(typeof(T)).GetValue(0);
            }
        }
    }

}
