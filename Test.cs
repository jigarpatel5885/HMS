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
using HMS.DataSets;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace HMS
{
    public partial class Test : Form
    {
        CommonServices _commonServices = new CommonServices();
        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_HMS_Services _services = new Frm_HMS_Services();

            _services.ShowDialog();
        }


        //private void fillServiceByRoomId(string roomNo)
        //{
        //    var ds = new DataSet();
           
           
           
        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
               

        //    }
        //    else
        //    {
        //       // dataGridView1.DataSource = null;
        //    }
        //   // setTotalAmount();
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    var ds = new DataSet();
        //    ds = _commonServices.getServiceByRoomId("1");
        //    Reports.CrystalReport1 cr = new Reports.CrystalReport1();
        //    cr.SetDataSource(ds.Tables[0]);          
        //    Frm_Hms_ReportViewer rpt1 = new Frm_Hms_ReportViewer();
        //    rpt1.LinkReport(cr);
        //    rpt1.Show();

        //}

    }
}
