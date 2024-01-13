using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBussiness1
{
    public class clsApplication
    {
        public enum enMode {AddNew=0,Update=1};
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enMode Mode = enMode.AddNew;
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
        public int ApplicationID { set; get; }
        public int ApplicantPersonID { set; get; }
        public clsPerson PersonInfo { set; get; }
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.FindPerson(ApplicantPersonID).FullName;
            }
        }
        public DateTime ApplicationDate { set; get; }
        public int ApplicationTypeID { set; get; }
        public enApplicationStatus ApplicationStatus { set; get; }
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus) 
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    default:
                        return "unknown";

                }
            }
        }
        public DateTime LastStatusDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public clsApplicationType ApplicationTypeInfo { set; get; }
        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.ApplicationDate = DateTime.Now;
            this.CreatedByUserID = -1;
            this.ApplicationTypeID = -1;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;

            Mode = enMode.AddNew;
        }
        public clsApplication(int ApplicationID, int ApplicantPersonID,enApplicationStatus ApplicationStatus,DateTime ApplicationDate,int CreatedByUserID,int ApplicationTypeID,DateTime LastPaidFees,float PaidFees)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = clsPerson.FindPerson(ApplicantPersonID);
            this.ApplicationStatus = ApplicationStatus;
            this.ApplicationDate = ApplicationDate;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.ApplicationTypeID =ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(this.ApplicationTypeID);
            this.LastStatusDate = LastPaidFees;
            this.PaidFees = PaidFees;

            Mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = DVLDDataAccess.clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,(byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        
            return this.ApplicationID!=-1;
        }
        private bool _UpdateApplication()
        {
            return DVLDDataAccess.clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID
                , (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }
        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0; int CreatedByUserID = -1;

            bool IsFound = DVLDDataAccess.clsApplicationData.GetApplicationInfoByID
                                (
                                    ApplicationID, ref ApplicantPersonID,
                                    ref ApplicationDate, ref ApplicationTypeID,
                                    ref ApplicationStatus, ref LastStatusDate,
                                    ref PaidFees, ref CreatedByUserID
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsApplication(ApplicationID,ApplicantPersonID,(enApplicationStatus)ApplicationStatus,ApplicationDate
                    ,CreatedByUserID,ApplicationTypeID,LastStatusDate,PaidFees);
            else
                return null;
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID,enApplicationType ApplicationType,int LicenseClassID)
        {
            return DVLDDataAccess.clsLocalDrivingLicenseApplicationData.GetActiveApplicationIDForLicenseClass(PersonID,(int)ApplicationType,LicenseClassID);
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 2);
        }
        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 3);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }
        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }
    }
}
