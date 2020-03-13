using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace projekt
{
    /// <summary>
    /// Class containing all methods regarding the main Form of UI.
    /// </summary>
    public partial class Form1 : Form
    {

        /// <summary>
        /// Field required for using "log4net" logger.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// OCP_OD_Relations field required for operations on data.
        /// </summary>  
        private OCP_OD_Relations ocp_OD_Relations = null;
        
        /// <summary>
        /// The constructor which loads from file (only if exists) strings to TextBoxes with Host name and DataBase name according to last used data.
        /// Also creates the instance of OCP_OD_Relations class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            log.Info("Form1 loaded.");

            ocp_OD_Relations = new OCP_OD_Relations();
            textBoxHost.Text = LoginDataController.ReadFromFile()[0];
            textBoxDBName.Text = LoginDataController.ReadFromFile()[1];           

        }

        /// <summary>
        /// Creates ConnectionString according to user's input in Text Boxes on the main Form of UI.
        /// </summary>
        /// <returns>Returns string with ConnectionString.</returns>
        private string GetConnectionStringFromTextBoxes()
        {
            log.Info("GetConnectionStringFromTextBoxes() invoked.");

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = textBoxHost.Text;
            builder.InitialCatalog = textBoxDBName.Text;
            builder.IntegratedSecurity = true;

            log.Info("GetConnectionStringFromTextBoxes() succeded.");
            return builder.ConnectionString;
        }

        /// <summary>
        /// Gets "Id" value from selected record on DataGridView.
        /// </summary>
        /// <param name="dataGridView">DataGridView on main form of UI which the method gets "Id" value from.</param>
        /// <returns>Returns integer with "Id".</returns>
        private int GetSelectedRowId(DataGridView dataGridView)
        {
            log.Info("GetSelectedRowID() invoked for " + dataGridView);

            int rowIndex = 0;

            try
            {
                rowIndex = dataGridView.CurrentCell.RowIndex;
                int id = Convert.ToInt32(dataGridView.Rows[rowIndex].Cells["Id"].Value);

                log.Info("GetSelectedRowID() for " + dataGridView + " succeded. Obtained Id: " + id);
                return id;
            }

            catch (FormatException ex)
            {
            
                log.Error("GetSelectedRowID() for " + dataGridView + " failed. FormatException.");
                log.Error("Id destined to conversion: " + dataGridView.Rows[rowIndex].Cells["Id"].Value.ToString());
                log.Error(ex);

                MessageBox.Show("Niepoprawna struktura bazy danych. Wartość w kolumnie Id nie jest liczbą całkowitą.");
                return 0;
            }

            catch (Exception ex)
            {
                log.Error("GetSelectedRowID() for " + dataGridView + " failed.");
                log.Error(ex);
                return 0;
            }
      
        }

        /// <summary>
        /// Helper method for saving files.
        /// Gets the "Name" value from selected row on DataGridView.
        /// Additionally eliminates empty spaces at the end - in case when DataBase uses CHAR() instead of VARCHAR().
        /// </summary>
        /// <param name="dataGridView">DataGridView which the method gets the "Name" value from.</param>
        /// <returns></returns>
        private string GetSelectedRowName(DataGridView dataGridView)
        {
            log.Info("GetSelectedRowName() invoked.");
            int deletedSpaces = 0;

            try
            {
                int rowIndex = dataGridView.CurrentCell.RowIndex;
                string name = dataGridView.Rows[rowIndex].Cells["Name"].Value.ToString();              

                while (name.EndsWith(" "))
                {
                    name = name.Remove(name.Length - 1);
                    deletedSpaces++;
                }

                log.Info("GetSelectedRowName() invoked.");
                return name;
            }

            catch (Exception ex)
            {
                log.Error("GetSelectedRowName() failed");
                log.Error("Deleted spaces: " + deletedSpaces);
                log.Error(ex);
                return "";
            }
        }

        /// <summary>
        /// Loads data from DataBase server to application and starts displaying records in DataGridViews on the main Form of UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_FromDataBaseToApplication_Click(object sender, EventArgs e)
        {

            log.Info("Button FromDataBaseToApplication clicked");
            log.Info("host: " + textBoxHost.Text + " name: " + textBoxDBName.Text);

            ConnectionStringController.SaveConnectionStringToFile(GetConnectionStringFromTextBoxes());
            LoginDataController.OverwriteFile(textBoxHost.Text, textBoxDBName.Text);

            if (textBoxDBName.Text == "" || textBoxHost.Text == "")
            {
                MessageBox.Show("Wprowadź nazwę hosta oraz nazwę bazy danych z którą chcesz się połączyć.");
            }

            else if (!ConnectionStringController.IsConnectionAvailable())
            {
                MessageBox.Show("Nie udało się połączyć z bazą danych. Upewnij się czy wprowadziłeś poprawnie nazwy hosta oraz bazy danych, lub czy posiadasz uprawnienia do połączenia się z bazą.");
            }

            else
            {
                ocp_OD_Relations.LoadDataToDGV_OConfigPack(DGV_OConfigPack);

                if (GetSelectedRowId(DGV_OConfigPack) != 0)
                {
                    ocp_OD_Relations.LoadDataToDGV_ODesign(DGV_ODesign, GetSelectedRowId(DGV_OConfigPack));
                }
            }
        }

        /// <summary>
        /// Changes records which are displayed in ODesign DataGridView according to selected record in OConfigPack DataGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGV_OConfigPack_SelectionChanged(object sender, EventArgs e)
        {
            log.Info("DGV_OConfigPack_SelectionChanged fired.");

            if (DGV_OConfigPack.Focused && DGV_OConfigPack.CurrentCell != null)
            {
                log.Info("OConfigPack index = " + GetSelectedRowId(DGV_OConfigPack) + ". Loading data do ODesign.");
                ocp_OD_Relations.LoadDataToDGV_ODesign(DGV_ODesign, GetSelectedRowId(DGV_OConfigPack));
            }
        }

        /// <summary>
        /// Creates DataSet with desirable data and saves to XML file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_FromApplicationToXML_Click(object sender, EventArgs e)
        {
            log.Info("Button_FromApplicationToXML clicked.");

            if (ocp_OD_Relations.TableOConfigPack == null)
                MessageBox.Show("Zaimportuj poprawnie bazę danych z serwera.");

            else if (!ConnectionStringController.IsConnectionAvailable())
                MessageBox.Show("Utracono połączenie z bazą danych. Nie można wyeksportować danych.");
            
            else if (ocp_OD_Relations.TableODesign == null || ocp_OD_Relations.TableODesign.Rows.Count == 0 || ocp_OD_Relations.TableOConfigPack.Rows.Count == 0)
                MessageBox.Show("Do zapisania pliku wymagana jest co najmniej jedna para powiązanych rekordów.");

            else
            {
                saveFileDialog_XML = new SaveFileDialog();
                saveFileDialog_XML.FileName = GetSelectedRowName(DGV_OConfigPack);
                saveFileDialog_XML.DefaultExt = "xml";
                saveFileDialog_XML.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

                if (saveFileDialog_XML.ShowDialog() == DialogResult.OK)
                {
                    DataGridViewSelectedRowCollection OD_rows = DGV_ODesign.SelectedRows;
                    int selectedOConfigPack_ID = GetSelectedRowId(DGV_OConfigPack);

                    DataSet dataSet = new DataSet();
                    dataSet = ocp_OD_Relations.CreateDataSetWithFiltredTables(selectedOConfigPack_ID, OD_rows);

                    if (dataSet != null)
                    {
                        XMLController.FromDataSetToXML(saveFileDialog_XML.FileName, dataSet);
                        MessageBox.Show("Zapisano plik");

                        log.Info("Button_FromApplicationToXML_Click() succeded. File saved to " + saveFileDialog_XML.FileName);
                        log.Info("Saved file:");
                        LoggerController.WriteFileToLogs(saveFileDialog_XML.FileName);                       
                    }

                    else
                    {
                        MessageBox.Show("Nie udało się zapisać pliku. Skontaktuj się z producentem.");
                        log.Error("DataSet = null. Failed to save XML.");
                    }
      
                }
            }

        }

        /// <summary>
        /// Loads XML file from hard disk and sends its data to DataBase server.
        /// Joints two methods - XMLController.FromXMLToDataSet and OCP_OD_Relations.SendDataSetToDataBase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_FromXMLToDataBase_Click(object sender, EventArgs e)
        {
            log.Info("Button FromXMLToDataBase clicked");

            ConnectionStringController.SaveConnectionStringToFile(GetConnectionStringFromTextBoxes());
            LoginDataController.OverwriteFile(textBoxHost.Text, textBoxDBName.Text);

            openFileDialog_XML = new OpenFileDialog();
            DialogResult result = openFileDialog_XML.ShowDialog();

            if (result == DialogResult.OK)
            {        
                DataSet dataset = new DataSet();
                dataset = XMLController.FromXMLToDataSet(openFileDialog_XML.FileName);

                if (dataset != null)
                {
                    LoggerController.WriteFileToLogs(openFileDialog_XML.FileName);
                    ocp_OD_Relations.SendDataSetToDataBase(dataset);
                }     
            }
        }

        /// <summary>
        /// Activates and disactivates buttons on the main Form of UI according to user's choice of Radio Button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_FromDBtoXML_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_FromDBtoXML.Checked == true)
            {
                button_FromDataBaseToApplication.Enabled = true;
                button_FromApplicationToXML.Enabled = true;
                button_FromXMLToDataBase.Enabled = false;
            }

            else
            {
                button_FromDataBaseToApplication.Enabled = false;
                button_FromApplicationToXML.Enabled = false;
                button_FromXMLToDataBase.Enabled = true;
            }
        }
       
    }
}
