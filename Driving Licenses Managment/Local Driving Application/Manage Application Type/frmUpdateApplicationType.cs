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
    public partial class frmUpdateApplicationType : Form
    {
        private int _ApplicationID = 0;
        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationID = ApplicationTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        clsManageApplicationType _applicationType = new clsManageApplicationType();
        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _applicationType = clsManageApplicationType.Find(_ApplicationID);
            if (_applicationType != null)
            {
                lblApplicationTypeID.Text = _applicationType._ApplicationID.ToString();
                txtTitle.Text = _applicationType._ApplicationName;
                txtFees.Text = _applicationType._Fees.ToString();
            }
            else
                MessageBox.Show("ApplicationType Not Found!");
            return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _applicationType._Fees = Convert.ToSingle(txtFees.Text.Trim());
            _applicationType._ApplicationName = txtTitle.Text.Trim();

            if(_applicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                txtTitle.Focus();
                errorProvider.SetError(txtTitle, "Please enter Test Title!");

            }
            else
            {
                //e.Cancel = true;
                //errorProvider.SetError(CTBFirstName, null);
                errorProvider.Clear();
                return;
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                txtFees.Focus();
                errorProvider.SetError(txtFees, "Please enter Test Fees!");

            }
            else if (int.TryParse(txtFees.Text, out int Fees) == false)
            {
                e.Cancel = true;
                txtFees.Focus();
                errorProvider.SetError(txtFees, "Only Numbers Allowed!");
            }
            else
            {
                //e.Cancel = true;
                //errorProvider.SetError(CTBFirstName, null);
                errorProvider.Clear();
                return;
            }
        }
    }
}
