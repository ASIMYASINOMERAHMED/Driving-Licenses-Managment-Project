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
    public class clsCountryData
    {
        public static bool GetCountryByID(int CountryID,ref string CountryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    CountryName = (string)reader["CountryName"];

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
        public static bool GetCountryByName(string CountryName,ref int  CountryID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    CountryID = (int)reader["CountryID"];

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
        public static DataTable GetAllCountries()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries order by CountryName";

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
    }
}
