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
    public partial class Frm_HMS_CorporateClientEntry : Form
    {
        #region Initialization & Declaration
        ClsRestService _clsRestService = new ClsRestService();
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        ClsCorporateClients _clsCorporateClients = new ClsCorporateClients();
        CommonServices _commonServices = new CommonServices();
        string _message = string.Empty;
        DataSet ds = new DataSet();
        #endregion

        public Frm_HMS_CorporateClientEntry()
        {
            InitializeComponent();
        }

        private void Frm_HMS_CorporateClientEntry_Load(object sender, EventArgs e)
        {
            lblClientId.Text = "0";
            fillCountries();
            fillCorporateClient();
            setMode("Add");
            cmbActive.SelectedIndex = 0;
        }

        #region utilities

        private void fillCorporateClient()
        {
            var dt = new DataTable();
            if (!_clsGeneralLibrary.tempPatch)
            {

                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.corporateClients);
                dt.DefaultView.Sort = "id";
                dt = dt.DefaultView.ToTable();
                dgvClientList.DataSource = dt;
            }
            else
            {
                
                ds = _commonServices.getCorporateClient();
                if (ds.Tables.Count > 0)
                {
                    dgvClientList.DataSource = ds.Tables[0];
                }                
            }

            dgvClientList.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgvClientList.EnableHeadersVisualStyles = false;
            dgvClientList.ClearSelection();
        }
        private void clearData()
        {
            txtName.Text = "";
            txtContactPerson.Text = "";
            txtEmailId.Text = "";
            txtMobileNo.Text = "";
            txtLandLineNo.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtArea.Text = "";
            txtGSTNNo.Text = "";
            txtPostalCode.Text = "";
            lblClientId.Text = "0";
            textBox1.Text = "";
            fillCorporateClient();
        }
        private void setMode(string mode)
        {
            switch (mode)
            {
                case "Add":
                    btnModify.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = true;
                    break;
                case "Modify":
                    btnModify.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;

                    break;
                default:
                    btnModify.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = true;
                    break;
            }
        }
        private string validateInput()
        {
            var validationMessage = string.Empty;

            if (txtName.Text.Equals(string.Empty))
            {
                validationMessage += "Please Enter Name." + System.Environment.NewLine;
            }
            //if (txtMobileNo.Text.Equals(string.Empty))
            //{
            //    validationMessage += "Please Enter Mobile No." + System.Environment.NewLine;
            //}
            //if (txtEmailId.Text.Equals(string.Empty))
            //{
            //    validationMessage += "Please Enter Email Id." + System.Environment.NewLine;
            //}

            if (txtAddress1.Text.Equals(string.Empty))
            {
                validationMessage += "Please Enter Address-1" + System.Environment.NewLine;
            }

            if (txtPostalCode.Text.Equals(string.Empty))
            {
                validationMessage += "Please Enter Postal Code No." + System.Environment.NewLine;
            }
            if (txtGSTNNo.Text.Equals(string.Empty))
            {
                validationMessage += "Please Enter GSTN No." + System.Environment.NewLine;
            }
            if (txtContactPerson.Text.Equals(string.Empty))
            {
                validationMessage += "Please Enter Contact Person." + System.Environment.NewLine;
            }
            if (txtMobileNo.Text.Length <10)
            {
                validationMessage += "Mobile Number should be of 10 digits." + System.Environment.NewLine;
            }

            return validationMessage;
        }
        private void fillCountries()
        {
            var dt = new DataTable();
            var ds = new DataSet();
            if (!_clsGeneralLibrary.tempPatch)
            {
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.countries);
                _clsGeneralLibrary.fillComboBox(dt, cmbCountries, "name", "id");
            }
            else
            {
                ds = _commonServices.getComboDetails(_clsGlobalConstants.glvUserId, "COUNTRIES");

                _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbCountries, "Code_Descr", "Key");
            }

          
            
            cmbCountries.SelectedIndex = cmbCountries.FindStringExact("India");

        }
        private void fillState(string countryId)
        {
            var dt = new DataTable();
            var ds = new DataSet();
            if (!_clsGeneralLibrary.tempPatch)
            {
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.states + countryId);
                _clsGeneralLibrary.fillComboBox(dt, cmbState, "name", "id");
            }
            else
            {
                ds = _commonServices.getStatesByCountryId(cmbCountries.SelectedValue.ToString());
                _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbState, "Code_Descr", "Key");
            }          
        }
        private void fillCities(string stateId)
        {
            var dt = new DataTable();
             var ds = new DataSet();
             if (!_clsGeneralLibrary.tempPatch)
             {
                 dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.cities + stateId);
                 _clsGeneralLibrary.fillComboBox(dt, cmbCities, "name", "id");
             }
             else
             {
                 ds = _commonServices.getCitiesByStateId(cmbState.SelectedValue.ToString());
                 _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbCities, "Code_Descr", "Key");
             }

        }

        #endregion

        private void cmbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillState(cmbCountries.SelectedValue.ToString());
        }

        private void cmbCities_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCities(cmbState.SelectedValue.ToString());
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void txtLandLineNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void txtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }
                    
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearData();
            setMode("Add");
        }
  
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
             string name = string.Empty, mobNo = string.Empty, landLine = string.Empty, emailId = string.Empty,
                add1 = string.Empty,id=string.Empty, add2 = string.Empty, add3 = string.Empty, contactPerson = string.Empty,
                area=string.Empty,city=string.Empty,country=string.Empty,pinCode=string.Empty,state=string.Empty,gstid=string.Empty;
            this.Cursor = Cursors.WaitCursor;
            var fgResponseCode = 0;
             var validationMessage = validateInput();
             if (validationMessage == string.Empty)
             {
                 if (MessageBox.Show("Are you sure you want to save", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                 {
                     if (validationMessage == string.Empty)
                     {
                         this.Cursor = Cursors.Default;
                         name = txtName.Text;
                         mobNo = txtMobileNo.Text;
                         landLine = txtLandLineNo.Text;
                         emailId = txtEmailId.Text;
                         add1 = txtAddress1.Text;
                         add2 = txtAddress2.Text;
                         add3 = txtAddress3.Text;
                         contactPerson = txtContactPerson.Text;
                         area = txtArea.Text;
                         city = cmbCities.Text;
                         country = cmbCountries.Text;
                         state = cmbState.Text;
                         pinCode = txtPostalCode.Text;
                         gstid = txtGSTNNo.Text;
                         id = lblClientId.Text;


                         var data = new Dictionary<string, string>();
                         data.Add("id", id);
                         data.Add(_clsCorporateClients.contactPerson, contactPerson);
                         data.Add(_clsCorporateClients.name, name);
                         data.Add(_clsCorporateClients.landLineNo, landLine);
                         data.Add(_clsCorporateClients.email, emailId);
                         data.Add(_clsCorporateClients.mobileNo, mobNo);
                         data.Add(_clsCorporateClients.address1, add1);
                         data.Add(_clsCorporateClients.address2, add2);
                         data.Add(_clsCorporateClients.address3, add3);
                         data.Add(_clsCorporateClients.area, area);
                         data.Add(_clsCorporateClients.city, city);
                         data.Add(_clsCorporateClients.postalCode, pinCode);
                         data.Add(_clsCorporateClients.state, state);
                         data.Add(_clsCorporateClients.country, country);
                         data.Add(_clsCorporateClients.GstnNo, gstid);
                         fgResponseCode = _clsRestService.PutRequest(_clsGlobalConstants.corporateClients, data);
                         if (fgResponseCode != 0)
                         {
                             MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                         else
                         {
                             MessageBox.Show(_clsRestService.globalResponseMessage.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }
                         clearData();
                         fillCorporateClient();

                     }

                     else
                     {
                         MessageBox.Show(validationMessage.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                     }

                     setMode("Add");
                 }

             }
             else
             {
                 MessageBox.Show(validationMessage.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
             }
             this.Cursor = Cursors.Default;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = string.Empty, mobNo = string.Empty, landLine = string.Empty, emailId = string.Empty,
                add1 = string.Empty, add2 = string.Empty, add3 = string.Empty, contactPerson = string.Empty,
                area=string.Empty,city=string.Empty,country=string.Empty,pinCode=string.Empty,state=string.Empty,gstid=string.Empty,
                active=string.Empty ,userId=string.Empty;
            decimal postalCode = 0;
            int id=0;
            this.Cursor = Cursors.WaitCursor;
            var fgResponseCode = 0;
             var validationMessage = validateInput();
             if (validationMessage == string.Empty)
             {
                 if (MessageBox.Show("Are you sure you want to save", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                 {

                     if (!_clsGeneralLibrary.tempPatch)
                     {
                         if (validationMessage == string.Empty)
                         {
                             this.Cursor = Cursors.Default;
                             name = txtName.Text;
                             mobNo = txtMobileNo.Text;
                             landLine = txtLandLineNo.Text;
                             emailId = txtEmailId.Text;
                             add1 = txtAddress1.Text;
                             add2 = txtAddress2.Text;
                             add3 = txtAddress3.Text;
                             contactPerson = txtContactPerson.Text;
                             area = txtArea.Text;
                             city = cmbCities.Text;
                             country = cmbCountries.Text;
                             state = cmbState.Text;
                             pinCode = txtPostalCode.Text;
                             gstid = txtGSTNNo.Text;



                             var data = new Dictionary<string, string>();

                             data.Add(_clsCorporateClients.contactPerson, contactPerson);
                             data.Add(_clsCorporateClients.name, name);
                             data.Add(_clsCorporateClients.landLineNo, landLine);
                             data.Add(_clsCorporateClients.email, emailId);
                             data.Add(_clsCorporateClients.mobileNo, mobNo);
                             data.Add(_clsCorporateClients.address1, add1);
                             data.Add(_clsCorporateClients.address2, add2);
                             data.Add(_clsCorporateClients.address3, add3);
                             data.Add(_clsCorporateClients.area, area);
                             data.Add(_clsCorporateClients.city, city);
                             data.Add(_clsCorporateClients.postalCode, pinCode);
                             data.Add(_clsCorporateClients.state, state);
                             data.Add(_clsCorporateClients.country, country);
                             data.Add(_clsCorporateClients.GstnNo, gstid);
                             fgResponseCode = _clsRestService.PostRequest(_clsGlobalConstants.corporateClients, data);
                             if (fgResponseCode != 0)
                             {
                                 MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             }
                             else
                             {
                                 MessageBox.Show(_clsRestService.globalResponseMessage.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                             }
                             clearData();
                             fillCorporateClient();
                             setMode("Add");
                         }
                     }
                     else
                     {
                         userId = _clsGlobalConstants.glvUserId;
                         name = txtName.Text;
                         mobNo = txtMobileNo.Text;
                         landLine = txtLandLineNo.Text;
                         emailId = txtEmailId.Text;
                         add1 = txtAddress1.Text;
                         add2 = txtAddress2.Text;
                         add3 = txtAddress3.Text;
                         area = txtArea.Text;
                         country = cmbCountries.Text;
                         city = cmbCities.Text;
                         state = cmbState.Text;
                         postalCode = Convert.ToDecimal(txtPostalCode.Text);
                         gstid = txtGSTNNo.Text;
                         contactPerson = txtContactPerson.Text;
                         id =  Convert.ToInt32(lblClientId.Text);
                         if (cmbActive.Text.Trim().Equals("Yes"))
                         {
                             active = "Y";
                         }
                         else
                         {
                             active = "N";
                         }

                         _message = _commonServices.setCorporateClient(userId, 
                                                                        id,
                                                                        name,
                                                                        Convert.ToDecimal(mobNo),
                                                                        Convert.ToDecimal(landLine),
                                                                        emailId,
                                                                        add1, 
                                                                        add2,
                                                                        add3,
                                                                        area, 
                                                                        country, 
                                                                        state,
                                                                        city,
                                                                        postalCode,
                                                                        gstid,
                                                                        contactPerson,
                                                                        active);
                                                                        
                         if (!_message.Equals("No Error"))
                         {
                             MessageBox.Show(_message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }
                         else
                         {
                             MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            fillCorporateClient();
                         }
                         clearData();

                     }

                 }

                
             }
             else
             {
                 MessageBox.Show(validationMessage.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
             }

             this.Cursor = Cursors.Default;

        }

        private void dgvClientList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgvClientList.Rows[rowIndex];
                lblClientId.Text = row.Cells["Corporate_Id"].Value.ToString();
                txtContactPerson.Text = row.Cells["Contact_Person"].Value.ToString();   
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtLandLineNo.Text = row.Cells["Lanline_No"].Value.ToString();
                txtEmailId.Text = row.Cells["Email_Id"].Value.ToString();
                txtMobileNo.Text = row.Cells["Mobile_No"].Value.ToString();
                txtAddress1.Text = row.Cells["Address1"].Value.ToString();
                txtAddress2.Text = row.Cells["Address2"].Value.ToString();
                txtAddress3.Text = row.Cells["Address3"].Value.ToString();
                txtArea.Text = row.Cells["Area"].Value.ToString();

                txtPostalCode.Text = row.Cells["Postal_Code"].Value.ToString();
                //cmbCountries.SelectedValue= row.Cells["Country"].Value.ToString();
                cmbCountries.SelectedIndex = cmbCountries.FindStringExact(row.Cells["Country"].Value.ToString());
                cmbState.SelectedIndex = cmbState.FindStringExact(row.Cells["State"].Value.ToString());
                cmbCities.SelectedIndex = cmbCities.FindStringExact(row.Cells["City"].Value.ToString());               
                txtGSTNNo.Text = row.Cells["GSTN"].Value.ToString();
                //setMode("Modify");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Frm_HMS_CorporateClientEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            var dvSearch = new DataView(ds.Tables[0]);

            dvSearch.RowFilter = string.Format("NAME LIKE '%{0}%'",textBox1.Text);
            dgvClientList.DataSource = dvSearch;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
