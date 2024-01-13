using Driving_Licenses_Managment.Properties;
using DVLDBussiness1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Driving_Licenses_Managment
{
    public partial class frmAddEditPerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGendor { Male = 0, Female = 1 };

        private enMode _Mode;
        private int _PersonID = -1;
        clsPerson _Person;
        public frmAddEditPerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

        }

        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }
        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lbTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lbTitle.Text = "Update Person";
            }

            //set default image for the person.
            if (rbMale.Checked)
                picPerson.Image = Resources.Male_512;
            else
                picPerson.Image = Resources.Female_512;

            //hide/show the remove linke incase there is no image for the person.
            llRemove.Visible = (picPerson.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            DtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            DtpDateOfBirth.Value = DtpDateOfBirth.MaxDate;

            //should not allow adding age more than 100 years
            DtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to jordan.
            cbCountry.SelectedIndex = cbCountry.FindString("Egypt");

            textBoxFirst.Text = "";
            textBoxSecond.Text = "";
            textBoxThird.Text = "";
            textBoxLast.Text = "";
            textBoxNationalNo.Text = "";
            rbMale.Checked = true;
            textBoxPhone.Text = "";
            textBoxEmail.Text = "";
            textBoxAddress.Text = "";
        }
        private void _LoadData()
        {
            _Person = clsPerson.FindPerson(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("This Form Will Be Closed Because Person with ID " + _PersonID+" is Deleted");
                this.Close();
                return;
            }
            lbPersonNo.Text = _Person.PersonID.ToString();
            textBoxFirst.Text = _Person.FirstName;
            textBoxSecond.Text = _Person.SecondName;
            textBoxThird.Text = _Person.ThirdName;
            textBoxLast.Text = _Person.LastName;
            textBoxEmail.Text = _Person.Email;
            textBoxPhone.Text = _Person.Phone;
            textBoxAddress.Text = _Person.Address;
            DtpDateOfBirth.Value = _Person.DateOfBirth;
            textBoxNationalNo.Text = _Person.NationalityNo;
            if (_Person.Gendor == 0)
                rbMale.Checked=true;
            else
                rbFemale.Checked = true;
            if (_Person.ImagePath != "")
            {
                picPerson.ImageLocation = _Person.ImagePath;
            }
            llRemove.Visible = (_Person.ImagePath != "");
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloseX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_HandlePersonImage())
                return;
            _Person.NationalityCountryID = clsCountry.Find(cbCountry.Text).ID;
            _Person.FirstName = textBoxFirst.Text;
            _Person.SecondName = textBoxSecond.Text;
            _Person.ThirdName = textBoxThird.Text;
            _Person.LastName = textBoxLast.Text;
            _Person.Email = textBoxEmail.Text;
            _Person.Address = textBoxAddress.Text;
            _Person.Phone = textBoxPhone.Text;
            _Person.DateOfBirth = DtpDateOfBirth.Value;
            _Person.NationalityNo = textBoxNationalNo.Text;
            if (rbMale.Checked)
                _Person.Gendor = (short)enGendor.Male;
            else
                _Person.Gendor = (short)enGendor.Female;
            if (picPerson.ImageLocation != null)
            {
                _Person.ImagePath = picPerson.ImageLocation;
            }
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {

                MessageBox.Show("Data Save Succssfully.");
                _Mode = enMode.Update;
                lbTitle.Text = "Edit Person";
                lbPersonNo.Text = _Person.PersonID.ToString();
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != picPerson.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(picPerson.ImageLocation);
                    }
                    catch (IOException ex)
                    {
                        //log error later
                    }
                    if (picPerson.ImageLocation != null)
                    {
                        string SourceImageFile = picPerson.ImageLocation;
                        if (clsUtil.CopyImageToProjectFileImages(ref SourceImageFile))
                        {
                            picPerson.ImageLocation = SourceImageFile;
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                picPerson.Load(selectedFilePath);
                llRemove.Visible = true;
                // ...
            }

        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            picPerson.ImageLocation = null;
            if (rbMale.Checked)
                picPerson.Image = Resources.Male_512;
            else
                picPerson.Image = Resources.Female_512;
            llRemove.Visible = false;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            System.Windows.Forms.TextBox Temp = ((System.Windows.Forms.TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider.SetError(Temp, null);
            }

        }
     

        private void textBoxNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider.SetError(textBoxNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (textBoxNationalNo.Text.Trim() != _Person.NationalityNo && clsPerson.isPersonExist(textBoxNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider.SetError(textBoxNationalNo, null);
            }
        }
    

   
        private bool IsValidEmail(string email)
        {
            string trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                MailAddress addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        private void textBoxEmail_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!IsValidEmail(textBoxEmail.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider.SetError(textBoxEmail, null);
            };
        }

     

   

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if(_Mode==enMode.Update)
                _LoadData();
        }
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (picPerson.ImageLocation == null)
                picPerson.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (picPerson.ImageLocation == null)
                picPerson.Image = Resources.Female_512;
        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
