using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using HMS.Custom_Classes;
using HMS.Custom_Classes.Service_Classes;

namespace HMS
{
    public partial class Frm_HMS_Services : Form
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

        public Frm_HMS_Services()
        {
            InitializeComponent();
        }

        private void Frm_HMS_Services_Load(object sender, EventArgs e)
        {
            txtServiceTime.Text = DateTime.Now.ToString("hh:mm:ss");
            fillRooms();
            fillServiceNames();
           
        }

        #region utilities

       
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
                ds = _commonServices.getComboDetails(_clsGlobalConstants.glvUserId, "CHECK_IN_ROOMS");
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
      

        private void fillServiceNames()
        {
            var ds = new DataSet();
            ds = _commonServices.getComboDetails(_clsGlobalConstants.glvUserId, "SERVICE_LIST");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbServices,"Code_Descr","Key");
            }
            else
            {
                MessageBox.Show(_commonServices._message);
            }
        }

        private Boolean validateCheckBoxInsideGrid()
        {
            Boolean fgCheck = false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["chkItems"].Value);
                if (isSelected)
                {
                    fgCheck = true;                   
                }
            }

            return fgCheck;
        }

        private void SetDataInsideGrid()
        {
            try
            {

                string[] row = new string[] { "false", dtpServiceDate.Text, txtServiceTime.Text, cmbServices.Text, txtRemark.Text, txtServiceAmount.Text };
                dataGridView1.Rows.Add(row);
                dataGridView1.ClearSelection();
                 setTotalAmount();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message.ToString());
            }
           
        }

        private double setTotalAmount()
        {
            double total = 0;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                total += Convert.ToDouble(dr.Cells["Price"].Value.ToString());

            }
            txtTotal.Text = total.ToString();
            txtRemark.Text = "";
            
            return total;
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

        private void fillServiceByRoomId(string roomNo)
        {
            var ds = new DataSet();
            ds = _commonServices.getServiceByRoomId(roomNo);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
               
            }
            else
            {
                dataGridView1.DataSource = null;
            }
            setTotalAmount();
        }
        private string getPriceByRoomId(string serviceId)
        {
            string price = "0";
            var ds = new DataSet();
 
            ds = _commonServices.getServicePriceById(serviceId);

            try 
	        {	        
		        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                  price = ds.Tables[0].Rows[0][0].ToString();
                }
	        }
	        catch (Exception ex)
	        {
		
		        MessageBox.Show(ex.Message.ToString());
	        }
                

            return price;
        }

        private void fillServiceDetails()
        {
            try
            {   
               
                var dt = new DataTable();
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms + "'\'" + cmbRoomNo.SelectedValue.ToString());
                dataGridView1.DataSource = dt;
                dataGridView1.ClearSelection();
                if (dataGridView1.Rows.Count > 0)
                {
                    lv_TranMode = _clsGlobalConstants.globalTransModePut;
                }
                else
                {
                    lv_TranMode = _clsGlobalConstants.globalTransModePost;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

        }
        #endregion         

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Frm_HMS_Services_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void txtServiceAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValueAllowDecimals(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string userId = string.Empty, reservationId = string.Empty, serviceTime = string.Empty, serviceDate = string.Empty,
                remarks = string.Empty, trnMode = string.Empty;
            decimal price = 0;
            int roomNo = 0,serviceId=0;
            
            if (txtServiceAmount.Text != string.Empty)
            {
                userId = _clsGlobalConstants.glvUserId;
                serviceId = Convert.ToInt32(cmbServices.SelectedValue.ToString());
                reservationId = txtRegNo.Text;
                serviceDate = dtpServiceDate.Text;
                serviceTime = txtServiceTime.Text;
                remarks = txtRemark.Text;
                trnMode = "ADD";
                roomNo = Convert.ToInt32(cmbRoomNo.Text);
                price = Convert.ToDecimal(txtServiceAmount.Text);

                _message = _commonServices.setCustomerService(userId,
                                            serviceId,
                                            reservationId,
                                            roomNo,
                                            serviceTime,
                                            serviceDate,
                                            remarks,
                                            price,
                                            trnMode);

              //  SetDataInsideGrid();

                         if (!_message.Equals(_clsGeneralLibrary.message))
                         {
                             MessageBox.Show(_message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }
                         else
                         {
                             MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           fillServiceByRoomId(cmbRoomNo.Text.Trim());
                          // txtServiceAmount.Text = getPriceByRoomId(cmbServices.SelectedValue.ToString());
                         }
            }
            else
            {
                MessageBox.Show("Enter service amount ..!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string userId = string.Empty, reservationId = string.Empty, serviceTime = string.Empty, serviceDate = string.Empty,
              remarks = string.Empty, trnMode = string.Empty;
            decimal price = 0;
            int roomNo = 0, serviceId = 0;
            userId = _clsGlobalConstants.glvUserId;
            if (validateCheckBoxInsideGrid())
            {
                if (MessageBox.Show("Are you sure you want to remove selected items ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (true)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {

                            bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["chkItems"].Value);
                            if (isSelected)
                            {
                                serviceId = Convert.ToInt32(dataGridView1.Rows[i].Cells["SERVICE_ID"].Value.ToString());
                                reservationId = txtRegNo.Text;
                                serviceDate = dataGridView1.Rows[i].Cells["SERVICE_DATE"].Value.ToString();
                                serviceTime = dataGridView1.Rows[i].Cells["SERVICE_TIME"].Value.ToString();
                                remarks = dataGridView1.Rows[i].Cells["REMARKS"].Value.ToString();
                                trnMode = "DELETE";
                                roomNo = Convert.ToInt32(cmbRoomNo.Text.Trim());
                                price = Convert.ToDecimal(dataGridView1.Rows[i].Cells["PRICE"].Value.ToString());
                               
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
                                    break;
                                }
                               
                            }
                           
                        }
                        if (!_message.Equals(_clsGeneralLibrary.message))
                        {
                            MessageBox.Show("Did not removed one or more selected service", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fillServiceByRoomId(cmbRoomNo.Text.Trim());

                       //     txtServiceAmount.Text = getPriceByRoomId(cmbServices.SelectedValue.ToString());
                        }
                    }
                    else
                    {
                        var row = new List<DataGridViewRow>();
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["chkItems"].Value);
                            if (isSelected)
                            {
                                row.Add(dataGridView1.Rows[i]);
                            }
                        }

                        for (int j = 0; j < row.Count; j++)
                        {
                            dataGridView1.Rows.Remove(row[j]);
                        }

                        foreach (DataGridViewRow item in dataGridView1.Rows)
                        {
                            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)item.Cells["chkItems"];
                            cell.Value = false;
                        }
                        dataGridView1.ClearSelection();
                    }
                    setTotalAmount();
                }
            }
            else
            {
                MessageBox.Show("Select items to be removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //private void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    string roomNr = string.Empty,totalAmt = string.Empty;
        //    try
        //    {
               
        //        Dictionary<string, string>[] data = new Dictionary<string, string>[dataGridView1.Rows.Count];
        //        int fgResponseCode = 0;
        //        if (MessageBox.Show("Are you sure you want to save ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {

        //            roomNr = cmbRoomNo.Text;
        //            totalAmt = txtTotal.Text;


        //            for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //            {
        //                data[i] = new Dictionary<string, string>();
        //                data[i].Add("Date", dataGridView1.Rows[i].Cells["Date"].Value.ToString());
        //                data[i].Add("Time", dataGridView1.Rows[i].Cells["Time"].Value.ToString());
        //                data[i].Add("Service", dataGridView1.Rows[i].Cells["Service"].Value.ToString());
        //                data[i].Add("Remarks", dataGridView1.Rows[i].Cells["Remarks"].Value.ToString());
        //                data[i].Add("Amount", dataGridView1.Rows[i].Cells["Amount"].Value.ToString());
        //            }

        //            var serviceData = new Dictionary<string, object>();
        //            serviceData.Add("RoomId", roomNr);
        //            serviceData.Add("Total", totalAmt);
        //            serviceData.Add("serviceDetails", data);
        //            if (lv_TranMode.Equals(_clsGlobalConstants.globalTransModePost))
        //            {
        //                fgResponseCode = _clsRestService.PostRequestMultiDimesionJsonString("", serviceData);
        //            }
        //            else
        //            {
        //                fgResponseCode = _clsRestService.PutRequestMultiDimesionJsonString("", serviceData);
        //            }

        //            if (fgResponseCode != 0)
        //            {
        //                MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                MessageBox.Show(_clsRestService.globalResponseMessage.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
          
        //}

        private void cmbRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillGuestData();        

            fillServiceByRoomId(cmbRoomNo.Text.Trim());
        }

        private void cmbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtServiceAmount.Text = getPriceByRoomId(cmbServices.SelectedValue.ToString());
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    
        

    }
}
