using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt
{
    /// <summary>
    /// Class containg all methods for operations regarding ConnectionString - required for connection to DataBase server.
    /// </summary>
    /// <remarks>
    /// The class can save ConnectionString to file, load and check if the connection to DataBase server is available.
    /// </remarks>
    static class ConnectionStringController

    {
        /// <summary>
        /// Field required for using "log4net" logger.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The name of the config file containing ConnectionString.
        /// </summary>
        private static string fileNameConnectionString = "cs_data.cfg";

        /// <summary>
        /// The path on to the file containing ConnectionString.
        /// </summary>
        private static string filePathConnectionString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileNameConnectionString);

        /// <summary>
        /// Saves ConnectionString to config file in the application folder.
        /// </summary>
        /// <param name="conn">String with ConnectionString</param>
        public static void SaveConnectionStringToFile(string conn)
        {
            log.Info("SaveConnectionStringToFile() invoked.");

            try
            {      
                File.WriteAllText(filePathConnectionString, conn);
                log.Info("ConnectionString saved.");
            }

            catch (Exception ex)
            {
                log.Error("Saving ConnectionString to file failed.");
                log.Error("ConnectionString parameter = " + conn);
                log.Error("Destination path = " + filePathConnectionString);
                log.Error(ex);
               
                MessageBox.Show("Błąd. Nie udało się utworzyć pliku konfiguracyjnego. Upewnij się, czy posiadasz uprawnienia do zapisywania danych na dysku, na którym znajduje się aplikacja. W przypadku dalszych problemów skontaktuj się z producentem.");
            }        
        }

        /// <summary>
        /// Loads ConnectionString from config file.
        /// </summary>
        /// <returns>Returns string with ConnectionString</returns>
        public static string GetConnectionStringFromFile()
        {
            log.Info("GetConnectionStringFromFile() invoked.");

            string connString = "";

            try
            {
                connString = File.ReadAllText(filePathConnectionString);
                log.Info("ConnectionString loaded from file.");
                return connString;
            }

            catch (Exception ex)
            {
                log.Error("Couldn't load ConnectionString from file.");
                log.Error("Source path = " + filePathConnectionString);
                log.Error("Supposed ConnectionString = " + connString);
                log.Error(ex);
                return null;
            }          
        }
        
        /// <summary>
        /// Checks if connection to DataBase server is available.
        /// </summary>
        /// <returns>Returns bool which tells if the connection to DataBase is available.</returns>
        public static bool IsConnectionAvailable()
        {
            log.Info("IsConnectionAvailable() invoked.");

            SqlConnection connection = new SqlConnection(GetConnectionStringFromFile());

            try
            {
                connection.Open();
                connection.Close();

                log.Info("Connection obtained.");
            }

            catch (Exception ex)
            {
                log.Error("Connection not obtained");
                log.Error("ConnectionString in file = " + GetConnectionStringFromFile());
                log.Error(ex);
         

                return false;
            }        

            return true;
        }

    }
}
