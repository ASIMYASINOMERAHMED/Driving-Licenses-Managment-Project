using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussiness1
{
    public class clsApplicationType
    {
        enum enMode { AddNew=0,Update=1}
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { set; get; }
        enMode Mode { get; set; }
        public clsApplicationType(int applicationTypeID, string applicationTypeTitle, float applicationFees)
        {
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationFees = applicationFees;
            Mode = enMode.Update;
        }
        public clsApplicationType()
        {
            ApplicationFees = -1;
            ApplicationTypeID = -1;
            ApplicationTypeTitle = "";
            Mode = enMode.AddNew;
        }
        private  bool _AddNewApplicationType()
        {
            this.ApplicationTypeID = DVLDDataAccess.clsApplicationType.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);
            return (this.ApplicationTypeID != -1);
        }
        private bool _UpdateApplicationType()
        {
            return DVLDDataAccess.clsApplicationType.UpdateApplicationType(this.ApplicationTypeID,this.ApplicationTypeTitle,this.ApplicationFees);
        }
        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string ApplicationTitle = "";
            float ApplicationFees = 0;
            if(DVLDDataAccess.clsApplicationType.FindApplicationType(ApplicationTypeID,ref ApplicationTitle,ref ApplicationFees))
                return new clsApplicationType(ApplicationTypeID,ApplicationTitle,ApplicationFees);
            else
                return null;
        }
        public static DataTable GetApplicationTypes()
        {
            return DVLDDataAccess.clsApplicationType.GetApplicationTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplicationType();
            }
            return false;
        }

    }
}
