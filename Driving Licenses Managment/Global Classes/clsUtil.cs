using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment
{
    public class clsUtil
    {

        public static bool CreateFolderIfNotExist(string FolderPath)
        {
            if(!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("Error Creating folder: " + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
            
            }
            return true;
        }
        public static string GenarateGuid()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
        public static string ReplaceImageFileNameWithGuid(string sourceFile) 
        {
               string FileName = sourceFile;
               FileInfo FileInfo = new FileInfo(FileName);
            return GenarateGuid() + FileInfo.Extension;
        }
        public static bool CopyImageToProjectFileImages(ref string sourceFileName)
        {
            string destinationFile = @"C:\DVLD-People-Images\";

            if(!CreateFolderIfNotExist(destinationFile))
            {
                return false;
            }
            string destinationFolder = destinationFile + ReplaceImageFileNameWithGuid(sourceFileName);
            try
            {
                File.Copy(sourceFileName, destinationFile,true);
                return true;
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFileName = destinationFile;
            return true;
        }
        public static string ComputeHash(string Password)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));


                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }

}
