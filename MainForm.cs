using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HMS;
using HMS.Masters;
namespace HMS
{
    public partial class MainForm : Form
    {
        private int childFormNumber = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Equals("Check In"))
            {
                Frm_Hms_CheckIn _chkIn = new Frm_Hms_CheckIn();
                _chkIn.MdiParent = this;
                _chkIn.Dock = DockStyle.Fill;
                _chkIn.Show();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripDateTimeStatus.Text = DateTime.Now.ToString("dd/MM/yyyy");
            toolStripStatusTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
           
        }

        private void checkOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Equals("Check Out"))
            {
                Frm_HMS_CheckOut _chkOut = new Frm_HMS_CheckOut();
                _chkOut.MdiParent = this;
                _chkOut.Dock = DockStyle.Fill;
                _chkOut.Show();
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {        //    Frm_HMS_CheckOut _chkOut = new Frm_HMS_CheckOut();
        //    _chkOut.MdiParent = this;
        //    _chkOut.Dock = DockStyle.Fill;
        //    _chkOut.Show();
        }

        private void corporateClientEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_HMS_CorporateClientEntry _corporateClient = new Frm_HMS_CorporateClientEntry();
            _corporateClient.MdiParent = this;
            _corporateClient.Dock = DockStyle.Fill;
            _corporateClient.Show();
        }

        //Frm_HMS_CorporateClientEntry _corporateClient = new Frm_HMS_CorporateClientEntry();
        //    _corporateClient.MdiParent = this;
        //    _corporateClient.Dock = DockStyle.Fill;
        //    _corporateClient.Show();
    }
}
