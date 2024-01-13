using Driving_Licenses_Managment.Properties;
using DVLDBussiness1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment
{
    public partial class CtrInternationalDriverLicense : UserControl
    {
        private int _InternationalLicenseID;
        private clsInternationalLicense _InternaionalLicense;
        public CtrInternationalDriverLicense()
        {
            InitializeComponent();
        }
        public int InternationalLicenseID
        {
            get { return _InternationalLicenseID; }
        }

        private void _LoadImage()
        {
            if (_InternaionalLicense.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _InternaionalLicense.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public void LoadInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;
            _InternaionalLicense = clsInternationalLicense.Find(_InternationalLicenseID);
            if (_InternaionalLicense == null)
            {
                MessageBox.Show("Could not find Internationa License ID = " + _InternationalLicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }

            lblInternationalLicenseID.Text = _InternaionalLicense.InternationalLicenseID.ToString();
            lblApplicationID.Text = _InternaionalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternaionalLicense.IsActive ? "Yes" : "No";
            lblLocalLicenseID.Text = _InternaionalLicense.IssuedUsingLocalLicenseID.ToString();
            lblFullName.Text = _InternaionalLicense.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _InternaionalLicense.DriverInfo.PersonInfo.NationalityNo;
            lblGendor.Text = _InternaionalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = _InternaionalLicense.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();

            lblDriverID.Text = _InternaionalLicense.DriverID.ToString();
            lblIssueDate.Text = _InternaionalLicense.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _InternaionalLicense.ExpirationDate.ToShortDateString();

            _LoadImage();



        }

    }
}
