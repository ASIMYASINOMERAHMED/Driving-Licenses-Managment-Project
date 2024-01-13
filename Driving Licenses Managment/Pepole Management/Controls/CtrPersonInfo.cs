using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using DVLDBussiness1;
using System.Security.AccessControl;
using Driving_Licenses_Managment.Properties;
using System.IO;
using static Driving_Licenses_Managment.frmAddEditPerson;

namespace Driving_Licenses_Managment
{
    public partial class CtrPersonInfo : UserControl
    {
        public CtrPersonInfo()
        {
            InitializeComponent();
        }
        private clsPerson _Person;

        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }
        public void LoadPersonInfo(int PersonID)
        {
             _Person = clsPerson.FindPerson(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersnInfo();

        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.FindPerson(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with NationalNo = " + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersnInfo();

        }
        private void _FillPersnInfo()
        {
            _PersonID = _Person.PersonID;
            llEditPersonInfo.Enabled = true;
            lbPersonID.Text = _Person.PersonID.ToString();
            lbName.Text = _Person.FullName;
            lbDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lbAddress.Text = _Person.Address;
            lbCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            lbEmail.Text = _Person.Email;
            lbPhone.Text = _Person.Phone;
            lbGendor.Text = _Person.Gendor == 0? "Male":"Female";
            lbNationalNo.Text = _Person.NationalityNo;

            _LoadPersonImage();
     

        }
        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                picPerson.Image = Resources.Male_512;
            else
                picPerson.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if(ImagePath != "")
                if (File.Exists(ImagePath))
                    picPerson.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

           
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lbPersonID.Text = "[????]";
            lbNationalNo.Text = "[????]";
            lbName.Text = "[????]";
            picGendor.Image = Resources.Man_32;
            lbGendor.Text = "[????]";
            lbEmail.Text = "[????]";
            lbPhone.Text = "[????]";
            lbDateOfBirth.Text = "[????]";
            lbCountry.Text = "[????]";
            lbAddress.Text = "[????]";
            picPerson.Image = Resources.Male_512;

        }

    

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frmAddEdit = new frmAddEditPerson(_PersonID);
            frmAddEdit.StartPosition = FormStartPosition.CenterScreen;
            frmAddEdit.ShowDialog();

            LoadPersonInfo(_PersonID);
        }
    }
}
