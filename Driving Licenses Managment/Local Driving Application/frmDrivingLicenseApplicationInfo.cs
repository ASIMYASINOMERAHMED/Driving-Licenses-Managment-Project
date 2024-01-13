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
    public partial class frmDrivingLicenseApplicationInfo : Form
    {
        private int _ApplicationID = -1;
        public frmDrivingLicenseApplicationInfo(int LocalApplicationID)
        {
            InitializeComponent();
            _ApplicationID = LocalApplicationID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_ApplicationID);
        }
    }
}
