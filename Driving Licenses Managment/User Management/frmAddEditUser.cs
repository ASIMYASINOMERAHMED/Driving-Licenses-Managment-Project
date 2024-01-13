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
    public partial class frmAddUpdateUser : Form
    {
        enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private clsUser _User;
        private int _UserID;
        public frmAddUpdateUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _UserID = UserID;
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values

            if (_Mode == enMode.AddNew)
            {
                lbTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();

                tpLoginInfo.Enabled = false;

                ctrPersonSelector1.FilterFocus();
            }
            else
            {
                lbTitle.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;


            }

            textBoxUserName.Text = "";
            textBoxPassword.Text = "";
            textBoxConfirmPassword.Text = "";
            checktboxIsActive.Checked = true;


        }

        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                MessageBox.Show("This Form Will Be Closed Because User with ID " + _UserID + " is Deleted");
                this.Close();
                return;
            }
            ctrPersonSelector1.LoadPersonInfo(_User.PersonID);
            textBoxPassword.Text = _User.Password;
            textBoxUserName.Text = _User.UserName;
            textBoxConfirmPassword.Text = _User.Password;
            checktboxIsActive.Checked = _User.IsActive;
            lbUserID.Text = _User.UserID.ToString();
     
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tabControl1.SelectedTab = tabControl1.TabPages["tpLoginInfo"];
                return;
            }
            if(ctrPersonSelector1.PersonID!=-1)
            {
                if(clsUser.IsUserExistForPersonID(ctrPersonSelector1.PersonID))
                {

                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrPersonSelector1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tabControl1.SelectedTab = tabControl1.TabPages["tpLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrPersonSelector1.FilterFocus();

            }
        }

        private void textBoxUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxUserName.Text.Trim()))
            {
                e.Cancel = true;
                textBoxUserName.Focus();
                errorProvider.SetError(textBoxUserName, "Please enter UserName!");

            }
            else
            {
              
                errorProvider.Clear();
                return;
            }
            if (_Mode == enMode.AddNew)
            {

                if (clsUser.IsUserExist(textBoxUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider.SetError(textBoxUserName, "username is used by another user");
                }
                else
                {
                    errorProvider.SetError(textBoxUserName, null);
                };
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_User.UserName != textBoxUserName.Text.Trim())
                {
                    if (clsUser.IsUserExist(textBoxUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider.SetError(textBoxUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider.SetError(textBoxUserName, null);
                    };
                }
            }
        }

        private void textBoxPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPassword.Text.Trim()))
            {
                e.Cancel = true;
                textBoxPassword.Focus();
                errorProvider.SetError(textBoxPassword, "Please enter Password!");

            }
            else
            {
                //e.Cancel = true;
                //errorProvider.SetError(CTBFirstName, null);
                errorProvider.Clear();
                return;
            }
        }

        private void textBoxConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                textBoxConfirmPassword.Focus();
                errorProvider.SetError(textBoxConfirmPassword, "Please Confirm Password!");

            }
            else if(textBoxConfirmPassword.Text.Trim()!=textBoxPassword.Text.Trim())
            {
                e.Cancel = true;
                textBoxConfirmPassword.Focus();
                errorProvider.SetError(textBoxConfirmPassword, "Password does not match!");
            }
            else
            {
                //e.Cancel = true;
                //errorProvider.SetError(CTBFirstName, null);
                errorProvider.Clear();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _User.IsActive = checktboxIsActive.Checked;
            _User.UserName = textBoxUserName.Text;
            _User.Password = clsUtil.ComputeHash(textBoxPassword.Text);
            if(_User.Save())
            {
                lbUserID.Text = _User.UserID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lbTitle.Text = "Update User";
                this.Text = "Update User";
                MessageBox.Show("User Saved Successfully.","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
        
                
            }
            else
                MessageBox.Show("Unable to Save User.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrPersonSelector1.FilterFocus();
        }
    }
}
