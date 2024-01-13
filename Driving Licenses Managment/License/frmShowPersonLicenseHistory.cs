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
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonID = -1;
        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }
        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            this._PersonID = PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if(_PersonID!=-1)
            {
                ctrPersonSelector1.LoadPersonInfo(_PersonID);
                ctrPersonSelector1.FilterEnabled = false;
                ctrDriverLicenses1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctrPersonSelector1.FilterEnabled = true;
                ctrPersonSelector1.FilterFocus();
            }
        }
        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
            if (_PersonID == -1)
            {
                ctrDriverLicenses1.Clear();
            }
            else
                ctrDriverLicenses1.LoadInfoByPersonID(_PersonID);
        }
        }
    }
