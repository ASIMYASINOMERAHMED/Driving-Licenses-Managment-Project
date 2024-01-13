using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussiness1
{
    public class clsManageApplicationType
    {
        public enum enMode { AddNew=1, Update=2}
        public int _ApplicationID { set; get; }
        public string _ApplicationName {  get; set; }
        public float _Fees { set; get; }

        enMode Mode = enMode.AddNew;
        public clsManageApplicationType(int ApplicationID,string ApplicationName,float Fees) 
        {
            _ApplicationID = ApplicationID;
            _ApplicationName = ApplicationName;
            _Fees = Fees;
            Mode = enMode.Update;
        }
        public clsManageApplicationType()
        {
            Mode = enMode.AddNew;
            _ApplicationID = -1;
            _Fees = 0;
            _ApplicationName = "";
        }

        public static DataTable GetApplicationTypes()
        {
            return clsApplicationType.GetApplicationTypes();
        }
        public static int GetApplicationsCount()
        {
            return DVLDDataAccess.clsApplicationType.GetApplicationsCount();
        }
        private bool _AddNewApplicationType()
        {
            this._ApplicationID = DVLDDataAccess.clsApplicationType.AddNewApplicationType(this._ApplicationName,this._Fees);
            return (this._ApplicationID !=-1);
        }
        private bool _UpdateApplicationType()
        {
            return DVLDDataAccess.clsApplicationType.UpdateApplicationType(this._ApplicationID,this._ApplicationName,this._Fees);
        }
        public static clsManageApplicationType Find(int ApplicationID)
        {
            string ApplicationName = "";
            float Fees = 0;
            if (DVLDDataAccess.clsApplicationType.FindApplicationType(ApplicationID,ref ApplicationName, ref Fees))
                return new clsManageApplicationType(ApplicationID, ApplicationName, Fees);
            else
                return null;
        }

        public bool Save()
        {
            switch(Mode)
            {
               case enMode.AddNew:
                    if(_AddNewApplicationType())
                    {
                        Mode= enMode.Update;
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
