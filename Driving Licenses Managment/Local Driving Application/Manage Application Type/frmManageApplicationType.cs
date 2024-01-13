using DVLDBussiness1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment
{
    public partial class frmManageApplicationType : Form
    {
        public frmManageApplicationType()
        {
            InitializeComponent();
        }
        private DataTable _dtAllApplicationTypes;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageApplicationType_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationType.GetApplicationTypes();
            DGVManageApplicationType.DataSource = _dtAllApplicationTypes;
            lbRecords.Text = DGVManageApplicationType.Rows.Count.ToString();

            DGVManageApplicationType.Columns[0].HeaderText = "ID";
            DGVManageApplicationType.Columns[0].Width = 110;

            DGVManageApplicationType.Columns[1].HeaderText = "Title";
            DGVManageApplicationType.Columns[1].Width = 400;

            DGVManageApplicationType.Columns[2].HeaderText = "Fees";
            DGVManageApplicationType.Columns[2].Width = 100;
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType applicationType = new frmUpdateApplicationType((int)DGVManageApplicationType.CurrentRow.Cells[0].Value);
            applicationType.StartPosition = FormStartPosition.CenterScreen;
            applicationType.ShowDialog();
            frmManageApplicationType_Load(null, null);

        }
    }
}
