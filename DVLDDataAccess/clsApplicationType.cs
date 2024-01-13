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
    public class clsApplicationType
    {
        public static bool FindApplicationType(int ApplicationID, ref string ApplicationName, ref float Fees)
        {
            {
                bool IsFound = false;
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                string query = "select * from ApplicationTypes where ApplicationTypeID = @ApplicationID;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        IsFound = true;
                        ApplicationName = (string)reader["ApplicationTypeTitle"];
                        Fees = Convert.ToSingle(reader["ApplicationFees"]);
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
        }
        public static int AddNewApplicationType(string ApplicationName, float Fees)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into ApplicationTypes (ApplicationTypeTitle,ApplicationFees)
                            values(@ApplicationName,@Fees);
                             Select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationName", ApplicationName);
            command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    ApplicationID = InsertedID;
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
            return ApplicationID;
        }
        public static bool UpdateApplicationType(int ApplicationID, string ApplicationName, float Fees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE [dbo].[ApplicationTypes]
                               SET [ApplicationTypeTitle] = @ApplicationName
                                  ,[ApplicationFees] = @Fees
                             WHERE ApplicationTypeID = @ApplicationID;";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@ApplicationName", ApplicationName);
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
        public static DataTable GetApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from ApplicationTypes;";
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
        public static int GetApplicationsCount()
        {
            int Count = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT COUNT(*) FROM ApplicationTypes;";

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
   
    }
}
