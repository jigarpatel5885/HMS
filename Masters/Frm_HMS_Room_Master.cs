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
    public partial class Frm_HMS_Room_Master : Form
    {
        #region Initialization & Declaration
        ClsRestService _clsRestService = new ClsRestService();
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        CommonServices _clsCommonServices = new CommonServices();
        string _message = string.Empty;
        #endregion

        public Frm_HMS_Room_Master()
        {
            InitializeComponent();
        }

        private void Frm_HMS_Room_Master_Load(object sender, EventArgs e)
        {
            cmbActive.SelectedIndex = 0;
            fillRoomsTypes();
            fillRooms();
            setMode("Add");
        }

        private void txtMaxAdult_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void txtMaxChildren_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void txtPricePerNight_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValueAllowDecimals(sender, e);
        }

        private void txtFloorNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        #region Utilities

        private void fillRoomsTypes()
        {
            var dt = new DataTable();
            var ds = new DataSet();
            try
            {
                if (!_clsGeneralLibrary.tempPatch)
                {
                    dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.roomType);
                    _clsGeneralLibrary.fillComboBox(dt, cmbRoomType, "name", "id");
                }
                else
                {
                    ds = _clsCommonServices.getComboDetails(_clsGlobalConstants.glvUserId,"ROOM_TYPE");

                    _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbRoomType,  "Code_Descr","Key");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
           
        }
        private void clearData()
        {
            txtFloorNumber.Text = "";
            txtMaxAdult.Text = "";
            txtMaxChildren.Text = "";
            txtPricePerNight.Text = "";
            cmbActive.SelectedIndex = 0;
            cmbRoomType.SelectedIndex = 0;
            txtFloorNumber.Text = "";
            lblRoomId.Text = "0";
            txtRoomNumber.Enabled = true;
            txtRoomNumber.Text = "";
        }
        private void fillRooms()
        {
            var dt = new DataTable();
            try
            {
                if (!_clsGeneralLibrary.tempPatch)
                {

                    dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms);
                    dgvRooms.DataSource = dt;
                    dgvRooms.Columns[0].HeaderText = "Id";
                    dgvRooms.Columns[1].HeaderText = "Floor No";
                    dgvRooms.Columns[2].HeaderText = "Max Adult";
                    dgvRooms.Columns[3].HeaderText = "Price";
                    dgvRooms.Columns[4].HeaderText = "Max Children";
                    dgvRooms.Columns[5].HeaderText = "Room No";
                    dgvRooms.Columns[7].HeaderText = "Type";
                    dgvRooms.Columns[6].Visible = false;

                }
                else
                {
                    var ds = new DataSet();
                    ds = _clsCommonServices.getRoomMaster();
                    if (ds.Tables.Count > 0)
                    {
                        dgvRooms.DataSource = ds.Tables[0];
                        dgvRooms.Columns[0].Visible = false;
                    }
                    dgvRooms.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                    dgvRooms.EnableHeadersVisualStyles = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
           
        }
        private void setMode(string mode)
        {
            switch (mode)
            {
                case "Add":
                    btnModify.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = true;
                    txtRoomNumber.Enabled = true;
                    break;
                case "Modify":
                    btnModify.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;
                    txtRoomNumber.Enabled = false;
                    break;
                default:
                    btnModify.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = true;
                    txtRoomNumber.Enabled = true;
                    break;
            }
        }

        private string validateInput()
        {
            var validationMessage = string.Empty;

            if (txtRoomNumber.Text == string.Empty)
            {
                validationMessage += "Please enter room number " + System.Environment.NewLine;
            }
            if (txtFloorNumber.Text == string.Empty)
            {
                validationMessage += "Please enter floor number" + System.Environment.NewLine;
            }
            if (txtMaxChildren.Text == string.Empty)
            {
                validationMessage += "Please enter max children" + System.Environment.NewLine;
            }
            if (txtMaxAdult.Text == string.Empty)
            {
                validationMessage += "Please enter max adult" + System.Environment.NewLine;
            }
            if (txtPricePerNight.Text == string.Empty)
            {
                validationMessage += "Please enter price per night" + System.Environment.NewLine;
            }
            return validationMessage;
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                var fgResponseCode = 0;
                var validationMessage = validateInput();

                if (validationMessage == string.Empty)
                {

                    if (MessageBox.Show("Are you sure you want to save", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (!_clsGeneralLibrary.tempPatch)
                        {
                            var data = new Dictionary<string, string>();
                            ClsRooms _clsRooms = new ClsRooms();
                            data.Add(_clsRooms.roomNumber, txtRoomNumber.Text);
                            data.Add(_clsRooms.floorNumber, txtFloorNumber.Text);
                            data.Add(_clsRooms.maxAdults, txtMaxAdult.Text);
                            data.Add(_clsRooms.maxChildren, txtMaxChildren.Text);
                            data.Add(_clsRooms.pricePerNight, txtPricePerNight.Text);
                            data.Add(_clsRooms.roomType, cmbRoomType.SelectedValue.ToString());
                            int active = 0;
                            if (cmbActive.Text.Trim().Equals("Yes"))
                            {
                                active = 1;
                            }
                            else
                            {
                                active = 0;
                            }

                            data.Add(_clsRooms.active, active.ToString());

                            fgResponseCode = _clsRestService.PostRequest(_clsGlobalConstants.rooms, data);
                            if (fgResponseCode != 0)
                            {
                                MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(_clsRestService.globalResponseMessage.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            clearData();
                            fillRooms();
                        }
                        else
                        {
                            string userId = string.Empty, roomNumber = string.Empty, floorNumber = string.Empty,active=string.Empty;
                            int maxAdult = 0, maxChildren = 0, roomTypeId = 0;
                            decimal price=0;

                            if (cmbActive.Text.Trim().Equals("Yes"))
                            {
                                active ="Y";
                            }
                            else
                            {
                                active = "N";
                            }
                            userId = _clsGlobalConstants.glvUserId;
                            roomNumber = txtRoomNumber.Text;
                            floorNumber = txtFloorNumber.Text;
                            maxAdult = Convert.ToInt32(txtMaxAdult.Text);
                            maxChildren = Convert.ToInt32(txtMaxChildren.Text);
                            roomTypeId = Convert.ToInt32(cmbRoomType.SelectedValue.ToString());
                            price = Convert.ToDecimal(txtPricePerNight.Text);
                            _message = _clsCommonServices.setRoomMaster(userId,
                                                                        roomNumber,
                                                                        floorNumber,
                                                                        maxAdult,
                                                                        maxChildren,
                                                                        price,
                                                                        roomTypeId,
                                                                        active);
                            if (!_message.Equals("No Error"))
                            {
                                MessageBox.Show(_message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             
                                fillRooms();
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void btnModify_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                var fgResponseCode = 0;
                var validationMessage = validateInput();

                if (validationMessage == string.Empty)
                {

                    if (MessageBox.Show("Are you sure you want to save", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var data = new Dictionary<string, string>();
                        ClsRooms _clsRooms = new ClsRooms();
                        data.Add(_clsRooms.id, lblRoomId.Text);
                        data.Add(_clsRooms.roomNumber, txtRoomNumber.Text);
                        data.Add(_clsRooms.floorNumber, txtFloorNumber.Text);
                        data.Add(_clsRooms.maxAdults, txtMaxAdult.Text);
                        data.Add(_clsRooms.maxChildren, txtMaxChildren.Text);
                        data.Add(_clsRooms.pricePerNight, txtPricePerNight.Text);
                        data.Add(_clsRooms.roomType, cmbRoomType.Text);
                        int active = 0;
                        if (cmbActive.SelectedItem.ToString().Trim().Equals("Yes"))
                        {
                            active = 1;
                        }
                        else
                        {
                            active = 0;
                        }
                        data.Add(_clsRooms.active, active.ToString());

                        fgResponseCode = _clsRestService.PutRequest(_clsGlobalConstants.rooms, data);
                        if (fgResponseCode != 0)
                        {
                            MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(_clsRestService.globalResponseMessage.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        clearData();
                        fillRooms();
                    }
                }
                else
                {
                    MessageBox.Show(validationMessage.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void dgvRooms_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var _clsRooms = new ClsRooms();
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgvRooms.Rows[rowIndex];
                lblRoomId.Text = row.Cells[_clsRooms.id].Value.ToString();
                txtFloorNumber.Text = row.Cells[_clsRooms.floorNumber].Value.ToString();
                txtMaxAdult.Text = row.Cells[_clsRooms.maxAdults].Value.ToString();
                txtMaxChildren.Text = row.Cells[_clsRooms.maxChildren].Value.ToString();
                txtPricePerNight.Text = row.Cells[_clsRooms.pricePerNight].Value.ToString();
                txtRoomNumber.Text = row.Cells[_clsRooms.roomNumber].Value.ToString();
               // var index = cmbRoomType.FindString(row.Cells[7].Value.ToString());

              cmbRoomType.SelectedIndex =   cmbRoomType.FindStringExact(row.Cells[_clsRooms.roomType].Value.ToString());
               // cmbRoomType.SelectedValue =  row.Cells[_clsRooms.roomType].Value.ToString();
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

        private void Frm_HMS_Room_Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

       
    }
}
