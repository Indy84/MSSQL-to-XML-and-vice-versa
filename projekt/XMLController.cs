using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace projekt
{
    /// <summary>
    /// Class containg all methods for operations regarding XML file.
    /// </summary>
    /// <remarks>
    /// The class can save data from DataSet to XML file and read data from XML file to DataSet.
    /// </remarks>
    static class XMLController
    {

        /// <summary>
        /// Field required for using "log4net" logger.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Saves data from DataSet to XML file.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="dataSet">DataSet containg data ready to save.</param>
        public static void FromDataSetToXML(string path, DataSet dataSet)
        {
            log.Info("FromDataSetToXML() invoked.");

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataSet));

                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    xmlSerializer.Serialize(fileStream, dataSet);
                }

                log.Info("FromDataSetToXML() succeded. File saved to: " + path);
            }

            catch (Exception ex)
            {
                log.Error("FromDataSetToXML() failed. path parameter: " + path);
                log.Error(ex);
                MessageBox.Show("Nie udało się utworzyć pliku. Upewnij się czy posiadasz uprawnienia do zapisywania plików w wybranym katalogu. W przypadku dalszych problemów skontaktuj się z producentem.");
            }

          
        }

        /// <summary>
        /// Loads XML file from hard disk and copies data to DataSet.
        /// </summary>
        /// <param name="path">Path to the XML file.</param>
        /// <returns>Returns DataSet filled with data from XMl file.</returns>
        public static DataSet FromXMLToDataSet(string path)
        {
            log.Info("FromXMLToDataSet() invoked. Path parameter: " + path);

            try
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(path);
                dataSet.ReadXml(path);

                log.Info("FromXMLToDataSet() succeded.");
                return dataSet;
            }
            catch (Exception ex)
            {
                log.Error("FromDataSetToXML() failed. path parameter: " + path);
                log.Error(ex);

                MessageBox.Show("Nie udało się wczytać danych z wybranego pliku. Upewnij się czy plik posiada poprawny format. W przypadku dalszych problemów skontaktuj się z producentem.");
                return null;
            }        
        }

    }
}
