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
    public partial class Frm_HMS_Occupacy : Form
    {
        #region Initialization & Declaration
        ClsRestService _clsRestService = new ClsRestService();
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        ClsRooms _clsRooms = new ClsRooms();
        CommonServices _commonServices = new CommonServices();
        string _message = string.Empty;
        int lv_RentUploadStatus = 0;
        #endregion
        public Frm_HMS_Occupacy()
        {
            InitializeComponent();
        }

        private void Frm_HMS_Occupacy_Load(object sender, EventArgs e)
        {
            
        }

        #region Utilities

        private void fillReservedRoomList(string p_Date)
        {           
            var dt = new DataTable();
            var ds = new DataSet();
            //to be changed 
            string userId = string.Empty;
            userId = _clsGlobalConstants.glvUserId;
            if (true)
            {
                ds = _commonServices.getOccupancyList(userId,p_Date);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.ClearSelection();
                }
                else
                {
                    dataGridView1.DataSource = null;
                    
                }
            }
            else
            {
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms + "'/'" + p_Date);
                dataGridView1.DataSource = dt;
                dataGridView1.ClearSelection();
            }
        }
        

        #endregion

        private void btnUploadRent_Click(object sender, EventArgs e)
        {
            Dictionary<string, string>[] data = new Dictionary<string, string>[dataGridView1.Rows.Count];
            int fgResponseCode = 0;
            int lv_success = 0;
            int lv_select = 0;

            try
            {
                if (true)
                {
                    string userId = string.Empty, reservationId = string.Empty, serviceTime = string.Empty, serviceDate = string.Empty,
                remarks = string.Empty, trnMode = string.Empty;
                    decimal price = 0;
                    int roomNo = 0, serviceId = 0;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {

                        bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["chkItems"].Value);
                        if (isSelected)
                        {
                            lv_select = 1;
                            serviceId = _clsGlobalConstants.roomRentSersvice;
                            reservationId = dataGridView1.Rows[i].Cells["RESERVATION_ID"].Value.ToString();
                            serviceDate =dtpDate.Text;
                            serviceTime = DateTime.Now.ToString("hh:mm:ss");
                            remarks = "";
                            trnMode = "ADD";
                            roomNo = Convert.ToInt32(dataGridView1.Rows[i].Cells["ROOM_NO"].Value.ToString());
                            price = Convert.ToDecimal(dataGridView1.Rows[i].Cells["PRICE"].Value.ToString());
                            userId = _clsGlobalConstants.glvUserId;
                            _message = _commonServices.setCustomerService(userId,
                                        serviceId,
                                        reservationId,
                                        roomNo,
                                        serviceTime,
                                        serviceDate,
                                        remarks,
                                        price,
                                        trnMode);
                            if (!_message.Equals(_clsGeneralLibrary.message))
                            {
                                MessageBox.Show(_message);
                                lv_success = 1;
                            }
                           

                        }

                    }
                    if (lv_select == 1)
                    {
                        if (lv_success != 1)
                        {
                            MessageBox.Show("Rent Uploaded..!!!");

                        }
                        else
                        {
                            MessageBox.Show("Rent Not Uploaded for all the rooms..!!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select any Rooms to upload rent..!!!");
                    }
                   
                    fillReservedRoomList(dtpDate.Text);
                     
                }
                else
                {
                    if (!_clsGlobalConstants.globalRentUploadStatus)
                    {
                        //Todo

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            data[i] = new Dictionary<string, string>();
                            data[i].Add("", dataGridView1.Rows[i].Cells[""].Value.ToString());
                            data[i].Add("", dataGridView1.Rows[i].Cells[""].Value.ToString());

                        }
                        var serviceData = new Dictionary<string, object>();
                        serviceData.Add("RentData", data);
                        fgResponseCode = _clsRestService.PostRequestMultiDimesionJsonString("", serviceData);
                        if (fgResponseCode != 0)
                        {
                            MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(_clsRestService.globalResponseMessage.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rent already uploaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Frm_HMS_Occupacy_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            fillReservedRoomList(dtpDate.Text);
        }
    }
}
