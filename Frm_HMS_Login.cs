using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HMS.Custom_Classes;
namespace HMS
{
    public partial class Frm_HMS_Login : Form
    {
        #region "initialization & Declaration"
        ClsRestService _clsRestService = new ClsRestService();
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        #endregion
        public Frm_HMS_Login()
        {
            InitializeComponent();
        }

        private void Frm_HMS_Login_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        { 
            var validationString = "";
            validationString = validateCredentials();
            if (!validationString.Equals(string.Empty))
            {
                string userName = string.Empty, password = string.Empty;

            }
            else
            {
                MessageBox.Show(validationString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region "Utilities"
        private string validateCredentials()
        {
            var validateMessage = "";

            if (txtUserName.Text.Equals(String.Empty))
            {
                validateMessage =validateMessage +  "Enter user name." + Environment.NewLine;
            }
            if (txtPassword.Text.Equals(String.Empty))
            {
                validateMessage = validateMessage + "Enter Password." + Environment.NewLine;
            }

            return validateMessage;
        }
        #endregion
    }
}
