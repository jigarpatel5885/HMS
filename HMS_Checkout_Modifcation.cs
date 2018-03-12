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
    public partial class HMS_Checkout_Modifcation : Form
    {
        #region Initialization & Declaration
        ClsRestService _clsRestService = new ClsRestService();
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();       
        CommonServices _commonServices = new CommonServices();
       
        string _message = string.Empty;

        #endregion
        public HMS_Checkout_Modifcation()
        {
            InitializeComponent();
        }

        private void BtnCheckOut_Click(object sender, EventArgs e)
        {
            string userId = string.Empty,paymentMode = string.Empty,checkoutDt = string.Empty,checkoutTime = string.Empty,trnMode=string.Empty;
            string reservationId = string.Empty;
            int discount = 0,cgst=0,sgst=0,roomNo=0;
            decimal totalAmount = 0;
            decimal totalServiceAmt = 0;
             userId = _clsGlobalConstants.glvUserId;
             reservationId = txtRegNo.Text;
            roomNo = Convert.ToInt32(txtRoomNo.Text);
            if (MessageBox.Show("Are you sure you want to check out ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                paymentMode = cmbPaymentMode.Text;
                checkoutDt = dtpDpDate.Text;
                checkoutTime = txtCheckOutTime.Text;
                trnMode = "UPDATE";
                discount = Convert.ToInt32(string.IsNullOrEmpty(txtDiscountPer.Text) ? "0" : txtDiscountPer.Text);
                cgst = Convert.ToInt32(string.IsNullOrEmpty(txtCgstPer.Text) ? "0" : txtCgstPer.Text);
                sgst = Convert.ToInt32(string.IsNullOrEmpty(txtSgstPer.Text) ? "0" : txtSgstPer.Text);
                totalServiceAmt = Convert.ToDecimal(string.IsNullOrEmpty(txtTotalServiceAmt.Text) ? "0" : txtTotalServiceAmt.Text);
                totalAmount = Convert.ToDecimal(string.IsNullOrEmpty(txtTotal.Text) ? "0" : txtTotal.Text);
               
                    _message = _commonServices.setCustomerCheckOut(userId,
                     paymentMode,
                     roomNo,
                     checkoutDt,
                     checkoutTime,
                     reservationId,
                     discount,
                     sgst,
                     cgst,
                     totalAmount,
                     trnMode,
                     totalServiceAmt,
                     false);
               


                if (!_message.Equals(_clsGeneralLibrary.message))
                {
                    MessageBox.Show(_commonServices._message.ToString());
                }
                else
                {
                    MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //if (MessageBox.Show("Print Bill ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    printBill(userId, reservationId, roomNo);
                    resetData();
                    //}

                }
            }
              //  resetData();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void HMS_Checkout_Modifcation_Load(object sender, EventArgs e)
        {
            fillServiceNames();
            
        }

        #region Utilities


       private void printBill(string userId, string reservationId, int roomNo)
        {
            try
            {
                var ds = new DataSet();
                var invoiceDataSet = new DataSet();
                var dt = new DataTable();
                var dtServicesData = new DataTable();
                var dtColumns = new DataColumn();
                //var dtRows = new DataRow();
                ds = _commonServices.getInvoiceDetails(userId, reservationId, roomNo);
                // invoiceDataSet.Tables.Add(ds.Tables[0]);
                // invoiceDataSet.Tables.Add(ds.Tables[1]);
                if (ds.Tables.Count >2)
                {
                    var list = new List<string>();
                    dt.Columns.Add("DAY1");
                    dt.Columns.Add("DAY2");
                    dt.Columns.Add("DAY3");
                    dt.Columns.Add("DAY4");
                    dt.Columns.Add("DAY5");
                    dt.Columns.Add("DAY6");
                    dt.Columns.Add("DAY7");

                    dtServicesData.Columns.Add("SERVICE_NAME");
                    dtServicesData.Columns.Add("DAY1");
                    dtServicesData.Columns.Add("DAY2");
                    dtServicesData.Columns.Add("DAY3");
                    dtServicesData.Columns.Add("DAY4");
                    dtServicesData.Columns.Add("DAY5");
                    dtServicesData.Columns.Add("DAY6");
                    dtServicesData.Columns.Add("DAY7");

                    DataRow dr = dt.NewRow();
                    DataRow drServicesRow = dtServicesData.NewRow();
                    for (int i = 1; i <= ds.Tables[2].Columns.Count - 1; i++)
                    {
                        list.Add(ds.Tables[2].Columns[i].ColumnName.ToString());
                    }
                    dr.ItemArray = list.ToArray();
                    dt.Rows.Add(dr);
                    dt.TableName = "SERVICESHEADER";
                    list.Clear();
                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        dtServicesData.Rows.Add(row.ItemArray);
                    }
                    dtServicesData.TableName = "SERVICES";
                    //  drServicesRow.ItemArray = list.ToArray();
                    // dtServicesData.Rows.Add(drServicesRow); 
                    ds.Tables.RemoveAt(2);
                    //  var day1 = ds.Tables[2].Rows[0][1].ToString(); 
                    ds.Tables[0].TableName = "CUSTOMERDETAILS";
                    ds.Tables[1].TableName = "SALES";

                    ds.Tables.Add(dt);
                    ds.Tables.Add(dtServicesData);
                    //invoiceDataSet.Tables.Add(dt);
                    Reports.Invoice cr = new Reports.Invoice();
                    cr.SetDataSource(ds);
                    Frm_Hms_ReportViewer rpt1 = new Frm_Hms_ReportViewer();
                    rpt1.LinkReport(cr);
                    rpt1.Show();
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void resetData()
        {
          txtInvoiceNo.Text="";
            
            setTotalAmount();
            cmbPaymentMode.SelectedIndex = 0;
            txtCheckOutTime.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void setTotalAmount()
        {
            double total = 0, discountTot = 0, cgstTot = 0, sgstTot = 0;
            var discount = txtDiscountPer.Text;
            var CGST = txtCgstPer.Text;
            var SGST = txtSgstPer.Text;
            if (discount.ToString() == "")
            {
                discount = "0";
            }
            if (CGST.ToString() == "")
            {
                CGST = "0";
            }
            if (SGST.ToString() == "")
            {
                SGST = "0";
            }

            foreach (DataGridViewRow dr in datagridview1.Rows)
            {
                total += Convert.ToDouble(dr.Cells["Price"].Value.ToString());

            }
            txtTotal.Text = total.ToString();
            if (discount.ToString() != "0")
            {
                discountTot = (Convert.ToDouble(txtTotalServiceAmt.Text)) * (Convert.ToDouble(discount.ToString()));

                discountTot = discountTot / 100;
                txtTotal.Text = System.Math.Round(((Convert.ToDouble(txtTotalServiceAmt.Text) - discountTot) + cgstTot + sgstTot)).ToString();

            }

            if (CGST.ToString() != "0")
            {

                cgstTot = (Convert.ToDouble(txtTotalServiceAmt.Text)) * (Convert.ToDouble(CGST.ToString().ToString()));
                cgstTot = cgstTot / 100;
                txtTotal.Text = System.Math.Round(((Convert.ToDouble(txtTotalServiceAmt.Text) - discountTot) + cgstTot + sgstTot)).ToString();
            }
            if (SGST.ToString() != "0")
            {
                sgstTot = (Convert.ToDouble(txtTotalServiceAmt.Text)) * (Convert.ToDouble(SGST.ToString()));
                sgstTot = sgstTot / 100;
                txtTotal.Text = System.Math.Round(((Convert.ToDouble(txtTotalServiceAmt.Text) - discountTot) + cgstTot + sgstTot)).ToString();
            }

        }
        private void fillServiceNames()
        {
            var ds = new DataSet();
            ds = _commonServices.getComboDetails(_clsGlobalConstants.glvUserId, "SERVICE_LIST");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbServices, "Code_Descr", "Key");
            }
            else
            {
                MessageBox.Show(_commonServices._message);
            }
        }
        private void fillServiceByRoomIdRid(string roomNo,string reservationId)
        {
            var ds = new DataSet();
            ds = _commonServices.getServiceByRoomIdRid(roomNo, reservationId);

            if (ds.Tables.Count > 0)
            {
                datagridview1.DataSource = ds.Tables[0];
                setTotalAmount();
            }
            else
            {
                datagridview1.DataSource = null;
            }

        }

        private void SetTotalServiceAmount()
        {
            double total = 0;

            if (datagridview1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in datagridview1.Rows)
                {
                    total += Convert.ToDouble(dr.Cells["Price"].Value.ToString());

                }
            }
            txtTotalServiceAmt.Text = total.ToString();
        }
         private void fillGuestData(string invoiceNo)
        {
            try
            {
                var dt = new DataTable();
                var ds = new DataSet();

                ds = _commonServices.getCustomerCheckinByInvoice(invoiceNo);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];

                        foreach (DataRow row in dt.Rows)
                        {
                            txtName.Text = row["NAME"].ToString();
                            txtRegNo.Text = row["Reservation_Id"].ToString();
                            txtArTime.Text = row["AR_Time"].ToString();
                            txtTotalPerson.Text = row["Total_Guest"].ToString();
                            txtRoomNo.Text = row["Room_No"].ToString();
                        }
                    }                              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

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

         private Boolean validateCheckBoxInsideGrid()
         {
             Boolean fgCheck = false;
             for (int i = 0; i < datagridview1.Rows.Count; i++)
             {
                 bool isSelected = Convert.ToBoolean(datagridview1.Rows[i].Cells["chkItems"].Value);
                 if (isSelected)
                 {
                     fgCheck = true;
                 }
             }

             return fgCheck;
         }
        #endregion

         private void btnGet_Click(object sender, EventArgs e)
         {
             if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
             {
                 fillGuestData(txtInvoiceNo.Text.Trim());
                 fillServiceByRoomIdRid(txtRoomNo.Text.Trim(), txtRegNo.Text.Trim());
                 SetTotalServiceAmount();
             }
             else
             {
                 MessageBox.Show("Please enter invoice number...");
             }
         }

         private void cmbServices_SelectedIndexChanged(object sender, EventArgs e)
         {
             txtServiceAmount.Text = getPriceByRoomId(cmbServices.SelectedValue.ToString());
         }

         private void btnAdd_Click(object sender, EventArgs e)
         {
             string userId = string.Empty, reservationId = string.Empty, serviceTime = string.Empty, serviceDate = string.Empty,
                remarks = string.Empty, trnMode = string.Empty;
             decimal price = 0;
             int roomNo = 0, serviceId = 0;

             if (txtServiceAmount.Text != string.Empty)
             {
                 userId = _clsGlobalConstants.glvUserId;
                 serviceId = Convert.ToInt32(cmbServices.SelectedValue.ToString());
                 reservationId = txtRegNo.Text;
                 serviceDate = dtpServiceDate.Text;
                 serviceTime = txtServiceTime.Text;
                 remarks = txtRemark.Text;
                 trnMode = "ADD";
                 roomNo = Convert.ToInt32(txtRoomNo.Text);
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
                     fillServiceByRoomIdRid(txtRoomNo.Text.Trim(), txtRegNo.Text.Trim());
                     setTotalAmount();
                     SetTotalServiceAmount();
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
                         for (int i = 0; i < datagridview1.Rows.Count; i++)
                         {

                             bool isSelected = Convert.ToBoolean(datagridview1.Rows[i].Cells["chkItems"].Value);
                             if (isSelected)
                             {
                                 serviceId = Convert.ToInt32(datagridview1.Rows[i].Cells["SERVICE_ID"].Value.ToString());
                                 reservationId = txtRegNo.Text;
                                 serviceDate = datagridview1.Rows[i].Cells["SERVICE_DATE"].Value.ToString();
                                 serviceTime = datagridview1.Rows[i].Cells["SERVICE_TIME"].Value.ToString();
                                 remarks = datagridview1.Rows[i].Cells["REMARKS"].Value.ToString();
                                 trnMode = "DELETE";
                                 roomNo = Convert.ToInt32(txtRoomNo.Text.Trim());
                                 price = Convert.ToDecimal(datagridview1.Rows[i].Cells["PRICE"].Value.ToString());

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

                             fillServiceByRoomIdRid(txtRoomNo.Text.Trim(), txtRegNo.Text.Trim());
                             setTotalAmount();
                             SetTotalServiceAmount();     
                         }
                     }
                     else
                     {
                         var row = new List<DataGridViewRow>();
                         for (int i = 0; i < datagridview1.Rows.Count; i++)
                         {
                             bool isSelected = Convert.ToBoolean(datagridview1.Rows[i].Cells["chkItems"].Value);
                             if (isSelected)
                             {
                                 row.Add(datagridview1.Rows[i]);
                             }
                         }

                         for (int j = 0; j < row.Count; j++)
                         {
                             datagridview1.Rows.Remove(row[j]);
                         }

                         foreach (DataGridViewRow item in datagridview1.Rows)
                         {
                             DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)item.Cells["chkItems"];
                             cell.Value = false;
                         }
                         datagridview1.ClearSelection();
                     }
                     setTotalAmount();
                 }
             }
             else
             {
                 MessageBox.Show("Select items to be removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
         }

         private void HMS_Checkout_Modifcation_FormClosing(object sender, FormClosingEventArgs e)
         {
             this.Dispose();
         }

         private void txtDiscountPer_TextChanged(object sender, EventArgs e)
         {
             setTotalAmount();
         }

         private void txtCgstPer_TextChanged(object sender, EventArgs e)
         {
             setTotalAmount();
         }

         private void txtSgstPer_TextChanged(object sender, EventArgs e)
         {
             setTotalAmount();
         }

        
    }
}
