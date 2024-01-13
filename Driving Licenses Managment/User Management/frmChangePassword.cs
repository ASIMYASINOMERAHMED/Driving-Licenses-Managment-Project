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
    public partial class frmChangePassword : Form
    {
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            clsUser user = clsUser.FindByUserID(UserID);
            _User = user;
            ctrUserCard1.PrintUserCard(_User.UserID);
     
        }
        clsUser _User=new clsUser();
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtCurrentPassword.Focus();
                errorProvider.SetError(txtCurrentPassword, "Please enter Current Password!");

            }
            else if(txtCurrentPassword.Text.Trim() != ctrUserCard1.CurrentPassword.Trim())
            {
                e.Cancel = true;
                txtCurrentPassword.Focus();
                errorProvider.SetError(txtCurrentPassword, "Worng Password!");
            }
            else
            {
                errorProvider.Clear();
                return;
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider.SetError(txtNewPassword, "Please enter New Password!");

            }
            else
            {
                errorProvider.Clear();
                return;
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();
                errorProvider.SetError(txtConfirmPassword, "Please Confirm Password!");

            }
            else if(txtConfirmPassword.Text.Trim()!=txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();
                errorProvider.SetError(txtConfirmPassword, "Password does not Match!");
            }
            else
            {
                errorProvider.Clear();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds are not Valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.Password = clsUtil.ComputeHash(txtNewPassword.Text);
            if (_User.Save())
            {
                MessageBox.Show("Password Updated Successfully.","success",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Password Couldn't Updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }
    }
}
