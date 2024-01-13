using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussiness1
{
    public class clsTest
    {
        public enum enMode { AddNew=0,Update =1};
        public enMode Mode = enMode.AddNew;

        public int TestID { set; get; }
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int CreatedByUserID { set; get; }
        public bool TestResults { get; set; }
        public string Notes { get; set; }   




        public clsTest()
        {
            Notes = string.Empty;
            TestID = -1;
            TestAppointmentID = -1;
            TestTypeID = -1; 
            CreatedByUserID = -1;
            TestResults = false;
            
            Mode = enMode.AddNew;
        }
        public clsTest(int TestID,int TestAppointmentID,int CreatedByUserID,bool TestResults,string Notes)
        {
            this.Notes = Notes;
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResults = TestResults;
            this.CreatedByUserID = CreatedByUserID;
    

            Mode = enMode.Update;
        }

   
        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID, this.TestResults, this.Notes, this.CreatedByUserID);
            return this.TestID != -1;
        }

        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResults,this.Notes, this.CreatedByUserID);
        }
        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;
            if (clsTestData.GetTestByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
                return new clsTest(TestID, TestAppointmentID, CreatedByUserID, TestResult, Notes);
            else
                return null;
            
        }
        public static clsTest FindLastTestPerPersonAndLicenseClass(int PersonID,int LicenseClassID,clsManageTestType.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if(clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID,LicenseClassID,(int) TestTypeID,ref TestID,ref TestAppointmentID,
                ref TestResult,ref Notes,ref CreatedByUserID))
                return new clsTest(TestID, TestAppointmentID, CreatedByUserID,TestResult, Notes);
            else
                return null;
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTest();
            }
            return false;
        }
    }
}
