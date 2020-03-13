using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace projekt
{

    /// <summary>
    /// Class performing operations on OConfigPack, ODesign and RConfigPackDesign tables.
    /// </summary>
    class OCP_OD_Relations
    {
        /// <summary>
        /// Field required for using "log4net" logger.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        const string OConfigPack = "OConfigPack";
        const string ODesign = "ODesign";
        const string RConfigPackDesign = "RConfigPackDesign";

        /// <summary>
        /// String containing the names of tables which the class operates on.
        /// </summary>
        string[] tablesNames = { OConfigPack, ODesign, RConfigPackDesign };


        /// <summary>
        /// Field required for operating on data from OConfigPack table.
        /// </summary>
        private DataTable tableOConfigPack = null;

        /// <summary>
        /// Property required for operating by other classes on data from OConfigPack table.
        /// </summary>
        public DataTable TableOConfigPack
        {
            get
            { return tableOConfigPack; }
        }

        /// <summary>
        /// Field required for operating on data from ODesign table.
        /// </summary>
        private DataTable tableODesign = null;

        /// <summary>
        /// Property required for operating by other classes on data from ODesign table.
        /// </summary>
        public DataTable TableODesign
        {
            get
            { return tableODesign; }
        }

     
        /// <summary>
        /// Creates DataTable with the data downloaded from the DataBase server and names it according to the tableName parameter.
        /// </summary>
        /// <param name="command">SqlCommand with a query for obtaining data from the server</param>
        /// <param name="tableName">The name of the table to create.</param>
        /// <returns>Returns DataTable with requested data.</returns>

        private DataTable CreateTable(SqlCommand command, string tableName )
        {
            log.Info("CreateTable() invoked. Table name: " + tableName);

            try
            {                
                DataTable table = new DataTable(tableName);
                SqlConnection connection = new SqlConnection(ConnectionStringController.GetConnectionStringFromFile());

                command.Connection = connection;

                connection.Open(); 
                table.Load(command.ExecuteReader());
                connection.Close();

                log.Info("Table " + tableName + " successfully created.");
                return table;
            }

            catch (Exception ex)
            {
                log.Error("Failed to create table: " + tableName);
                log.Error("SQLCommand: " + command.CommandText); 
                log.Error(ex);

                return null;
            }

        }

        /// <summary>
        /// Downloads all records from DataBase server from the OConfigPack table, loads them to DataTable and binds it to proper DataGridView on UI.
        /// </summary>
        /// <param name="dataGridView">Target DataGridView which the method loads data to.</param>
        public void LoadDataToDGV_OConfigPack(DataGridView dataGridView)
        {
            log.Info("LoadDataToDGV_OConfigPack() invoked.");

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, Description FROM OConfigPack");

                tableOConfigPack = CreateTable(cmd, OConfigPack);
                dataGridView.DataSource = tableOConfigPack;

                log.Info("Successfully loaded data to DGV_OConfigPack.");
                LoggerController.WriteDataTableContentToLogs(tableOConfigPack);
               
            }

            catch (Exception ex)
            {
                log.Error("LoadDataToDGV_OConfigPack() failed.");
                log.Error(ex);
                    
                MessageBox.Show("Nie udało się załadować danych z serwera. Upewnij się czy tabela OConfigPack na serwerze posiada odpowiednią strukturę. W przypadku dalszych problemów skontaktuj się z producentem.");
            }


        }

        /// <summary>
        /// According to selected OConfigPack on UI the method downloads from DataBase server filtred records from ODesign table, loads them to DataTable and binds it to proper DataGridView on UI.
        /// </summary>
        /// <param name="dataGridView">Target DataGridView which the method loads data to.</param>
        /// <param name="selected_OCP_ID">Integer with "Id" value of selected OConfigPack on UI.</param>
        public void LoadDataToDGV_ODesign(DataGridView dataGridView, int selected_OCP_ID)
        {
            log.Info("LoadDataToDGV_ODesign invoked.");

            try
            {

                SqlCommand cmd = new SqlCommand(@"SELECT ODesign.Id, ODesign.Name, ODesign.Description, ODesign.Content
                                    FROM RConfigPackDesign
                                        INNER JOIN OConfigPack
                                            ON RConfigPackDesign.R1ConfigPackId = OConfigPack.Id
                                        INNER JOIN ODesign 
                                            ON RConfigPackDesign.R2DesignId = ODesign.Id WHERE OConfigPack.Id = @selected_OCP_ID");

                cmd.Parameters.AddWithValue("@selected_OCP_ID", selected_OCP_ID);


                //creates DataTable, fills with filtred records and eliminates duplicates
                tableODesign = CreateTable(cmd, ODesign).DefaultView.ToTable(true);

                //sorts by Id
                DataView dv = tableODesign.DefaultView;
                dv.Sort = "Id";
                tableODesign = dv.ToTable();

                //binds table with DataGridView_ODesign
                dataGridView.DataSource = tableODesign;

                log.Info("Successfully loaded data to DGV_ODesign.");
                LoggerController.WriteDataTableContentToLogs(tableODesign);

            }

            catch (Exception ex)
            {
                log.Error("LoadDataToDGV_OConfigPack() failed.");
                log.Error("Selected OConfigPack ID: " + selected_OCP_ID);
                log.Error(ex);

                MessageBox.Show("Nie udało się załadować danych z serwera. Upewnij się czy tabela ODesign na serwerze posiada odpowiednią strukturę. W przypadku dalszych problemów skontaktuj się z producentem.");
            }
            
        }

        /// <summary>
        /// Creates single table with data desirable to save - ready for merging with DataSet and then saving to file.
        /// The method contains independent logic for every table (invoked according to tablename parameter).
        /// </summary>
        /// <param name="tablename">The name of the table to obtain.</param>
        /// <param name="selected_OCP_ID">Integer with "Id" value of selected OConfigPack on UI.</param>
        /// <param name="selected_OD_Rows">Collection of selected rows on ODesign DataGridView.</param>
        /// <returns>Returns table with desirable data.</returns>        
        private DataTable CreateTableWithFiltredData(string tablename, int selected_OCP_ID, DataGridViewSelectedRowCollection selected_OD_Rows)
        {
            DataTable targetTable = null;
            SqlConnection connection = null;

            log.Info("CreateTableWithFiltredData() invoked. tablename parameter: " + tablename);


            switch (tablename)
            {

                case OConfigPack:

                    try
                    {
                        //copies from Data Base only selected record
                        SqlCommand cmd = new SqlCommand(@"SELECT Id, Name, Description FROM OConfigPack WHERE Id = @selected_OCP_ID");
                        cmd.Parameters.AddWithValue("@selected_OCP_ID", selected_OCP_ID);

                        targetTable = CreateTable(cmd, OConfigPack);

                        log.Info(tablename + " successfully created.");
                        LoggerController.WriteDataTableContentToLogs(targetTable);

                        return targetTable;
                    }
                    catch (Exception ex)
                    {
                        log.Error("CreateTableWithFiltredData() failed. tablename parameter: " + tablename);
                        log.Error("selected_OCP_row: " + selected_OCP_ID);
                        log.Error(ex);

                        return null;
                    }

                case ODesign:

                    try
                    {
                        DataGridViewSelectedRowCollection rows = selected_OD_Rows;

                        //creates empty table and copies metadata from actual ODesign table
                        targetTable = tableODesign.Clone();

                        //converts every selected record from DataGridView format to DataTable format and updates the table 
                        foreach (DataGridViewRow row in rows)
                        {
                            DataRow rowTemp = (row.DataBoundItem as DataRowView).Row;
                            targetTable.ImportRow(rowTemp);
                        }

                        log.Info(tablename + " successfully created.");
                        LoggerController.WriteDataTableContentToLogs(targetTable);

                        return targetTable;
                    }

                    catch (Exception ex)
                    {
                        log.Error("CreateTableWithFiltredData() failed. tablename parameter: " + tablename);
                        log.Error(ex);

                        return null;
                    }


                case RConfigPackDesign:

                    try
                    {
                        DataGridViewSelectedRowCollection rows = selected_OD_Rows;
                        targetTable = new DataTable(RConfigPackDesign);
                        connection = new SqlConnection(ConnectionStringController.GetConnectionStringFromFile());


                        //for every selected record from DataGridView ODesign - finds 1 RConfigPackDesign matching to selected OConfigPack and selected ODesign
                        foreach (DataGridViewRow row in rows)
                        {
                            string current_OD_ID = row.Cells["Id"].Value.ToString();

                            connection.Open();

                            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Id, R1ConfigPackId, R2DesignId FROM RConfigPackDesign WHERE R1ConfigPackId = @selected_OCP_ID AND R2DesignId = @current_OD_ID", connection);
                            cmd.Parameters.AddWithValue("@selected_OCP_ID", selected_OCP_ID);
                            cmd.Parameters.AddWithValue("@current_OD_ID", current_OD_ID);
                            targetTable.Load(cmd.ExecuteReader());

                            connection.Close();
                        }

                        log.Info(tablename + " successfully created.");
                        LoggerController.WriteDataTableContentToLogs(targetTable);

                        return targetTable;
                    }

                    catch (Exception ex)
                    {
                        log.Error("CreateTableWithFiltredData() failed. tablename parameter: " + tablename);
                        log.Error(ex);

                        return null;
                    }


                default:
                    return null;
            }

        }

        /// <summary>
        /// Creates DataSet and merges with tables prepared by CreateTableWithFiltredData() method.
        /// </summary>
        /// <param name="selected_OCP_ID">Integer with "Id" value of selected OConfigPack on UI.</param>
        /// <param name="selected_OD_Rows">Collection of selected rows on ODesign DataGridView.</param>
        /// <returns>Returns DataSet with all tables with desirable data.</returns>
        public DataSet CreateDataSetWithFiltredTables(int selected_OCP_ID, DataGridViewSelectedRowCollection selected_OD_Rows)
        {
            log.Info("CreateDataSetWithFiltredTables() invoked.");
            DataSet dataSet = new DataSet();

            try
            {
                foreach (string table in tablesNames)
                {
                    dataSet.Merge(CreateTableWithFiltredData(table, selected_OCP_ID, selected_OD_Rows));
                    dataSet.Tables[table].PrimaryKey = null;
                    dataSet.Tables[table].Columns.Remove("Id");                   
                }

                log.Info("CreateDataSetWithFiltredTables() succeded.");
                return dataSet;
            }
            catch (Exception ex)
            {
                log.Error("CreateDataSetWithFiltredTables() failed.");
                log.Error(ex);
                return null;
            }         
        }


        /// <summary>
        /// Adds "Id" columns to OConfigPack and ODesign tables inside DataSet and fills them with values according to records from RConfigPackDesign.
        /// </summary>
        /// <param name="dataSet">DataSet destined to add "Id" columns with values to.</param>
        /// <returns>Returns DataSet with additional "Id" columns filled with values according to records from RConfigPackDesign.</returns>
        private DataSet AddIdsToDataSet(DataSet dataSet)
        {
            log.Info("AddIdsToDataSet() invoked.");

            try
            {
                //All records at RConfigPackDesign indicate the same OConfigPack.
                //Gets "Id" value from the first record (RConfigPackDesign.R1ConfigPackId column) and adds it to the first OConfigPack record at "Id" Column.
                dataSet.Tables[OConfigPack].Columns.Add("Id", typeof(Int32)).SetOrdinal(0);
                dataSet.Tables[OConfigPack].Rows[0]["Id"] = dataSet.Tables[RConfigPackDesign].Rows[0]["R1ConfigPackId"];


                //The number of RConfigPackDesign records should be equal to the number of ODesign records (duplicates are eliminated).
                //Gets all "Id" values from RConfigPackDesign.R2DesignId column and adds them to ODesign "Id" Column.
                dataSet.Tables[ODesign].Columns.Add("Id", typeof(Int32)).SetOrdinal(0);

                if (dataSet.Tables[ODesign].Rows.Count == dataSet.Tables[RConfigPackDesign].Rows.Count)
                {
                    for (int i = 0; i < dataSet.Tables[ODesign].Rows.Count; i++)
                    {
                        dataSet.Tables[ODesign].Rows[i]["Id"] = dataSet.Tables[RConfigPackDesign].Rows[i]["R2DesignId"];
                    }

                    log.Info("Content of OConfigPack and ODesign after adding Ids");
                    LoggerController.WriteDataTableContentToLogs(dataSet.Tables[OConfigPack]);
                    LoggerController.WriteDataTableContentToLogs(dataSet.Tables[ODesign]);

                    return dataSet;
                }

                else
                {
                    log.Error("ODesign.Count != RConfigPackDesign.Count");
                    MessageBox.Show("Liczba rekordów ODesign nie jest zgodna z RConfigPackDesign. Jeżeli po wygenerowaniu przez aplikację plik nie był modifykowany, skontaktuj się z producentem.");
                    return null;
                }
            }

            catch (Exception ex)
            {
                log.Error("AddIdsToDataSet() failed."); 
                log.Error(ex);
                return null;
            }


          


        }


        /// <summary>
        /// Sends data from each table of DataSet to DataBase server.
        /// </summary>
        /// <param name="dataSet">Contains database loaded from XML file.</param>
        public void SendDataSetToDataBase(DataSet dataSet)
        {
            log.Info("SendDataSetToDataBase() invoked.");

            dataSet = AddIdsToDataSet(dataSet);

            string messageToDisplay = "";

            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(ConnectionStringController.GetConnectionStringFromFile(), SqlBulkCopyOptions.KeepIdentity))
                {
                    bulkCopy.DestinationTableName = OConfigPack;
                    bulkCopy.ColumnMappings.Clear();
                    AddColumnMappings(new string[] { "Id", "Name", "Description" }, bulkCopy);
                    log.Info("Invoking bulkCopy.WriteToServer(dataSet.Tables[OConfigPack])");
                    bulkCopy.WriteToServer(dataSet.Tables[OConfigPack]);
                    messageToDisplay += "OConfigPack: Zaimportowano dane.\n";
                }

 
            }
            catch (Exception ex)
            {               
                log.Error("Exception at OConfigPack.");
                log.Error(ex);
                messageToDisplay += "OConfigPack: Nie udało się zaimportować danych. Upewnij się czy nie wystąpił konflikt kluczy.\n";
            }

            try
            {

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(ConnectionStringController.GetConnectionStringFromFile(), SqlBulkCopyOptions.KeepIdentity))
                {
                 

                    bulkCopy.DestinationTableName = ODesign;
                    AddColumnMappings(new string[] { "Id", "Name", "Description", "Content" }, bulkCopy);
                    log.Info("Invoking bulkCopy.WriteToServer(dataSet.Tables[ODesign])");
                    bulkCopy.WriteToServer(dataSet.Tables[ODesign]);
                    messageToDisplay += "ODesign: Zaimportowano dane.\n";
                }

            }
            catch (Exception ex)
            {

                log.Error("Exception at ODesign.");
                log.Error(ex);
                messageToDisplay += "ODesign: Nie udało się zaimportować danych. Upewnij się czy nie wystąpił konflikt kluczy.\n";
            }


            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(ConnectionStringController.GetConnectionStringFromFile()))
                {
                    bulkCopy.DestinationTableName = RConfigPackDesign;
                    bulkCopy.ColumnMappings.Clear();
                    AddColumnMappings(new string[] { "R1ConfigPackId", "R2DesignId" }, bulkCopy);
                    log.Info("Invoking bulkCopy.WriteToServer(dataSet.Tables[OConfigPack])");
                    bulkCopy.WriteToServer(dataSet.Tables[RConfigPackDesign]);
                    messageToDisplay += "RConfigPackDesign: Zaimportowano dane.";
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception at RConfigPack.");
                log.Error(ex);
                messageToDisplay += "RConfigPackDesign: Nie udało się zaimportować danych. Upewnij się czy nie wystąpił konflikt kluczy.";
            }

            MessageBox.Show(messageToDisplay);
        }

        /// <summary>
        /// Helper method for SendDataSetToDataBase().
        /// Performs ColumnMappings for all tables listed in String Array parameter.
        /// </summary>
        /// <param name="columns">Array String with the names of columns to map.</param>
        /// <param name="bulk">SqlBulkCopy which performs mapping.</param>
        private void AddColumnMappings(string[] columns, SqlBulkCopy bulk)
        {
            log.Info("AddColumnMappings() invoked.");

            try
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    bulk.ColumnMappings.Add(columns[i], columns[i]);
                }

                log.Info("AddColumnMappings() succeded.");
            }

            catch (Exception ex)
            {
                log.Error("AddColumnMappings() failed.");
                log.Error(ex);
            }

        }

      

    }
}