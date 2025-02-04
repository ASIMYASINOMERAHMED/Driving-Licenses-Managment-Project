﻿using DVLDBussiness1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment
{
    public partial class frmTakeTest : Form
    {
        private int _AppointmentID;
        private clsManageTestType.enTestType _TestType;

        private clsTest _Test;
        public frmTakeTest(int AppointmentID,clsManageTestType.enTestType TestType)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _TestType = TestType;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                   "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No )
            {
                return;
            }
            _Test.TestAppointmentID = _AppointmentID;
            _Test.TestResults = rbPass.Checked;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrScheduledTest1.TestTypeID=_TestType;
            ctrScheduledTest1.LoadInfo(_AppointmentID);
            if(ctrScheduledTest1.TestAppointmentID==-1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            int TestID = ctrScheduledTest1.TestID;
            if (TestID != -1)
            {
                _Test = clsTest.Find(TestID);
                if (_Test.TestResults)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                txtNotes.Text = _Test.Notes;

                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
            }
            else
                _Test = new clsTest();
        }
    }
}
