using DVLD_DataAccess;
using System.Data;

namespace DVLD_Bussiness
{
    public class clsDVLDBussiness
    {
        public class clsUser : clsPerson
        { 
        enum enMode { EmptyMode = 0, UpdateMode = 1, AddNewMode = 2 };
        enMode Mode;
        string _UserName;
        string _Password;
        int _Permissions, _CountryID, _DepartmentID;
        string _Gendor;
        string _ImagePath, _ExitDate;
        double _Salary, _Bonus;
        DateTime _DateOfBirth, _HireDate;
        private int ID;
        public clsUser(string UserName, string Password, string FirstName, string LastName,
        string Email, string Phone, string Gendor, string ImagePath, int CountryID, int DepartmentID, double Salary, double Bonus,
        DateTime DateOfBirth, DateTime HireDate, string ExitDate,
        int Permissions) : base(FirstName, LastName, Email, Phone)
        {

            _UserName = UserName;
            _Password = Password;
            _Permissions = Permissions;
            _CountryID = CountryID;
            _DepartmentID = DepartmentID;
            _Salary = Salary;
            _Bonus = Bonus;
            _DateOfBirth = DateOfBirth;
            _HireDate = HireDate;
            _ExitDate = ExitDate;
            _Gendor = Gendor;
            _ImagePath = ImagePath;


        }
        //public static clsDVLDBussiness Find(string UserName, string Password)
        //{
            //string FirstName = "", LastName = "", Email = "", Phone = "", Gendor = "", ImagePath = "", ExitDate = "";
            //int Permissions = -1, CountryID = 0, DepartmentID = 0;
            //double Salary = 0, Bonus = 0;


            //DateTime DateOfBirth = DateTime.Now, HireDate = DateTime.Now;
            //if (clsDVLDDataAccess.GetUserInfoByUserNamePassword(UserName, Password, ref FirstName, ref LastName
            //    , ref Email, ref Phone, ref Gendor, ref DateOfBirth, ref HireDate, ref ExitDate, ref CountryID, ref DepartmentID, ref Permissions, ref Salary, ref Bonus, ref ImagePath))
            //    return new clsDVLDBussiness(UserName, Password, FirstName, LastName, Email, Phone, Gendor, ImagePath, CountryID, DepartmentID, Salary, Bonus, DateOfBirth, HireDate, ExitDate, Permissions);
            //else
            //    return null;
       // }

        //private bool _AddNewUser()
        //{

        //}
        //private bool _UpateUser()
        //{

        //}
        //public static bool DeleteUser(string UserName)
        //{

        //}
        //public static DataTable GetAllUsers()
        //{

        //}
        //public bool Save()
        //{


        //    switch (Mode)
        //    {
        //        case enMode.AddNewMode:
        //            if (_AddNewUser())
        //            {

        //                Mode = enMode.UpdateMode;
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }

        //        case enMode.UpdateMode:

        //            return _UpateUser();

        //    }

        //    return false;
        //}
    }
    }
}
    
