using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using HMS.Custom_Classes;
using HMS.Custom_Classes.Service_Classes;
using HMS.DataSets;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using HMS.Reports;
namespace HMS
{
    public partial class Frm_Hms_ReportViewer : Form
    {
        CommonServices _commonServices = new CommonServices();
        public string reportName { get; set; }
        public Frm_Hms_ReportViewer()
        {
            InitializeComponent();
        }
        //public Frm_Hms_ReportViewer( string rptName)
        //{
        //    reportName = rptName;
        //    if (rptName == "test")
        //    {
        //        CrystalReport1 crystal = new CrystalReport1();
        //        var ds = new DataSet();
        //        ds = _commonServices.getServiceByRoomId("1");


        //        crystal.SetDataSource(ds.Tables[0]);

        //        this.crystalReportViewer1.ReportSource = crystal;
        //        this.crystalReportViewer1.RefreshReport();
        //    }
        //}

        private void Frm_Hms_ReportViewer_Load(object sender, EventArgs e)
        {
            //CustomerReport crystalReport = new CustomerReport();
            //Customers dsCustomers = GetData();
            //if (reportName == "test")
            //{
            //    CrystalReport1 crystal = new CrystalReport1();
            //    var ds = new DataSet();
            //    ds = _commonServices.getServiceByRoomId("1");


            //    crystal.SetDataSource(ds.Tables[0]);

            //    this.crystalReportViewer1.ReportSource = crystal;
            //    this.crystalReportViewer1.RefreshReport();
            //}
        }

        public void LinkReport(CrystalDecisions.CrystalReports.Engine.ReportDocument cr)
        {
            crystalReportViewer1.ReportSource = cr;
            this.crystalReportViewer1.RefreshReport();
        }

        
    }
}
