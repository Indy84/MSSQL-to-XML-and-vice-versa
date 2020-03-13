using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace projekt
{    
     /// <summary>
     /// Class containg all methods for operations regarding config file with login data.
     /// </summary>
     /// <remarks>
     /// The class can save login to config file and load from file.
     /// </remarks>
    static class LoginDataController
    {
        /// <summary>
        /// Field required for using "log4net" logger.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Name of the config file containing login data.
        /// </summary>
        private static string fileNameLoginData = "login_data.cfg";

        /// <summary>
        /// Path to the config file containing login data.
        /// </summary>
        private static string filePathLoginData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileNameLoginData);

        /// <summary>
        /// Overwrites the config file in the application folder with login data.
        /// </summary>
        /// <param name="host">The actual text in Host TextBox on the main Form of UI.</param>
        /// <param name="dbName">The actual text in DataBase Name TextBox on the main Form of UI.</param>
        public static void OverwriteFile(string host, string dbName)
        {
            log.Info("OverwriteFile() invoked.");

            try
            {
                string[] inputs = { host, dbName };
                File.WriteAllLines(filePathLoginData, inputs);
                log.Info("OverwriteFile() succeded.");
            }
            catch (Exception ex)
            {
                log.Error("OverwriteFile() failed. host parameter: " + host + " dbName parameter: " + dbName);
                log.Error(ex);
            }
        }

        /// <summary>
        /// On the start of the application the method reads the config file (if exists) and accordingly fills Text Boxes. 
        /// </summary>
        /// <returns>Returns String Array with two strings with Host name and DataBase name.</returns>
        public static string[] ReadFromFile()
        {
            log.Info("ReadFromFile() invoked.");
            string[] loginData = new string[2];

            try
            {
                if (File.Exists(filePathLoginData))
                {
                    loginData[0] = File.ReadLines(filePathLoginData).ElementAt(0);
                    loginData[1] = File.ReadLines(filePathLoginData).ElementAt(1);
                }

                log.Info("ReadFromFile() succeded.");
            }

            catch (Exception ex)
            {
                log.Error("ReadFromFile() failed.");
                log.Error(ex);
            }

           return loginData;       
        }

    }
}