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
    public partial class Frm_HMS_RoomShifting : Form
    {
        #region Initialization & Declaration
        ClsRestService _clsRestService = new ClsRestService();
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        ClsRooms _clsRooms = new ClsRooms();
        string lv_TranMode = "";
        #endregion

        public Frm_HMS_RoomShifting()
        {
            InitializeComponent();
        }

        private void Frm_HMS_RoomShifting_Load(object sender, EventArgs e)
        {
            fillAvialableRooms();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int fgResponseCode = 0;
            string newRoomNr = string.Empty, shiftingDate = string.Empty, shiftingTime = string.Empty,oldRoomNr, roomCharges = string.Empty;
            newRoomNr = cmbNewRoomNo.SelectedValue.ToString();
            oldRoomNr = cmbOldRoomNo.SelectedValue.ToString();
            shiftingTime = dtpTime.Text;
            shiftingDate = dtpDate.Text;
            roomCharges = txtRoomCharges.Text;
            var data = new Dictionary<string, string>();
            data.Add("", newRoomNr);
            data.Add("", oldRoomNr);
            data.Add("", shiftingTime);
            data.Add("", shiftingDate);
            data.Add("", roomCharges);

            fgResponseCode = _clsRestService.PostRequest("", data);
            if (fgResponseCode != 0)
            {
                MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(_clsRestService.globalResponseMessage.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        #region utilities

        private void fillRooms()
        {
            var dt = new DataTable();
            //to be changed for reserved rooms 
            dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms);
            _clsGeneralLibrary.fillComboBox(dt, cmbOldRoomNo, _clsRooms.roomNumber, _clsRooms.id);
        }
        private void fillAvialableRooms()
        {
            var dt = new DataTable();
            dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms);
            _clsGeneralLibrary.fillComboBox(dt, cmbNewRoomNo, _clsRooms.roomNumber, _clsRooms.id);
        }
        #endregion

        private void btnGet_Click(object sender, EventArgs e)
        {

        }

        private void Frm_HMS_RoomShifting_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
