using System;
using System.Collections;
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
    public partial class Frm_AvailableRooms : Form
    {
        #region Initialization and Declaration
        ClsRestService _clsRestService = new ClsRestService();
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        Frm_Hms_CheckIn frm = new Frm_Hms_CheckIn();
        ClsRooms _clsRooms = new ClsRooms();
        CommonServices _commonServices = new CommonServices();
            Form _form;
        #endregion
        
        public Frm_AvailableRooms(Frm_Hms_CheckIn frmCheckIn)
        {
            _form = frmCheckIn;
            InitializeComponent();
            
        }

        private void Frm_AvailableRooms_Load(object sender, EventArgs e)
        {
            GetAvailableRoom(chkAvailableRooms);                                                                
        }

        #region "Utilities"
        private void GetAvailableRoom(CheckedListBox chkList)
        {
            var dt = new DataTable();
            Frm_Hms_CheckIn chkin = (Frm_Hms_CheckIn)_form;
            try
            {
                if (true)
                {
                    var ds = new DataSet();
                    ds = _commonServices.getComboDetails(_clsGlobalConstants.glvUserId, "AVIALABLE_ROOMS");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            if (!chkin.cmbRoomNo.Text.Trim().Equals(row["Key"].ToString()))
                            {
                                 chkList.Items.Add(row["Key"].ToString());
                            }
                           
                        }
                    }
                }
                else
                {
                    dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms);

                    foreach (DataRow row in dt.Rows)
                    {
                        chkList.Items.Add(row[_clsRooms.roomNumber].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
           
        }   

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            Frm_Hms_CheckIn chkin = (Frm_Hms_CheckIn)_form;
            string roomList = "";
            foreach (var items in chkAvailableRooms.CheckedItems)
            {
                if (roomList.Equals(string.Empty))
                {
                    roomList = roomList + items;
                }
                else
                {
                    roomList = roomList + "," + items;
                }
            }
            if (roomList.Equals(string.Empty))
            {
                chkin.chkMultipleRooms.Checked = false;
            }
            else
            {
                chkin.txtMultipleRooms.Text = roomList;
               
            }
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Frm_Hms_CheckIn chkin = (Frm_Hms_CheckIn)_form;
            if (chkin.txtMultipleRooms.Text == "")
            {
                chkin.chkMultipleRooms.Checked = false;
            }
            this.Dispose();
        }

       
    }
}
