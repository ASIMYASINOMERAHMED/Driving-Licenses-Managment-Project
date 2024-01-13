using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussiness1
{
    public class clsUser
    {
            enum enMode { UpdateMode = 1, AddNewMode = 2 };
            enMode Mode;
            public string UserName { set; get; }
            public string Password { set; get; }
            public bool IsActive { get; set; }
            public int UserID { get; set; }
            public int PersonID { get; set; }
            public clsPerson PersonInfo = new clsPerson();
            public clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
            {
                this.UserID = UserID;
                this.PersonID = PersonID;
                this.PersonInfo = clsPerson.FindPerson(PersonID);
                this.IsActive = IsActive;
                this.UserName = UserName;
                this.Password = Password;
                Mode = enMode.UpdateMode;
            }
            public clsUser()
            {
                PersonInfo.PersonID = -1;
                this.IsActive = true;
                this.UserName = "";
                this.Password = "";
                Mode = enMode.AddNewMode;
            }

            public static DataTable GetAllUsers()
            {
                return clsUserData.GetAllUsers();
            }
            public static int GetUsersCount()
            {
                return clsUserData.GetUsersCount();
            }
            public static clsUser FindByUserNameAndPassword(string UserName, string Password)
            {
                int PersonID = -1;
                bool IsActive = false;
                int UserID = -1; 
                if (clsUserData.FindUserByUserNamePassword(UserName, Password,ref UserID, ref PersonID, ref IsActive))
                    return new clsUser(UserID,PersonID, UserName, Password, IsActive);
                else
                    return null;

            }

       
            public static clsUser FindByUserID(int UserID)
            {

                int PersonID = -1;
                bool IsActive = false;
                string UserName="", Password="";

                bool IsFound = clsUserData.GetUserByUserID(UserID, ref UserName, ref Password, ref PersonID, ref IsActive);
                if(IsFound)
                    return new clsUser(UserID,PersonID, UserName, Password, IsActive);
                else
                    return null;

            }
        public static clsUser FindByPersonID(int PersonID)
        {

            int UserID = -1;
            bool IsActive = false;
            string UserName = "", Password = "";

            bool IsFound = clsUserData.GetUserInfoByPersonID(PersonID, ref UserName, ref Password, ref UserID, ref IsActive);
            if (IsFound)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID,this.UserName,this.Password,this.IsActive,this.PersonID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNewMode:
                    if (_AddNewUser())
                    {

                        Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateMode:

                    return _UpdateUser();

            }

            return false;
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }
        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }
    }
}
