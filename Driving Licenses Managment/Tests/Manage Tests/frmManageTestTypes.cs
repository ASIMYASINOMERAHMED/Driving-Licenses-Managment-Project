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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }
        private DataTable _dtAllTestTypes;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestTypes testTypes = new frmUpdateTestTypes((int)DGVManageTestType.CurrentRow.Cells[0].Value);
            testTypes.StartPosition = FormStartPosition.CenterScreen;
            testTypes.ShowDialog();
            frmManageTestTypes_Load(null, null);
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _dtAllTestTypes = clsManageTestType.GetTestTypes();
            DGVManageTestType.DataSource = _dtAllTestTypes;
            lbRecords.Text = DGVManageTestType.Rows.Count.ToString();

            if(DGVManageTestType.Rows.Count > 0 )
            {
                DGVManageTestType.Columns[0].HeaderText = "ID";
                DGVManageTestType.Columns[0].Width = 120;

                DGVManageTestType.Columns[1].HeaderText = "Title";
                DGVManageTestType.Columns[1].Width = 200;

                DGVManageTestType.Columns[2].HeaderText = "Description";
                DGVManageTestType.Columns[2].Width = 400;

                DGVManageTestType.Columns[3].HeaderText = "Fees";
                DGVManageTestType.Columns[3].Width = 100;
            }
           
        }
    }
}
