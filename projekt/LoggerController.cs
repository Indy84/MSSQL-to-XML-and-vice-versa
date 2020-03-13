using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace projekt
{
    /// <summary>
    /// Class containg all helper methods regarding logger.
    /// </summary>
    static class LoggerController
    {
        /// <summary>
        /// Field required for using "log4net" logger.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Writes the content of the DataTable to logs.
        /// </summary>
        /// <param name="table">DataTable to be written to logs.</param>
        public static void WriteDataTableContentToLogs(DataTable table)
        {
            log.Info("WriteDataTableContentToLogs() invoked for " + table);
            log.Info("Content of " + table);

            foreach (DataRow dataRow in table.Rows)
            {
                string record = "";

                foreach (var item in dataRow.ItemArray)
                {
                    record += item + " ";
                }

                log.Info(record);
            }

            log.Info("Finished displaying content from " + table);
        }

        /// <summary>
        /// Writes down the content of the file to logs.
        /// </summary>
        /// <param name="path">Path to the file to be written to logs.</param>
        public static void WriteFileToLogs(string path)
        {
            log.Info("WriteFileToLogs() invoked.");

            try
            {
                const Int32 BufferSize = 128;
                using (FileStream fileStream = File.OpenRead(path))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            log.Info(line);
                        }
                    }
                }
                log.Info("WriteFileToLogs() succeded.");
            }

            catch (Exception ex)
            {
                log.Error("WriteFileToLogs() failed.");
                log.Error("path parameter: " + path);
                log.Error(ex);
            }          
        }

        /// <summary>
        /// Writes down to logs the count of the records in particular table in DataBase on server.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static int CheckCountOfTableOnServer(string tableName)
        {
            log.Info("CheckCountOfTableOnServer() invoked. tableName: " + tableName);

            try
            {
                SqlConnection conn = new SqlConnection(ConnectionStringController.GetConnectionStringFromFile());
                SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM @tableName", conn);
                comm.Parameters.AddWithValue("@tableName", tableName);

                conn.Open();
                Int32 count = (Int32)comm.ExecuteScalar();
                conn.Close();

                log.Info("CheckCountOfTableOnServer() succeded. Number of records: " + count);
                return count;
            }
            catch (Exception ex)
            {
                log.Info("CheckCountOfTableOnServer() failed");
                log.Info(ex);
                return 0;
            }
        }
    }
}