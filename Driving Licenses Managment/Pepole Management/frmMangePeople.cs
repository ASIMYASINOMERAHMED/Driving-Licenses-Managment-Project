using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBussiness1;

namespace Driving_Licenses_Managment
{
    public partial class frmMangePeople : Form
    {
        private static DataTable _dtAllPeople = new DataTable();
        private DataTable _dtPeople = new DataTable();
        public frmMangePeople()
        {
            InitializeComponent();
    
        }
        private void _RefreshPeople()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                  "FirstName", "SecondName", "ThirdName", "LastName",
                                                  "Gendor", "DateOfBirth", "Nationality",
                                                  "Phone", "Email");

            DGVPeople.DataSource = _dtPeople;
   
            cbFilterBy.SelectedIndex = 0;
        
            if (DGVPeople.Rows.Count > 0)
            {
                DGVPeople.Columns[0].HeaderText = "Person ID";
                DGVPeople.Columns[0].Width = 110;

                DGVPeople.Columns[1].HeaderText = "National No.";
                DGVPeople.Columns[1].Width = 120;

                DGVPeople.Columns[2].HeaderText = "First Name";
                DGVPeople.Columns[2].Width = 120;

                DGVPeople.Columns[3].HeaderText = "Second Name";
                DGVPeople.Columns[3].Width = 140;

                DGVPeople.Columns[4].HeaderText = "Third Name";
                DGVPeople.Columns[4].Width = 120;

                DGVPeople.Columns[5].HeaderText = "Last Name";
                DGVPeople.Columns[5].Width = 120;

                DGVPeople.Columns[6].HeaderText = "Gendor";
                DGVPeople.Columns[6].Width = 120;

                DGVPeople.Columns[7].HeaderText = "Date Of Birth";
                DGVPeople.Columns[7].Width = 140;

                DGVPeople.Columns[8].HeaderText = "Nationality";
                DGVPeople.Columns[8].Width = 120;

                DGVPeople.Columns[9].HeaderText = "Phone";
                DGVPeople.Columns[9].Width = 120;

                DGVPeople.Columns[10].HeaderText = "Email";
                DGVPeople.Columns[10].Width = 170;
            }
            lbRecords.Text = DGVPeople.Rows.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloseX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloseX_MouseEnter(object sender, EventArgs e)
        {
            btnCloseX.BackColor = Color.Red;
        }

        private void btnCloseX_MouseLeave(object sender, EventArgs e)
        {
            btnCloseX.BackColor = SystemColors.HotTrack;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxfilter.Visible = (cbFilterBy.Text != "None");

            if (textBoxfilter.Visible)
            {
                textBoxfilter.Text = "";
                textBoxfilter.Focus();
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAddEdit = new frmAddEditPerson();
            frmAddEdit.StartPosition = FormStartPosition.CenterScreen;
            frmAddEdit.ShowDialog();
            _RefreshPeople();
        }

      
    

        private void textBoxfilter_TextChanged_1(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }
            if (textBoxfilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lbRecords.Text = DGVPeople.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, textBoxfilter.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, textBoxfilter.Text.Trim());

            lbRecords.Text = DGVPeople.Rows.Count.ToString();



        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            Form PersonInfo = new frmPersonDetails((int)DGVPeople.CurrentRow.Cells[0].Value);
            PersonInfo.StartPosition = FormStartPosition.CenterScreen;
            PersonInfo.ShowDialog();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            frmAddEditPerson frmAddEdit = new frmAddEditPerson((int)DGVPeople.CurrentRow.Cells[0].Value);
            frmAddEdit.StartPosition = FormStartPosition.CenterScreen;
            frmAddEdit.ShowDialog();
            _RefreshPeople();
        }

        private void addNewPersonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAddEdit = new frmAddEditPerson();
            frmAddEdit.StartPosition = FormStartPosition.CenterScreen;
            frmAddEdit.ShowDialog();
            _RefreshPeople();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete Person [" + DGVPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson((int)DGVPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.");
                    _RefreshPeople();
                    return;
                }
                else
                {
                    MessageBox.Show("Can Not Delete Person because There's Data Link To it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void textBoxfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 9)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
            else
                return;
        }

        private void frmMangePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeople();
          
        }

        private void phoneCallToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void sendEmailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
    }
}
