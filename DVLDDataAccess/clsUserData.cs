using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public class clsUserData
    {
        public static int GetUsersCount()
        {
            int Count = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT COUNT(*) FROM Users;";

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
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select UserID,Users.PersonID,(FirstName+' '+SecondName+' '+ThirdName+' '+LastName)as FullName,UserName,IsActive from Users inner join People on People.PersonID=Users.PersonID";

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

        public static bool DeleteUser(int UserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Users 
                                where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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
    
        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

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
        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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
        public static bool IsUserExist(string UserName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

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
        public static bool FindUserByUserNamePassword(string UserName, string Password, ref int UserID, ref int PersonID, ref bool IsActive)
        {

            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString); //ConnectionString2
            string query = "Select * From Users where UserName = @UserName and Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    IsActive = (bool)reader["IsActive"];


                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;


        }
        public static bool GetUserByUserID(int UserID, ref string UserName, ref string Password, ref int PersonID, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;

        }
        public static bool GetUserInfoByPersonID(int PersonID, ref string UserName, ref string Password, ref int UserID, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                if (!EventLog.SourceExists(clsDataAccessSettings.SourceName))
                {
                    EventLog.CreateEventSource(clsDataAccessSettings.SourceName, "Application");
                }
                EventLog.WriteEntry(clsDataAccessSettings.SourceName, $"An Error Occured : {ex.Message}", EventLogEntryType.Error);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;

        }
        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Users (PersonID,UserName,Password,IsActive)
                             VALUES (@PersonID, @UserName,@Password,@IsActive);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }
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

            return UserID;

        }
        public static bool UpdateUser(int UserID, string UserName, string Password, bool IsActive, int PersonID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Users  
                            set PersonID = @PersonID,
                                UserName = @UserName,
                                Password = @Password,
                                IsActive = @IsActive
                                where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);


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
    }
}
