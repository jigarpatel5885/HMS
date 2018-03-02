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
using HMS.Custom_Classes.Service_Classes;
namespace HMS.Masters
{
    public partial class Frm_HMS_Service_Master : Form
    {
        #region Initialization and declaration
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        CommonServices _clsCommonServices = new CommonServices();
        string _message = string.Empty;
        #endregion
        public Frm_HMS_Service_Master()
        {
            InitializeComponent();
        }

        private void Frm_HMS_Service_Master_Load(object sender, EventArgs e)
        {
            fillServicesList();
            cmbActive.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string serviceName = string.Empty, serviceDescr = string.Empty, active = string.Empty,userId=string.Empty;
            decimal price=0;
            int serviceId = 0;
            serviceId = Convert.ToInt32(lblServiceId.Text);
            serviceName = txtServiceName.Text;
            serviceDescr = txtServiceDescr.Text;
            price =  Convert.ToDecimal(txtServicePrice.Text);
            userId = _clsGlobalConstants.glvUserId;

           

            if (cmbActive.Text.Trim().Equals("Yes"))
            {
                active = "Y";
            }
            else
            {
                active = "N";
            }

            _message = _clsCommonServices.setServiceMaster(userId,
                                                             serviceId,
                                                            serviceName,
                                                            serviceDescr,
                                                            price,
                                                            active);
            if (!_message.Equals("No Error"))
            {
                MessageBox.Show(_message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);

                fillServicesList();
            }
            clearData();

        }

        #region utilities
        private void fillServicesList()
        {
            try
            {
                var ds = new DataSet();
                ds = _clsCommonServices.getServiceMaster();
                {
                    if (ds.Tables.Count > 0)
                    {
                        dgvRooms.DataSource = ds.Tables[0];
                    }
                }       
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message.ToString());
            }
                        
        }

        private void clearData()
        {
            txtServiceName.Text = "";
            txtServiceDescr.Text = "";
            txtServicePrice.Text = "";
            lblServiceId.Text = "0";
        }
        #endregion

        private void txtServicePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void dgvRooms_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var _clsRooms = new ClsServiceMaster();
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgvRooms.Rows[rowIndex];
                lblServiceId.Text = row.Cells[_clsRooms.serviceId].Value.ToString();
                txtServiceName.Text = row.Cells[_clsRooms.serviceName].Value.ToString();
                txtServiceDescr.Text = row.Cells[_clsRooms.serviceDescr].Value.ToString();
                txtServicePrice.Text = row.Cells[_clsRooms.price].Value.ToString();
              
                if (row.Cells["Active"].Value.ToString().Equals("Y"))
                {
                    cmbActive.SelectedIndex = cmbActive.FindStringExact("Yes");
                }
                else
                {
                    cmbActive.SelectedIndex = cmbActive.FindStringExact("No");
                } // setMode("Modify");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Frm_HMS_Service_Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearData();
        }
     }
}
