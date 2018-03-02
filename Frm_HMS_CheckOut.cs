using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HMS.Custom_Classes.Service_Classes;
using HMS.Custom_Classes;
namespace HMS
{
    public partial class Frm_HMS_CheckOut : Form
    {

        #region Initialization and Declaration

        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        ClsRooms _clsRooms = new ClsRooms();
        CommonServices _commonServices = new CommonServices();
        string _message = string.Empty;
        #endregion
        public Frm_HMS_CheckOut()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Utilities

        private void fillRooms()
        {

            
            var dt = new DataTable();

            
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

        private void fillServiceByRoomId(string roomNo)
        {
            var ds = new DataSet();
            ds = _commonServices.getServiceByRoomId(roomNo);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                datagridview1.DataSource = ds.Tables[0];
                 setTotalAmount();
            }
            else
            {
                datagridview1.DataSource = null;
            }
            
        }

        private void fillGuestData()
        {
            try
            {
                var dt = new DataTable();
                var ds = new DataSet();
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void setTotalAmount()
        {
            double total = 0;
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
            if (discount.ToString() != "0")
            {
                total = GetGrandTotalAmount(total, discount.ToString(), true);
            }

            if (CGST.ToString() != "0")
            {
                total = GetGrandTotalAmount(total, CGST.ToString(), false);
            }
            if (SGST.ToString() != "0")
            {
                total = GetGrandTotalAmount(total, SGST.ToString(),false);
            }
           
            txtTotal.Text = total.ToString();
            
           
        }

        private double GetGrandTotalAmount(Double total, string value, bool fgDiscount)
        {
            var total1 = (Convert.ToDouble(total)) * (Convert.ToDouble(value.ToString()));

            total1 = total1 / 100;
            if (fgDiscount)
            {
                total1 = (System.Math.Round( total - total1));
            }
            else
            {
                total1 = (System.Math.Round( total + total1));
            }
            
            return total1;
        }

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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void resetData()
        {
            fillRooms();
            fillServiceByRoomId(cmbRoomNo.Text);
            setTotalAmount();
            cmbPaymentMode.SelectedIndex = 0;
            txtCheckOutTime.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        #endregion

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Frm_HMS_CheckOut_Load(object sender, EventArgs e)
        {
            fillRooms();
            cmbPaymentMode.SelectedIndex = 0;
             txtCheckOutTime.Text = DateTime.Now.ToString("hh:mm:ss");
         
        }

        private void cmbRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillServiceByRoomId(cmbRoomNo.Text);
            fillGuestData();
            setTotalAmount();
        }

        private void Frm_HMS_CheckOut_FormClosing(object sender, FormClosingEventArgs e)
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

        private void BtnCheckOut_Click(object sender, EventArgs e)
        {
            string userId = string.Empty,paymentMode = string.Empty,checkoutDt = string.Empty,checkoutTime = string.Empty,trnMode=string.Empty;
            string reservationId = string.Empty;
            int discount = 0,cgst=0,sgst=0,roomNo=0;
            decimal totalAmount = 0;
             userId = _clsGlobalConstants.glvUserId;
             reservationId = txtRegNo.Text;
            roomNo = Convert.ToInt32(cmbRoomNo.Text);
            if (MessageBox.Show("Are you sure you want to check out ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                                              
                paymentMode = cmbPaymentMode.Text;
                checkoutDt = dtpDpDate.Text;
                checkoutTime = txtCheckOutTime.Text;
                trnMode = "ADD";
                discount = Convert.ToInt32(string.IsNullOrEmpty(txtDiscountPer.Text) ? "0" : txtDiscountPer.Text);
                cgst = Convert.ToInt32(string.IsNullOrEmpty(txtCgstPer.Text) ? "0" : txtCgstPer.Text);
                sgst = Convert.ToInt32(string.IsNullOrEmpty(txtSgstPer.Text) ? "0" : txtSgstPer.Text);

                totalAmount = Convert.ToDecimal( string.IsNullOrEmpty (txtTotal.Text)? "0": txtTotal.Text);
                 if (chkReckin.Checked)
                 {
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
                    trnMode, true);
                 }
                 else
                 {
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
                      trnMode,false);
                 }
              

                if (!_message.Equals(_clsGeneralLibrary.message))
                {
                    MessageBox.Show(_commonServices._message.ToString());
                }
                else
                {
                    MessageBox.Show("Operation sucessfully completed....!!!!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (MessageBox.Show("Print Bill ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        printBill(userId, reservationId, roomNo);
                    }
                }
              //  resetData();
            }
            
                

        }

        

       

    }
}
