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
    public partial class Frm_HMS_Room_Type_Master : Form
    {
        #region Initialization & Declaration
        ClsRestService _clsRestService = new ClsRestService();
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        CommonServices _clsCommonServices = new CommonServices();
       
        #endregion

        public Frm_HMS_Room_Type_Master()
        {
            InitializeComponent();
        }

        private void txtRoomPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&(e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Only numeric values allowed","Warning",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
          
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void Frm_Room_Type_Master_Load(object sender, EventArgs e)
        {
            // to be checked webservice check alive
            setMode("Add");
            fillRoomList();
            cmbActive.SelectedIndex = 0;
        }

        #region Utilities
        private void fillRoomList()
        {
            bool fgPatchEnabled = true;
            try
            {
                if (!fgPatchEnabled)
                {
                    var dt = new DataTable();
                    dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.roomType); ;
                    dgvRoomTypeList.DataSource = dt;
                    dgvRoomTypeList.ClearSelection();
                    dgvRoomTypeList.Columns[0].HeaderText = "Id";
                    dgvRoomTypeList.Columns[1].HeaderText = "Room Name";
                    dgvRoomTypeList.Columns[2].HeaderText = "Price";
                    dgvRoomTypeList.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                    dgvRoomTypeList.EnableHeadersVisualStyles = false;
                    dgvRoomTypeList.ClearSelection();
                }
                else
                {
                    var ds = new DataSet();

                     ds = _clsCommonServices.getRoomTypeMaster();

                     if (ds.Tables.Count == 0)
                     {
                         if (!_clsCommonServices._message.Equals("No Error"))
                         {
                             MessageBox.Show(_clsCommonServices._message);
                         }
                     }
                     else
                     {
                         dgvRoomTypeList.DataSource = ds.Tables[0];
                         dgvRoomTypeList.ClearSelection();
                         dgvRoomTypeList.Columns[0].HeaderText = "Id";
                         dgvRoomTypeList.Columns[1].HeaderText = "Room Name";
                         dgvRoomTypeList.Columns[2].HeaderText = "Price";
                         dgvRoomTypeList.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                         dgvRoomTypeList.EnableHeadersVisualStyles = false;
                     }
                    
                }
            }
            catch (Exception)
            {
                
              
            }
            
            
        }
        private void ClearData()
        {
            txtRoomName.Text = "";
            txtRoomPrice.Text = "";
            lblRoomId.Text = "0";
        }
        private void setMode(string mode)
        {
            switch (mode)
            {
                case "Add" :
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

            if (txtRoomName.Text == string.Empty)
            {
                validationMessage += "Please enter room name " + System.Environment.NewLine;
            }
            if (txtRoomPrice.Text == string.Empty)
            {
                validationMessage += "Please enter room price" + System.Environment.NewLine;
            }

            return validationMessage;
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();            
        }

        private void Frm_Room_Type_Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();   
        }
              
        private void btnModify_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            var fgResponseCode = 0;
            bool fgpatchEnabled = true;
             var validationMessage = validateInput();
             if (validationMessage == string.Empty)
             {
                 if (MessageBox.Show("Are you sure you want to Modify", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                 {
                     if (!fgpatchEnabled)
                     {
                         var data = new Dictionary<string, string>();
                         ClsRoomTypes _clsRoomType = new ClsRoomTypes();
                         data.Add(_clsRoomType.roomName, txtRoomName.Text);
                         data.Add(_clsRoomType.price, txtRoomPrice.Text);
                         data.Add(_clsRoomType.roomId, lblRoomId.Text);
                         fgResponseCode = _clsRestService.PutRequest(_clsGlobalConstants.roomType, data);
                         if (fgResponseCode != 0)
                         {
                             MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                         else
                         {
                             MessageBox.Show(_clsRestService.globalResponseMessage.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }
                         ClearData();
                         fillRoomList();
                         setMode("Add");
                     }
                     else
                     {
                         string userId = "test";
                         string active = string.Empty;
                         if (cmbActive.SelectedItem.ToString().Equals("Yes"))
                         {
                             active = "Y";
                         }
                         else
                         {
                             active = "N";
                         }
                         string _message = _clsCommonServices.setRoomTypeMaster(userId, txtRoomName.Text, Convert.ToDecimal(txtRoomPrice.Text), active);

                         if (!_message.Equals("No Error"))
                         {
                             MessageBox.Show(_message);
                         }
                         else
                         {
                             MessageBox.Show("Sucess");
                         }

                         ClearData();
                          fillRoomList();
                         setMode("Add");
                     }
                    
                 }
             }
             else 
             {
                 MessageBox.Show(validationMessage.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
             }
             this.Cursor = Cursors.Default;
             
        }

        private void dgvRoomTypeList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
               
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgvRoomTypeList.Rows[rowIndex];
                lblRoomId.Text = row.Cells[0].Value.ToString();
                txtRoomName.Text = row.Cells[1].Value.ToString();
                txtRoomPrice.Text = row.Cells[2].Value.ToString();
                if (row.Cells["Active"].Value.ToString().Equals("Y"))
                {
                    cmbActive.SelectedIndex = cmbActive.FindStringExact("Yes");
                }
                else
                {
                    cmbActive.SelectedIndex = cmbActive.FindStringExact("No");
                }
               // setMode("Modify");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
            setMode("Add");
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            var fgResponseCode = 0;
            bool fgpatchEnabled = true;
            var validationMessage = validateInput();
            
              if (validationMessage == string.Empty)
              {
           
                if (MessageBox.Show("Are you sure you want to save", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!fgpatchEnabled)
                    {
                        var data = new Dictionary<string, string>();
                        ClsRoomTypes _clsRoomType = new ClsRoomTypes();
                        data.Add(_clsRoomType.roomName, txtRoomName.Text);
                        data.Add(_clsRoomType.price, txtRoomPrice.Text);
                        data.Add(_clsRoomType.roomId, lblRoomId.Text);
                        fgResponseCode = _clsRestService.PostRequest(_clsGlobalConstants.roomType, data);
                        if (fgResponseCode != 0)
                        {
                            MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(_clsRestService.globalResponseMessage.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ClearData();
                        fillRoomList();
                        setMode("Add");
                    }
                    else
                    {
                        string userId = "test";
                        string active = string.Empty;
                        if (cmbActive.SelectedItem.ToString().Equals("Yes"))
                        {
                            active = "Y";
                        }
                        else
                        {
                            active = "N";
                        }
    
                        string _message = _clsCommonServices.setRoomTypeMaster(userId, txtRoomName.Text,Convert.ToDecimal(txtRoomPrice.Text), active);

                        if (!_message.Equals("No Error"))
                        {
                            MessageBox.Show(_message);
                        }
                        else
                        {
                            MessageBox.Show("Sucess");
                        }

                        ClearData();
                       fillRoomList();
                        setMode("Add");
                    }
                   
                }
            }
              else
              {
                  MessageBox.Show(validationMessage.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
              }
              this.Cursor = Cursors.Default;
           
              
        }

        

       

        
    }
}
