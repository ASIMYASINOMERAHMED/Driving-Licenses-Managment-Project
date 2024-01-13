using Driving_Licenses_Managment.Properties;
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
    public partial class frmTestAppointment : Form
    {
        private DataTable _dtLicenseTestAppointments;
        private int _LocalDrivingLicenseApplicationID;
        private clsManageTestType.enTestType _TestType = clsManageTestType.enTestType.VisionTest;

        public frmTestAppointment(int LocalDrivingLicenseApplicationID,clsManageTestType.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch(_TestType)
            {
                case clsManageTestType.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointment";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }
                case clsManageTestType.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointment";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsManageTestType.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointment";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            ctrDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID,_TestType);
            dgvLicenseTestAppointments.DataSource = _dtLicenseTestAppointments;
            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }
        
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingApplicationByID(_LocalDrivingLicenseApplicationID);
            if(localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType) )
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestType);
            if(LastTest == null)
            {
                frmScheduleTest scheduleTest = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                scheduleTest.StartPosition = FormStartPosition.CenterScreen;
                scheduleTest.ShowDialog();
                frmTestAppointment_Load(null, null);
                return;
            }
            if(LastTest.TestResults == false)
            {
                frmScheduleTest scheduleTest = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                scheduleTest.StartPosition = FormStartPosition.CenterScreen;
                scheduleTest.ShowDialog();
                frmTestAppointment_Load(null, null);
                return;
            }
            if(LastTest.TestResults == true)
            {
                MessageBox.Show("person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
       
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest((int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value, _TestType);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            frmTestAppointment_Load(null, null);

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType, (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            frmTestAppointment_Load(null, null);

        }

    }
}
