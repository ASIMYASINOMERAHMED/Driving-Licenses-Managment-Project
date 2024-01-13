using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussiness1
{
    public class clsPerson
    {
    
            public string FirstName { set; get; }
            public string SecondName { set; get; }
            public string ThirdName { set; get; }  
            public string LastName { set; get; }
            public string Phone { set; get; }
            public string Email { set; get; }
         
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        enum enMode { UpdateMode = 1, AddNewMode = 2 };
        enMode Mode;
        public string NationalityNo;
        public string Address;
        public int PersonID, NationalityCountryID;
        public short Gendor;
        public string ImagePath;
        public DateTime DateOfBirth;
        public clsCountry CountryInfo;


        public clsPerson()

        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
            this.CountryInfo = new clsCountry();

            Mode = enMode.AddNewMode;
        }

        private clsPerson(int PersonID, string FirstName, string SecondName, string ThirdName,
            string LastName, string NationalNo, DateTime DateOfBirth, short Gendor,
             string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)

        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalityNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            Mode = enMode.UpdateMode;
        }


        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
        public static DataTable GetPeople()
        {
            return clsPersonData.GetPeople();
        }
        public static int GetPeopleCounts()
        {
            return clsPersonData.GetPeopleCount();
        }
        public static DataTable GetPeople(int PersonID)
        {
            return clsPersonData.GetPeople(PersonID);
        }
        public static DataTable GetPeople(string NationalNo)
        {
            return clsPersonData.GetPeople(NationalNo);
        }
        public static clsPerson FindPerson(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", NationalNo = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = 0;
            int NationalityCountryID = 0;

            if (clsPersonData.GetPersonInfoByID(PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref NationalNo, ref DateOfBirth, ref Gendor, ref Address,ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
                return new clsPerson(PersonID, FirstName,SecondName,ThirdName,LastName,NationalNo,DateOfBirth,Gendor,Address,Phone,Email,NationalityCountryID,ImagePath);
            else
                return null;

        }
        public static clsPerson FindPerson(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = 0;
            int NationalityCountryID = 0,PersonID=0;

            if (clsPersonData.GetPersonInfoByNationalNo(NationalNo,ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;

        }
        private bool _AddNewPerson()
        {

            this.PersonID = clsPersonData.AddNewPerson(this.NationalityNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.Email,
                this.Phone, this.Address, this.DateOfBirth, this.Gendor, this.NationalityCountryID, this.ImagePath);
            return (PersonID != -1);
        }
        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PersonID, this.NationalityNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.Gendor, this.NationalityCountryID, this.ImagePath);
        }
        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNewMode:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateMode:

                    return _UpdatePerson();

            }

            return false;
        }
        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static bool isPersonExist(int ID)
        {
            return clsPersonData.IsPersonExist(ID);
        }

        public static bool isPersonExist(string NationlNo)
        {
            return clsPersonData.IsPersonExist(NationlNo);
        }

    }

}
    


