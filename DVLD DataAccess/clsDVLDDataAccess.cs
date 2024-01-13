using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Net;
using System.Security;


namespace DVLD_DataAccess
{
    public class clsDVLDDataAccess
    {
       // public static bool GetUserInfoByUserNamePassword(string UserName, string Password, ref string FirstName,
       //ref string LastName, ref string Email, ref string Phone, ref string Gendor,
       //ref DateTime HireDate, ref DateTime ExitDate,
       //ref int DepartmentID, ref string Position, ref int Permission, ref double Salary, ref string Status, ref string ImagePath)
       // {
       //     bool IsFound = false;
       //     SqlConnection connection = new SqlConnection(DVLDDataAccessSettings.ConnectionString); 
       //     string query = "Select * From Employees where UserName = @UserName and Password = @Password";
       //     SqlCommand command = new SqlCommand(query, connection);
       //     command.Parameters.AddWithValue("@UserName", UserName);
       //     command.Parameters.AddWithValue("@Password", Password);
       //     try
       //     {
       //         connection.Open();
       //         SqlDataReader reader = command.ExecuteReader();
       //         if (reader.Read())
       //         {
       //             IsFound = true;
       //             ID = (int)reader["EmployeeID"];      //ID
       //             FirstName = (string)reader["FirstName"];
       //             LastName = (string)reader["LastName"];
       //             Email = (string)reader["Email"];
       //             Phone = (string)reader["Phone"];
       //             Gendor = (string)reader["Gendor"];
       //             Salary = (double)reader["Salary"];   //MonthlySalary
       //                                                  //  Bonus = (double)reader["BonusPerc"];
       //             DepartmentID = (int)reader["DepartmentID"];
       //             //  CountryID = (int)reader["CountryID"];
       //             Permission = (int)reader["Permissions"];
       //             // DateOfBirth = (DateTime)reader["DateOfBirth"];
       //             HireDate = (DateTime)reader["HireDate"];

       //             if (reader["Status"] == DBNull.Value)
       //             {
       //                 Status = "";
       //             }
       //             else
       //                 Status = (string)reader["Status"];
       //             if (reader["Position"] == DBNull.Value)
       //             {
       //                 Position = "";
       //             }
       //             else
       //                 Position = (string)reader["Position"];
       //             if (reader["ExitDate"] == DBNull.Value)
       //             {
       //                 ExitDate = DateTime.Now;
       //             }
       //             else
       //                 ExitDate = (DateTime)reader["ExitDate"];

       //             if (reader["ImagePath"] == DBNull.Value)
       //             {
       //                 ImagePath = "";

       //             }
       //             else
       //                 ImagePath = (string)reader["ImagePath"];

       //         }
       //         else
       //         {
       //             IsFound = false;
       //         }
       //         reader.Close();
       //     }
       //     catch
       //     {
       //         IsFound = false;
       //     }
       //     finally
       //     {
       //         connection.Close();
       //     }
       //     return IsFound;


        }
    }
