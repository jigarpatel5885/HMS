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
namespace HMS
{
    public partial class Frm_HMS_RoomShifting : Form
    {
      
        #region Initialization & Declaration
        ClsRestService _clsRestService = new ClsRestService();
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        ClsRooms _clsRooms = new ClsRooms();
        CommonServices _commonServices = new CommonServices();
        string lv_TranMode = "";
        string _message = string.Empty;
       
        #endregion

        public Frm_HMS_RoomShifting()
        {
            InitializeComponent();
        }

        private void Frm_HMS_RoomShifting_Load(object sender, EventArgs e)
        {
            fillRooms();
            fillAvailableRooms();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
           // int fgResponseCode = 0;
            string shiftingDate = string.Empty, shiftingTime = string.Empty,userId=string.Empty,reservationNo=string.Empty;
            int newRoomNr, oldRoomNr;
            decimal roomCharges = 0;
            newRoomNr =  Convert.ToInt32(cmbNewRoomNo.SelectedValue.ToString());
           oldRoomNr = Convert.ToInt32( cmbRoomNo.SelectedValue.ToString());
            shiftingTime = dtpTime.Text;
            shiftingDate = dtpDate.Text;
            roomCharges = Convert.ToDecimal(string.IsNullOrEmpty(txtRoomCharges.Text) ? "0" : txtRoomCharges.Text);
            reservationNo = txtRegNo.Text;
            userId = _clsGlobalConstants.glvUserId;
            _message = _commonServices.setRoomShifting(userId,
                                                       newRoomNr,
                                                       oldRoomNr,
                                                       shiftingDate,
                                                       shiftingTime,
                                                       reservationNo,
                                                       roomCharges);

           if (!_message.Equals(_clsGeneralLibrary.message))
           {
                      MessageBox.Show(_message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
           else
           {

                MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fillRooms();
                fillAvailableRooms();
           }
            


        }

        #region utilities


        private void fillGuestData(string roomNo)
        {
            try
            {
                var dt = new DataTable();
                var ds = new DataSet();

                ds = _commonServices.getCustomerCheckinByRoomId(roomNo );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];

                        foreach (DataRow row in dt.Rows)
                        {
                            txtName.Text = row["NAME"].ToString();
                            txtRegNo.Text = row["Reservation_Id"].ToString();
                            txtArTime.Text = row["AR_Time"].ToString();
                            txtTotalPerson.Text = row["Total_Guest"].ToString();
                            txtRoomCharges.Text = row["Room_Rent"].ToString();
                        }
                    }                              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

        }



        private void fillRooms()
        {
            var dt = new DataTable();

            if (!_clsGeneralLibrary.tempPatch)
            {
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms);
                _clsGeneralLibrary.fillComboBox(dt, cmbRoomNo, _clsRooms.roomNumber, _clsRooms.id);
            }
            else
            {
                var ds = new DataSet();
                ds = _commonServices.getComboDetails(_clsGlobalConstants.glvUserId, "CHECK_IN_ROOMS_SHIFTING");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbRoomNo, "Key", "Key");
                }
                else
                {
                    MessageBox.Show(_commonServices._message);
                }
            }

        }

        private void fillAvailableRooms()
        {
            var dt = new DataTable();
           
                var ds = new DataSet();
                ds = _commonServices.getComboDetails(_clsGlobalConstants.glvUserId, "AVIALABLE_ROOMS");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbNewRoomNo, "Key", "Key");

                }
                else
                {
                    MessageBox.Show(_commonServices._message);
                }
            
        }

        private void fillGuestData()
        {
            try
            {
                var dt = new DataTable();
                var ds = new DataSet();
                if (true)
                {
                    ds = _commonServices.getCustomerCheckinByRoomId(cmbRoomNo.Text.Trim());

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];

                        foreach (DataRow row in dt.Rows)
                        {
                            txtName.Text = row["NAME"].ToString();
                            txtRegNo.Text = row["Reservation_Id"].ToString();
                            txtArTime.Text = row["AR_Time"].ToString();
                            txtTotalPerson.Text = row["Total_Guest"].ToString();

                        }
                    }
                }
                else
                {

                    dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms + "'\'" + cmbRoomNo.SelectedValue.ToString());
                    foreach (DataRow row in dt.Rows)
                    {
                        txtName.Text = row[""].ToString();
                        txtRegNo.Text = row[""].ToString();
                        txtArTime.Text = row[""].ToString();
                        txtTotalPerson.Text = row[""].ToString();

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

        }
        #endregion

        private void btnGet_Click(object sender, EventArgs e)
        {
            fillGuestData(cmbRoomNo.Text.Trim());
        }

        private void Frm_HMS_RoomShifting_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void txtRoomCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValueAllowDecimals(sender,e);
        }

        private void cmbRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
