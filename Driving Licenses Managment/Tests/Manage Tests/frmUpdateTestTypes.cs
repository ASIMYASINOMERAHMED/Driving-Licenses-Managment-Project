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
    public partial class frmUpdateTestTypes : Form
    {
        private int _TestID = 0;
        public frmUpdateTestTypes(int TestID)
        {
            InitializeComponent();
            _TestID = TestID;
        }
        clsManageTestType testType = new clsManageTestType();

        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            testType = clsManageTestType.Find((clsManageTestType.enTestType) _TestID);
            if(testType != null)
            {
                lblTestTypeID.Text = testType._TestTypeID.ToString();
                txtTitle.Text = testType._TestTitle;
                txtDescription.Text = testType._TestDescription;
                txtFees.Text = testType._Fees.ToString();
            }
            else
                MessageBox.Show("Test Type Not Found!");
            return;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            testType._Fees = Convert.ToDecimal(txtFees.Text.Trim());
            testType._TestTitle = txtTitle.Text.Trim();
            testType._TestDescription = txtDescription.Text.Trim();

            if (testType.Save())
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

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                e.Cancel = true;
                txtDescription.Focus();
                errorProvider.SetError(txtDescription, "Please enter Test Description!");

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
            else if(int.TryParse(txtFees.Text,out int Fees))
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
