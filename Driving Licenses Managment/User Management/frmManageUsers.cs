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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Driving_Licenses_Managment
{
    public partial class frmManageUsers : Form
    {
        private static DataTable _dtAllUsers;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _RefreshUsers()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            DGVUsers.DataSource = _dtAllUsers;
            if (DGVUsers.Rows.Count ==0 )
            {
                lbNoUsers.Visible = true;
            }
            else { lbNoUsers.Visible = false; }
            lbRecords.Text = DGVUsers.Rows.Count.ToString();
            DGVUsers.Columns[0].HeaderText = "User ID";
            DGVUsers.Columns[0].Width = 110;

            DGVUsers.Columns[1].HeaderText = "Person ID";
            DGVUsers.Columns[1].Width = 120;

            DGVUsers.Columns[2].HeaderText = "Full Name";
            DGVUsers.Columns[2].Width = 350;

            DGVUsers.Columns[3].HeaderText = "UserName";
            DGVUsers.Columns[3].Width = 120;

            DGVUsers.Columns[4].HeaderText = "Is Active";
            DGVUsers.Columns[4].Width = 120;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloseX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                textBoxfilter.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {

                textBoxfilter.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    textBoxfilter.Enabled = false;
                }
                else
                    textBoxfilter.Enabled = true;

                textBoxfilter.Text = "";
                textBoxfilter.Focus();
            }
        }

        private void textBoxfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 3)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
            else
                return;
        }

        private void textBoxfilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }
            if (textBoxfilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lbRecords.Text = DGVUsers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, textBoxfilter.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, textBoxfilter.Text.Trim());

            lbRecords.Text = _dtAllUsers.Rows.Count.ToString();
            //DataTable dt = clsUser.GetAllUsers();
            //DataRow[] ResultRows;

            //if (textBoxfilter.Text != string.Empty)
            //{
            //    DataTable FilteredRows = dt.Clone();
            //    if (cbFilterBy.SelectedIndex == 1)
            //    {

            //            ResultRows = dt.Select($"UserID={textBoxfilter.Text}");

            //            foreach (DataRow row in ResultRows)
            //            {
            //                FilteredRows.ImportRow(row);
            //            }
            //            DGVUsers.DataSource = FilteredRows;

            //    }
            //    else if (cbFilterBy.SelectedIndex == 2)
            //    {

            //            ResultRows = dt.Select($"UserName='{textBoxfilter.Text}'");
            //            foreach (DataRow row in ResultRows)
            //            {
            //                FilteredRows.ImportRow(row);
            //            }
            //            DGVUsers.DataSource = FilteredRows;

            //    }
            //    else if (cbFilterBy.SelectedIndex == 3)
            //    {
            //        ResultRows = dt.Select($"PersonID={textBoxfilter.Text}");
            //        foreach (DataRow row in ResultRows)
            //        {
            //            FilteredRows.ImportRow(row);
            //        }
            //        DGVUsers.DataSource = FilteredRows;
            //    }
            //    else if (cbFilterBy.SelectedIndex == 4)
            //    {
            //        ResultRows = dt.Select($"FullName='{textBoxfilter.Text}'");
            //        foreach (DataRow row in ResultRows)
            //        {
            //            FilteredRows.ImportRow(row);
            //        }
            //        DGVUsers.DataSource = FilteredRows;
            //    }
            //}
            //else
            //    _RefreshUsers();

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lbRecords.Text = _dtAllUsers.Rows.Count.ToString();
            //DataTable dt = clsUser.GetAllUsers();
            //DataRow[] ResultRows;
            //DataTable FilteredRows = dt.Clone();
            //if (cbIsActive.SelectedIndex == 0) 
            //{
            //    _RefreshUsers();
            //}
            //else if( cbIsActive.SelectedIndex == 1) 
            //{
            //    ResultRows = dt.Select($"IsActive=1");
            //    foreach (DataRow row in ResultRows)
            //    {
            //        FilteredRows.ImportRow(row);
            //    }
            //    DGVUsers.DataSource = FilteredRows;
            //}
            //else if(cbIsActive.SelectedIndex == 2) 
            //{
            //    ResultRows = dt.Select($"IsActive=0");
            //    foreach (DataRow row in ResultRows)
            //    {
            //        FilteredRows.ImportRow(row);
            //    }
            //    DGVUsers.DataSource = FilteredRows;
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frmAddUpdate = new frmAddUpdateUser();
            frmAddUpdate.StartPosition = FormStartPosition.CenterScreen;
            frmAddUpdate.ShowDialog();
            _RefreshUsers();
          
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfoCard frmUserInfo = new frmUserInfoCard((int)DGVUsers.CurrentRow.Cells[0].Value);
            frmUserInfo.StartPosition = FormStartPosition.CenterScreen;
            frmUserInfo.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(clsUser.DeleteUser((int)DGVUsers.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("User Deleted Succssfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmManageUsers_Load(null, null);
            }
            else
                MessageBox.Show("User not Deleted due to data Link to it.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frmAddUpdate = new frmAddUpdateUser(-1);
            frmAddUpdate.StartPosition = FormStartPosition.CenterScreen;
            frmAddUpdate.ShowDialog();
            frmManageUsers_Load(null, null);
        }

        private void ChangePasswordtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = DGVUsers.CurrentRow;
            string UserID = currentRow.Cells[0].Value?.ToString();
            frmChangePassword changePassword = new frmChangePassword(Convert.ToInt32(UserID));
            changePassword.StartPosition = FormStartPosition.CenterScreen;
            changePassword.ShowDialog();
            frmManageUsers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = DGVUsers.CurrentRow;
            string UserID = currentRow.Cells[0].Value?.ToString();
            frmAddUpdateUser addUpdateUser = new frmAddUpdateUser(Convert.ToInt32(UserID));
            addUpdateUser.StartPosition = FormStartPosition.CenterScreen;
            addUpdateUser.ShowDialog();
            frmManageUsers_Load(null, null);
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshUsers();
         
        }
    }
}
