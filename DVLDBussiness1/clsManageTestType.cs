using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussiness1
{
    public class clsManageTestType
    {
        public enum enMode { AddNew = 1, Update = 2 }
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public enTestType _TestTypeID { set; get; }
        public string _TestTitle { get; set; }
        public string _TestDescription { get; set; }
        public decimal _Fees { set; get; }

        enMode Mode = enMode.AddNew;
        public clsManageTestType(enTestType TestID,string TestTitle,string TestDescription,decimal Fees) 
        {
            _TestTypeID = TestID;
            _TestTitle = TestTitle;
            _TestDescription = TestDescription;
            _Fees = Fees;
            Mode = enMode.Update;
        
        }
        public clsManageTestType()
        {
            _TestTypeID = enTestType.VisionTest;
            _TestTitle = "";
            _TestDescription = "";
            _Fees = 0;
            Mode = enMode.AddNew;
        }
        public static DataTable GetTestTypes()
        {
            return clsTestTypes.GetTestTypes();
        }
        public static int GetTestTypesCount()
        {
            return clsTestTypes.GetTestTypesCount();
        }
        private bool _AddNewTestType()
        {
            this._TestTypeID = (enTestType)clsTestTypes.AddNewTestType(this._TestTitle,this._TestDescription, this._Fees);
            return (this._TestTitle != "");
        }
        private bool _UpdateTestType()
        {
            return clsTestTypes.UpdateTestType((int)this._TestTypeID,this._TestTitle, this._TestDescription, this._Fees);
        }
        public static clsManageTestType Find(enTestType TestID)
        {
            string TestTitle = "", TestDescription="";
            decimal Fees = 0;
            if (clsTestTypes.FindTestType((int)TestID, ref TestTitle,ref TestDescription, ref Fees))
                return new clsManageTestType(TestID,TestTitle,TestDescription,Fees);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestType();
            }
            return false;
        }
    }
}

