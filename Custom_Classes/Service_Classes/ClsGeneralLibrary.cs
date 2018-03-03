using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace HMS.Custom_Classes
{
   
    class ClsGeneralLibrary
    {
        public bool tempPatch = true;
        public string message { get; set; }
          
        public ClsGeneralLibrary()
        {
            message = "No Error";
        }
     /// <summary>
     /// 
     /// </summary>
     /// <param name="dt"></param>
     /// <param name="combo"></param>
     /// <param name="textField"></param>
     /// <param name="valueField"></param>
        public void fillComboBox(DataTable dt,ComboBox combo,string textField,string valueField)
        {
            combo.DisplayMember = textField;
            combo.ValueMember = valueField;
           combo.DataSource = dt;
          
                    
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public  void isNumericValue(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || (e.KeyChar == '.'))
            {
                e.Handled = true;
                MessageBox.Show("Only numeric values allowed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void isNumericValueAllowDecimals(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Only numeric values allowed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // only allow one decimal point


            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }


    }
}
