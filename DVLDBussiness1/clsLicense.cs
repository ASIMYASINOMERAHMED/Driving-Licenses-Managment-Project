using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DVLDDataAccess;
using static System.Net.Mime.MediaTypeNames;
using static DVLDBussiness1.clsLicense;

namespace DVLDBussiness1
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime =1,Renew=2,DamagedReplacement=3, LostReplacement=4};
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int LicenseClass { set; get; }
        public clsLicenseClass LicenseClassIfo;
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }
        public bool IsActive { set; get; }
        public enIssueReason IssueReason { set; get; }
        public clsDetainLicense DetainInfo { set; get; }
        public string IssueReasonText
        {
            get { return GetIssueReasonText(this.IssueReason); }
        }
        public clsDriver DriverInfo;
        public bool IsDetained
        {
            get {return clsDetainedLicense.IsLicenseDetained(this.LicenseID);  }
        }
        public int CreatedByUserID { set; get; }
        public clsLicense()

        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        public clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)

        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.LicenseClassIfo = clsLicenseClass.Find(this.LicenseClass);
            this.DetainInfo = clsDetainLicense.FindByLicenseID(this.LicenseID);

            Mode = enMode.Update;

        }
        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes,
                this.PaidFees, this.IsActive,(byte)this.IssueReason, this.CreatedByUserID);
            return this.LicenseID != -1;
        }
        private bool _UpdateLicense()
        {
            //call DataAccess Layer 

            return clsLicenseData.UpdateLicense(this.ApplicationID, this.LicenseID, this.DriverID, this.LicenseClass,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;
            if (clsLicenseData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }
        public static clsLicense FindByDriverID(int DriverID)
        {
            int ApplicationID = -1; int LicenseID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;
            if (clsLicenseData.GetLicenseInfoByDriverID(DriverID, ref LicenseID, ref ApplicationID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicense();

            }

            return false;
        }
        public static string GetIssueReasonText(enIssueReason issueReason)
        {
            switch (issueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
          
                case enIssueReason.Renew:
                    return "Renew";
              
                case enIssueReason.DamagedReplacement:
                    return "Replacement For Damaged";
                    
                case enIssueReason.LostReplacement:
                    return "Replacement For Lost";          
                default:
                    return "First Time";
            }
        }

        public static bool IsLicenseExistByPersonID(int PersonID,int LicenseClassID)
        {
            return (clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }
        public Boolean IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }
        public bool DeactivateCurrentLicense()
        {
            return (clsLicenseData.DeactivateLicense(this.LicenseID));
        }
        public clsLicense ReNewLicense(string Notes,int CreatedByUserID)
        {
            clsApplication application = new clsApplication();
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            application.ApplicationDate = DateTime.Now;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsManageApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense)._Fees;
            application.CreatedByUserID = CreatedByUserID;
            application.ApplicantPersonID = this.DriverInfo.PersonID;
            if(!application.Save())
            {
                return null;
            }
            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;

            int DefualtValidityLength = this.LicenseClassIfo.DefaultValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefualtValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassIfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if(!NewLicense.Save())
            {
                return null;
            }
            DeactivateCurrentLicense();
            return NewLicense;

        }
        public clsLicense Replace(enIssueReason Reason,int CreatedByUserID)
        {
            clsApplication application = new clsApplication();
            application.ApplicantPersonID = this.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;

            application.ApplicationTypeID = IssueReason == enIssueReason.DamagedReplacement ?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsApplicationType.Find(application.ApplicationTypeID).ApplicationFees;
            application.CreatedByUserID = CreatedByUserID;
            if(!application.Save())
            {
                return null;
            }
            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;



            if (!NewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLicense();
            return NewLicense;

        }
        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainLicense detainedLicense = new clsDetainLicense();
            detainedLicense.LicenseID = this.LicenseID;
            detainedLicense.FineFees = Convert.ToSingle(FineFees);
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.CreatedByUserID = CreatedByUserID;
            if(!detainedLicense.Save())
            {
                return -1;
            }
            return detainedLicense.DetainID;
          
        }
        public bool ReleaseDetainLicense(int ReleasedByUserID,ref int ApplicationID)
        {
            clsApplication application = new clsApplication();
            application.ApplicantPersonID = this.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            application.CreatedByUserID = ReleasedByUserID;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            if(!application.Save())
            {
                ApplicationID = -1;
                return false;
            }
            ApplicationID = application.ApplicationID;
            return this.DetainInfo.ReleaseDetainedLicense(ReleasedByUserID, application.ApplicationID);
        }

    }

}
