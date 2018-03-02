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
    public partial class Frm_Hms_ReprintInvoice : Form
    {

        #region Initialization and declaration
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        CommonServices _commonServices = new CommonServices();
        string _message = string.Empty;
        #endregion
        public Frm_Hms_ReprintInvoice()
        {
            InitializeComponent();
        }



        #region Utilities

        private void printBill(string userId, string InvoiceNumber)
        {
            try
            {
                var ds = new DataSet();
                var invoiceDataSet = new DataSet();
                var dt = new DataTable();
                var dtServicesData = new DataTable();
                var dtColumns = new DataColumn();
                //var dtRows = new DataRow();
                ds = _commonServices.getInvoiceByNumber(userId, InvoiceNumber);
                // invoiceDataSet.Tables.Add(ds.Tables[0]);
                // invoiceDataSet.Tables.Add(ds.Tables[1]);

                if (ds.Tables.Count > 0)
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
                else
                {
                    MessageBox.Show("Invoice not found !!","Not Found");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

        }

        #endregion

        private void BtnPrintBill_Click(object sender, EventArgs e)
        {
            string invoiceNo = string.Empty, userId = string.Empty;
            userId = _clsGlobalConstants.glvUserId;

            invoiceNo = txtInvoiceNo.Text;

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                printBill(userId, invoiceNo);
            }
            else
            {
                MessageBox.Show("Enter Invoice No.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

      
    }
}
