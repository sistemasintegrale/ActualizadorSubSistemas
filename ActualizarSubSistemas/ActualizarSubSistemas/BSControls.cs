using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualizarSubSistemas
{
    public class BSControls
    {
        public static void Guna2Combo(Guna2ComboBox Control, object DataSource, string description, string values, bool DefaulValue)
        {
            var _with1 = Control;
            _with1.DataSource = DataSource;
            _with1.DisplayMember = description;
            _with1.ValueMember = values;
            if (DefaulValue)
                _with1.StartIndex = 1;
        }
    }
}
