using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Windows.Forms;

namespace com.netinfocentral.ClayUI
{
    class AppPartDataAdapter
    {
        // define member variables
        private SqlCeDataAdapter _db = null;
        private SqlCeConnection _conn = null;
        private ClayUIDatabaseHelper _dbHelper = null;
        private string[] _columns = { AppPartDatabaseHelper.COLUMN_ID,
                                       AppPartDatabaseHelper.COLUMN_APP_PART_NAME,
                                       AppPartDatabaseHelper.COLUMN_VERSION};
        private int _appID;
        private string _baseURI;

        // default constructor
        public AppPartDataAdapter(int appID, string baseURI)
        {
            this._appID = appID;
            this._baseURI = baseURI;
            this._dbHelper = new ClayUIDatabaseHelper(this._appID, this._baseURI);
            this._db = new SqlCeDataAdapter();
        }

        // method to clear app part tables
        public void ClearDatabase()
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + AppPartDatabaseHelper.TABLE_NAME;
                    this._db.DeleteCommand = cmd;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (null != this._db.DeleteCommand) this._db.DeleteCommand.Dispose();
                }
            }
        }

        /**
         * Methods for handling reading/writing to app part tables
         **/

        // method to add an app part record to database
        public AppPart CreateAppPart(int appPartID, string appPartName, int version)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO " + AppPartDatabaseHelper.TABLE_NAME + " (" +
                        AppPartDatabaseHelper.COLUMN_ID + ", " +
                        AppPartDatabaseHelper.COLUMN_APP_PART_NAME + ", " +
                        AppPartDatabaseHelper.COLUMN_VERSION + ") VALUES (" +
                        "@" + AppPartDatabaseHelper.COLUMN_ID + ", " +
                        "@" + AppPartDatabaseHelper.COLUMN_APP_PART_NAME + ", " +
                        "@" + AppPartDatabaseHelper.COLUMN_VERSION + ")";
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + AppPartDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = appPartID;
                    SqlCeParameter p_appPartName = cmd.Parameters.Add("@" + AppPartDatabaseHelper.COLUMN_APP_PART_NAME, SqlDbType.NVarChar);
                    p_appPartName.Value = appPartName;
                    SqlCeParameter p_version = cmd.Parameters.Add("@" + AppPartDatabaseHelper.COLUMN_VERSION, SqlDbType.Int);
                    p_version.Value = version;
                    adp.InsertCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    DataTable table = ds.Tables[0];
                    DataRow row = table.Rows[0];
                    AppPart appPart = DataRowToAppPart(row);
                    ds.Dispose();
                    return appPart;
                }
                catch (Exception e)
                {
                    AppPart appPart = new AppPart(0, "NULL", 0);
                    Console.WriteLine(e.Message);
                    return appPart;
                }
            }
        }

        // method to update an existing app part
        public AppPart UpdateAppPart(int appPartID, string appPartName, int version)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "UPDATE " + AppPartDatabaseHelper.TABLE_NAME + "SET " +
                        AppPartDatabaseHelper.COLUMN_APP_PART_NAME + " = @" + AppPartDatabaseHelper.COLUMN_APP_PART_NAME + ", " +
                        AppPartDatabaseHelper.COLUMN_VERSION + " = @" + AppPartDatabaseHelper.COLUMN_VERSION + " " +
                        "WHERE " + AppPartDatabaseHelper.COLUMN_ID + " = @" + AppPartDatabaseHelper.COLUMN_ID;
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + AppPartDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = appPartID;
                    SqlCeParameter p_appPartName = cmd.Parameters.Add("@" + AppPartDatabaseHelper.COLUMN_APP_PART_NAME, SqlDbType.NVarChar);
                    p_appPartName.Value = appPartName;
                    SqlCeParameter p_version = cmd.Parameters.Add("@" + AppPartDatabaseHelper.COLUMN_VERSION, SqlDbType.Int);
                    p_version.Value = version;
                    adp.UpdateCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    DataTable table = ds.Tables[0];
                    DataRow row = table.Rows[0];
                    AppPart appPart = DataRowToAppPart(row);
                    ds.Dispose();
                    return appPart;
                }
                catch (Exception e)
                {
                    AppPart appPart = new AppPart(0, "NULL", 0);
                    Console.WriteLine(e.Message);
                    return appPart;
                }
            }
        }

        // method to delete existing app part record
        public void DeleteAppPart(int appPartID)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + AppPartDatabaseHelper.TABLE_NAME +
                        "WHERE " + AppPartDatabaseHelper.COLUMN_ID + " = @" + AppPartDatabaseHelper.COLUMN_ID;
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + AppPartDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = appPartID;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // method to return a list array of appPart objects
        public List<AppPart> GetAllAppParts()
        {
            List<AppPart> appParts = new List<AppPart>();

            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "SELECT " + AppPartDatabaseHelper.COLUMN_ID + ", " +
                        AppPartDatabaseHelper.COLUMN_APP_PART_NAME + ", " +
                        AppPartDatabaseHelper.COLUMN_VERSION + " FROM " +
                        AppPartDatabaseHelper.TABLE_NAME;
                    adp.SelectCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            AppPart appPart = DataRowToAppPart(row);
                            appParts.Add(appPart);
                        }
                    }

                    ds.Dispose();
                    return appParts;
                }
                catch (Exception e)
                {
                    AppPart appPart = new AppPart(0, "NULL", 0);
                    appParts.Add(appPart);
                    Console.WriteLine(e.Message);
                    return appParts;
                }
            }
        }

        // method to return an app part object
        public AppPart GetAppPart(int appPartID)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "SELECT " + AppPartDatabaseHelper.COLUMN_ID + ", " +
                        AppPartDatabaseHelper.COLUMN_APP_PART_NAME + ", " +
                        AppPartDatabaseHelper.COLUMN_VERSION + " FROM " +
                        AppPartDatabaseHelper.TABLE_NAME + " WHERE " +
                        AppPartDatabaseHelper.COLUMN_ID + " = " + appPartID;
                    adp.SelectCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    DataTable table = ds.Tables[0];
                    DataRow row = table.Rows[0];
                    AppPart appPart = DataRowToAppPart(row);

                    ds.Dispose();
                    return appPart;
                }
                catch (Exception e)
                {
                    AppPart appPart = new AppPart(0, "NULL", 0);
                    Console.WriteLine(e.Message);
                    return appPart;
                }
            }
        }

        // method to return if an app part has an exisiting data table
        public bool DataTableExists(string appPartName)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + appPartName + "'";
                    adp.SelectCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    DataTable table = ds.Tables[0];

                    if (table.Rows.Count < 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        // method to convert a datarow to an AppPart
        public AppPart DataRowToAppPart(DataRow dr)
        {
            AppPart appPart = new AppPart(int.Parse(dr[0].ToString()), dr[1].ToString(), int.Parse(dr[2].ToString()));
            return appPart;
        }

        /**
         * Methods for handling reading/writing to temp app part tables
         **/

        // method to add an app part record to database
        public void CreateTempAppPart(int appPartID, string appPartName, int version)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO " + AppPartDatabaseHelper.TEMP_TABLE_NAME + " (" +
                        AppPartDatabaseHelper.COLUMN_ID + ", " +
                        AppPartDatabaseHelper.COLUMN_APP_PART_NAME + ", " +
                        AppPartDatabaseHelper.COLUMN_VERSION + ") VALUES (" +
                        "@" + AppPartDatabaseHelper.COLUMN_ID + ", " +
                        "@" + AppPartDatabaseHelper.COLUMN_APP_PART_NAME + ", " +
                        "@" + AppPartDatabaseHelper.COLUMN_VERSION + ")";
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + AppPartDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = appPartID;
                    SqlCeParameter p_appPartName = cmd.Parameters.Add("@" + AppPartDatabaseHelper.COLUMN_APP_PART_NAME, SqlDbType.NVarChar);
                    p_appPartName.Value = appPartName;
                    SqlCeParameter p_version = cmd.Parameters.Add("@" + AppPartDatabaseHelper.COLUMN_VERSION, SqlDbType.Int);
                    p_version.Value = version;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // method to clear temp app parts table
        public void DeleteTempAppParts()
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + AppPartDatabaseHelper.TEMP_TABLE_NAME;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /***
         * Methods to handle syncing the TEMP tables with actual app part tables
         **/
        
        // method to lead temp table from array from web service
        public void SyncWithTempTable(List<AppPart> appParts)
        {
            // clear temp table
            this.DeleteTempAppParts();

            // loop through list array and add to app parts
            foreach (AppPart appPart in appParts)
            {
                this.CreateTempAppPart(appPart.AppPartID, appPart.AppPartName, appPart.Version);
            }

            // sync temp table with app part table
            this.Sync();
        }

        // method to sync temp table with app part table
        private void Sync()
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();

                    //delete records in app part table which are not in temp table (Delete all due to limitaion in SQL Compact Edition)
                    cmd.CommandText = "DELETE FROM " + AppPartDatabaseHelper.TABLE_NAME; /** +" " +
                        "WHERE " + AppPartDatabaseHelper.COLUMN_ID + " NOT IN (SELECT " + AppPartDatabaseHelper.COLUMN_ID +
                        " FROM " + AppPartDatabaseHelper.TEMP_TABLE_NAME + ")";**/
                    cmd.ExecuteNonQuery();

                    // update records that exist in both tables (Skip due to limitation in SQL Compact Edition)
                    /**cmd.CommandText = "UPDATE " + AppPartDatabaseHelper.TABLE_NAME + " " +
                        "SET " + AppPartDatabaseHelper.COLUMN_APP_PART_NAME + " = t." + AppPartDatabaseHelper.COLUMN_APP_PART_NAME + ", " +
                        AppPartDatabaseHelper.COLUMN_VERSION + " = t." + AppPartDatabaseHelper.COLUMN_VERSION + " FROM " +
                        AppPartDatabaseHelper.TABLE_NAME + " JOIN " + AppPartDatabaseHelper.TEMP_TABLE_NAME + " t ON (" +
                        AppPartDatabaseHelper.TABLE_NAME + "." + AppPartDatabaseHelper.COLUMN_ID + " = t." + AppPartDatabaseHelper.COLUMN_ID + ")";
                    string sql = cmd.CommandText;
                    cmd.ExecuteNonQuery();**/

                    // insert records that do not exist in app parts table
                    cmd.CommandText = "INSERT INTO " + AppPartDatabaseHelper.TABLE_NAME + " " +
                        "SELECT t." + AppPartDatabaseHelper.COLUMN_ID + ", t." +
                        AppPartDatabaseHelper.COLUMN_APP_PART_NAME + ", t." +
                        AppPartDatabaseHelper.COLUMN_VERSION + " FROM " + AppPartDatabaseHelper.TEMP_TABLE_NAME + " t " +
                        "LEFT OUTER JOIN " + AppPartDatabaseHelper.TABLE_NAME + " a ON (t." + AppPartDatabaseHelper.COLUMN_ID + " = " +
                        "a." + AppPartDatabaseHelper.COLUMN_ID + ") " +
                        "WHERE a." + AppPartDatabaseHelper.COLUMN_ID + " IS NULL";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /**
         * method to save app part data to its respective data table
         * @param appPartName 
         * @param layout
         **/
        public void SaveAppPartData(string appPartName, FlowLayoutPanel layout)
        {
            ElementDataAdapter adapter = new ElementDataAdapter();
            List<string> elementNames = new List<string>();
            List<string> appPartValues = new List<string>();

            // loop through layout components and get values for items with storeable data
            foreach (Control control in layout.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    TextBox textbox = (TextBox)control;
                    appPartValues.Add(textbox.Text);
                    Element element = adapter.GetElement(int.Parse(textbox.Name));
                    elementNames.Add(element.ElementID + "." + element.ElementName);
                }
                else if (control.GetType() == typeof(CheckBox))
                {
                    CheckBox checkbox = (CheckBox)control;
                    if (checkbox.Checked)
                    {
                        appPartValues.Add("1");
                    }
                    else
                    {
                        appPartValues.Add("0");
                    }
                    Element element = adapter.GetElement(int.Parse(checkbox.Name));
                    elementNames.Add(element.ElementID + "." + element.ElementName);
                }
                else if (control.GetType() == typeof(ComboBox))
                {
                    ComboBox combobox = (ComboBox)control;
                    appPartValues.Add(combobox.SelectedText);
                    Element element = adapter.GetElement(int.Parse(combobox.Name));
                    elementNames.Add(element.ElementID + "." + element.ElementName);
                }
                else if (control.GetType() == typeof(FlowLayoutPanel))
                {
                    FlowLayoutPanel radiogroup = (FlowLayoutPanel)control;

                    foreach (Control cntrl in radiogroup.Controls)
                    {
                        if (cntrl.GetType() == typeof(RadioButton))
                        {
                            // TODO partial method stub.
                        }
                    }
                }
            }

            // build insert query
            string sql = "INSERT INTO " + appPartName + "(";

            // iterate though list arrays to build insert query
            foreach (string columnName in elementNames)
            {
                sql = sql + "'" + columnName + "', ";
            }
            // trim trailing ", "
            sql = sql.Substring(0, sql.Length - 2) + ") VALUES (";

            // iterate through values
            foreach (string value in appPartValues)
            {
                sql = sql + "'" + value +"', ";
            }
            // trim trailing ", "
            sql = sql.Substring(0, sql.Length - 2) + ")";

            try
            {
                this._conn.Open();
                SqlCeCommand cmd = this._conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /**
         * method to get app part data records
         * @param appPartName
         * @return DataTableRecord object
         **/
        public List<DataTableRecord> GetAppPartData(string appPartName)
        {
            // array list to hold data table records
            List<DataTableRecord> dataTableRecords = new List<DataTableRecord>();

            // query data table to get values that have not been sent to web.
            try
            {
                this._conn.Open();
                SqlCeDataAdapter adp = new SqlCeDataAdapter();
                SqlCeCommand cmd = this._conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + appPartName + " WHERE sentToWeb = 0";
                adp.SelectCommand = cmd;

                DataSet ds = new DataSet();
                adp.Fill(ds);
                
                // loop through dataset 
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        List<string> columns = new List<string>();
                        List<string> values = new List<string>();

                        foreach (DataColumn column in table.Columns)
                        {
                            if (column.ColumnName.Equals("sentToWeb") == false &&
                                column.ColumnName.Equals("_ID") == false)
                            {
                                columns.Add(column.ColumnName);
                                values.Add(row[column.ColumnName].ToString());
                            }
                        }
                        DataTableRecord record = new DataTableRecord(columns, values);
                        dataTableRecords.Add(record);
                    }
                }

                ds.Dispose();
                return dataTableRecords;
            }
            catch (Exception e)
            {
                List<string> columns = new List<string>();
                List<string> values = new List<string>();
                columns.Add("");
                values.Add("");
                DataTableRecord record = new DataTableRecord(columns, values);
                dataTableRecords.Add(record);
                Console.WriteLine(e.Message);
                return dataTableRecords;
            }
        }

        // method to update sent to web value in existing records
        public void UpdateSentToWebStatus(string appPartName)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "UPDATE " + appPartName + " SET sentToWeb = 1 WHERE sentToWeb = 0";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
