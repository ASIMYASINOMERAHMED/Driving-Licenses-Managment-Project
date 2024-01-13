using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DVLDDataAccess
{
    public class clsPersonData
    {
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor = ( CASE" +
                "                                                                                                 WHEN Gendor = '0' THEN 'Male'" +
                "                                                                                                 WHEN Gendor = '1' THEN 'Female'" +
                "                                                                                                 ELSE 'Not Provided' END),Address,CountryName as Nationality,Phone,Email from People inner join Countries on Countries.CountryID = People.NationalityCountryID  ORDER BY People.FirstName;";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static DataTable GetPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor,Address,CountryName as Nationality,Phone,Email from People inner join Countries on Countries.CountryID = People.NationalityCountryID;";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetPeople(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor = ( CASE" +
                "                                                                                                 WHEN Gendor = '0' THEN 'Male'" +
                "                                                                                                 WHEN Gendor = '1' THEN 'Female'" +
                "                                                                                                 ELSE 'Not Provided' END),Address,CountryName as Nationality,Phone,Email from People inner join Countries on Countries.CountryID = People.NationalityCountryID where PersonID = @PersonID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static DataTable GetPeople(string NationalNo)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor = ( CASE" +
                "                                                                                                 WHEN Gendor = '0' THEN 'Male'" +
                "                                                                                                 WHEN Gendor = '1' THEN 'Female'" +
                "                                                                                                 ELSE 'Not Provided' END),Address,CountryName as Nationality,Phone,Email from People inner join Countries on Countries.CountryID = People.NationalityCountryID where NationalNo = @NationalNo;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public static int GetPeopleCount()
        {
            int Count = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT COUNT(*) FROM People;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                Count = (int)command.ExecuteScalar();

            }

            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }

            return Count;
        }


        public static bool GetPersonInfoByID(int PersonID, ref string FirstName, ref string SecondName,
           ref string ThirdName, ref string LastName, ref string NationalNo, ref DateTime DateOfBirth,
            ref short Gendor, ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    //ThirdName: allows null in database so we should handle null
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }

                    LastName = (string)reader["LastName"];
                    NationalNo = (string)reader["NationalNo"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];


                    //Email: allows null in database so we should handle null
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
       ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
        ref short Gendor, ref string Address, ref string Phone, ref string Email,
        ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    //ThirdName: allows null in database so we should handle null
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    //Email: allows null in database so we should handle null
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, short Gendor, int CountryID, string ImagePath)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into People (NationalNo,FirstName,SecondName,ThirdName,LastName,Email,Phone,Gendor,Address,DateOfBirth,NationalityCountryID,ImagePath)
                            values(@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@Email,@Phone,@Gendor,@Address,@DateOfBirth,@CountryID,@ImagePath);
                             Select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    PersonID = InsertedID;
                }

            }
            catch(Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return PersonID;
        }


        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, short Gendor, int CountryID, string ImagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update  People  
                            set FirstName = @FirstName,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName, 
                                NationalNo = @NationalNo,
                                DateOfBirth = @DateOfBirth,
                                Gendor=@Gendor,
                                Address = @Address,  
                                Phone = @Phone,
                                Email = @Email, 
                                NationalityCountryID = @NationalityCountryID,
                                ImagePath =@ImagePath
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


        public static bool DeletePerson(int PersonID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete People 
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }
    }
}
