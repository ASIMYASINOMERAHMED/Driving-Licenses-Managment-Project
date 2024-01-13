using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussiness1
{
    public class clsLicenseClass
    {
        public enum enMode { AddNew =0,Update=1}
        public enMode Mode = enMode.AddNew;
        public int LicenseClassID { set; get; }
        public string ClassName { set; get; }
        public string ClassDescription { set; get; }
        public byte MinimumAge { set; get; }
        public byte DefaultValidityLength { set; get; }
        public float ClassFees { set; get; }  

        public clsLicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;

            Mode = enMode.AddNew;
        }
        public clsLicenseClass(int LicenseClassID, string ClassName,
            string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            this.MinimumAge = MinimumAllowedAge;

            Mode = enMode.Update; 
        }

        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = DVLDDataAccess.clsLicenseClass.AddNewLicenseClass(this.ClassName,this.ClassDescription,this.MinimumAge
                ,this.DefaultValidityLength,this.ClassFees);
            return (this.LicenseClassID != -1);
        }
        private bool _UpdateLicenseClass()
        {
            return DVLDDataAccess.clsLicenseClass.UpdateLicenseClass(this.LicenseClassID, this.ClassName, this.ClassDescription
                ,this.MinimumAge, this.DefaultValidityLength, this.ClassFees);
        }
        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            byte MinimumAge = 18, DefaultValidityLength = 10;
            float ClassFees = 0;
            if (DVLDDataAccess.clsLicenseClass.GetLicenseClassByClassID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAge, DefaultValidityLength, ClassFees);
            else 
                return null;
        }
        public static clsLicenseClass Find(string LicenseClassName)
        {
            int ClassID = 0;
            string ClassDescription = "";
            byte MinimumAge = 18, DefaultValidityLength = 10;
            float ClassFees = 0;
            if (DVLDDataAccess.clsLicenseClass.GetLicenseClassByClassName(LicenseClassName, ref ClassID, ref ClassDescription, ref MinimumAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(ClassID, LicenseClassName, ClassDescription, MinimumAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }
        public static DataTable GetAllLicenseClasses()
        {
            return DVLDDataAccess.clsLicenseClass.GetAllLicenseClasses();
        }
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewLicenseClass())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicenseClass();
            }
            return false;
        }
    }
}
