using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace com.netinfocentral.ClayUI
{
    class ElementOptionDataAdapter
    {
        // define member variables
        private SqlCeDataAdapter _db = null;
        private SqlCeConnection _conn = null;
        private ElementOptionDatabaseHelper _dbHelper = null;
        private string[] _columns = { ElementOptionDatabaseHelper.COLUMN_ID,
                                        ElementOptionDatabaseHelper.COLUMN_APP_PART_ID,
                                        ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID,
                                        ElementOptionDatabaseHelper.COLUMN_VALUE,
                                        ElementOptionDatabaseHelper.COLUMN_DESCRIPTION,
                                        ElementOptionDatabaseHelper.COLUMN_VERSION};

        // default constructor
        public ElementOptionDataAdapter()
        {
            this._dbHelper = new ElementOptionDatabaseHelper();
            this._db = new SqlCeDataAdapter();
        }

        // method to clear element option tables
        public void ClearDatabase()
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + ElementOptionDatabaseHelper.TABLE_NAME;
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

        // method to add an element option object to database
        public ElementOption CreateElementOption(int elementOptionID, int appPartID, int elementID, string value,
            string description, int version)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO " + ElementOptionDatabaseHelper.TABLE_NAME + " (" +
                        ElementOptionDatabaseHelper.COLUMN_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_VALUE + ", " +
                        ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + ", " +
                        ElementOptionDatabaseHelper.COLUMN_VERSION + ") VALUES (" +
                        "@" + ElementOptionDatabaseHelper.COLUMN_ID + ", " +
                        "@" + ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        "@" + ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + ", " +
                        "@" + ElementOptionDatabaseHelper.COLUMN_VALUE + ", " +
                        "@" + ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + ", " +
                        "@" + ElementOptionDatabaseHelper.COLUMN_VERSION + ")";
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = elementOptionID;
                    SqlCeParameter p_appPartID = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_APP_PART_ID, SqlDbType.Int);
                    p_appPartID.Value = appPartID;
                    SqlCeParameter p_elementID = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID, SqlDbType.Int);
                    p_elementID.Value = elementID;
                    SqlCeParameter p_value = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_VALUE, SqlDbType.NVarChar);
                    p_value.Value = value;
                    SqlCeParameter p_descr = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_DESCRIPTION, SqlDbType.NVarChar);
                    p_descr.Value = description;
                    SqlCeParameter p_version = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_VERSION, SqlDbType.Int);
                    p_version.Value = version;
                    adp.InsertCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    DataTable table = ds.Tables[0];
                    DataRow row = table.Rows[0];
                    ElementOption option = DataRowToElementOption(row);
                    ds.Dispose();
                    return option;
                }
                catch (Exception e)
                {
                    ElementOption option = new ElementOption(0, 0, 0, "", "", 0);
                    Console.WriteLine(e.Message);
                    return option;
                }
            }
        }

        // method to update element option
        public ElementOption UpdateElementOption(int elementOptionID, int appPartID, int elementID, string value,
            string description, int version)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "UPDATE " + ElementOptionDatabaseHelper.TABLE_NAME + "SET " +
                        ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + " = @" + ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + " = @" + ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_VALUE + " = @" + ElementOptionDatabaseHelper.COLUMN_VALUE + ", " +
                        ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + " = @" + ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + ", " +
                        ElementOptionDatabaseHelper.COLUMN_VERSION + " = @" + ElementOptionDatabaseHelper.COLUMN_VERSION +
                        "WHERE " + ElementOptionDatabaseHelper.COLUMN_ID + " = @" + ElementOptionDatabaseHelper.COLUMN_ID;
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = elementOptionID;
                    SqlCeParameter p_appPartID = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_APP_PART_ID, SqlDbType.Int);
                    p_appPartID.Value = appPartID;
                    SqlCeParameter p_elementID = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID, SqlDbType.Int);
                    p_elementID.Value = elementID;
                    SqlCeParameter p_value = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_VALUE, SqlDbType.NVarChar);
                    p_value.Value = value;
                    SqlCeParameter p_descr = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_DESCRIPTION, SqlDbType.NVarChar);
                    p_descr.Value = description;
                    SqlCeParameter p_version = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_VERSION, SqlDbType.Int);
                    p_version.Value = version;
                    adp.InsertCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    DataTable table = ds.Tables[0];
                    DataRow row = table.Rows[0];
                    ElementOption option = DataRowToElementOption(row);
                    ds.Dispose();
                    return option;
                }
                catch (Exception e)
                {
                    ElementOption option = new ElementOption(0, 0, 0, "", "", 0);
                    Console.WriteLine(e.Message);
                    return option;
                }
            }
        }

        // method to delete an existing element option
        public void DeleteElementOption(int elementOptionID)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + ElementOptionDatabaseHelper.TABLE_NAME +
                        "WHERE " + ElementOptionDatabaseHelper.COLUMN_ID + " = @" + ElementOptionDatabaseHelper.COLUMN_ID;
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = elementOptionID;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // method to return a list array of element options for a specifc emlement
        public List<ElementOption> GetAllElementOptions(int elementOptionID)
        {
            List<ElementOption> elementOptions = new List<ElementOption>();

            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "SELECT " + ElementOptionDatabaseHelper.COLUMN_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_VALUE + ", " +
                        ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + ", " +
                        ElementOptionDatabaseHelper.COLUMN_VERSION + " FROM " +
                        ElementOptionDatabaseHelper.TABLE_NAME +
                        "WHERE " + ElementOptionDatabaseHelper.COLUMN_ID + " = " + elementOptionID;
                    adp.SelectCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            ElementOption option = DataRowToElementOption(row);
                            elementOptions.Add(option);
                        }
                    }

                    ds.Dispose();
                    return elementOptions;
                }
                catch (Exception e)
                {
                    ElementOption option = new ElementOption(0, 0, 0, "", "", 0);
                    elementOptions.Add(option);
                    Console.WriteLine(e.Message);
                    return elementOptions;
                }
            }

        }

        // method to return a list array of element options
        public List<ElementOption> GetAllElementOptions()
        {
            List<ElementOption> elementOptions = new List<ElementOption>();

            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "SELECT " + ElementOptionDatabaseHelper.COLUMN_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_VALUE + ", " +
                        ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + ", " +
                        ElementOptionDatabaseHelper.COLUMN_VERSION + " FROM " +
                        ElementOptionDatabaseHelper.TABLE_NAME;
                    adp.SelectCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            ElementOption option = DataRowToElementOption(row);
                            elementOptions.Add(option);
                        }
                    }

                    ds.Dispose();
                    return elementOptions;
                }
                catch (Exception e)
                {
                    ElementOption option = new ElementOption(0, 0, 0, "", "", 0);
                    elementOptions.Add(option);
                    Console.WriteLine(e.Message);
                    return elementOptions;
                }
            }

        }

        // method to return an element option from data row
        public ElementOption DataRowToElementOption(DataRow dr)
        {
            return new ElementOption(int.Parse(dr[0].ToString()), int.Parse(dr[1].ToString()),
                int.Parse(dr[2].ToString()), dr[3].ToString(), dr[4].ToString(), int.Parse(dr[5].ToString()));
        }

        /**
         * Methods for handling syncing the TEMP table with the actual element options table
         **/

        // method to load the temp table from array from web service
        public void SyncWithTempTable(List<ElementOption> elementOptions)
        {
            // clear temp table
            this.DeleteTempElementOptions();

            // loop through list array and add to element options
            foreach (ElementOption option in elementOptions)
            {
                this.CreateTempElementOption(option.ElementOptionID,
                    option.AppPartID,
                    option.ElementID,
                    option.Value,
                    option.Description,
                    option.Version);
            }

            // sync temp table with element options table
            this.Sync();
        }

        // method to add an element option to the temp table
        public void CreateTempElementOption(int elementOptionID, int appPartID, int elementID, string value,
            string description, int version)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO " + ElementOptionDatabaseHelper.TEMP_TABLE_NAME + " (" +
                        ElementOptionDatabaseHelper.COLUMN_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + ", " +
                        ElementOptionDatabaseHelper.COLUMN_VALUE + ", " +
                        ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + ", " +
                        ElementOptionDatabaseHelper.COLUMN_VERSION + ") VALUES (" +
                        "@" + ElementOptionDatabaseHelper.COLUMN_ID + ", " +
                        "@" + ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        "@" + ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + ", " +
                        "@" + ElementOptionDatabaseHelper.COLUMN_VALUE + ", " +
                        "@" + ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + ", " +
                        "@" + ElementOptionDatabaseHelper.COLUMN_VERSION + ")";
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = elementOptionID;
                    SqlCeParameter p_appPartID = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_APP_PART_ID, SqlDbType.Int);
                    p_appPartID.Value = appPartID;
                    SqlCeParameter p_elementID = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID, SqlDbType.Int);
                    p_elementID.Value = elementID;
                    SqlCeParameter p_value = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_VALUE, SqlDbType.NVarChar);
                    p_value.Value = value;
                    SqlCeParameter p_descr = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_DESCRIPTION, SqlDbType.NVarChar);
                    p_descr.Value = description;
                    SqlCeParameter p_version = cmd.Parameters.Add("@" + ElementOptionDatabaseHelper.COLUMN_VERSION, SqlDbType.Int);
                    p_version.Value = version;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // method to delete all element options from temp table
        public void DeleteTempElementOptions()
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + ElementOptionDatabaseHelper.TEMP_TABLE_NAME;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // method to sync the temp table with the element options table
        private void Sync()
        {
            try
            {
                this._conn.Open();
                SqlCeCommand cmd = this._conn.CreateCommand();

                //delete records in app part table which are not in temp table
                cmd.CommandText = "DELETE FROM " + ElementOptionDatabaseHelper.TABLE_NAME + " " +
                    "WHERE " + ElementOptionDatabaseHelper.COLUMN_ID + " NOT IN (SELECT " + ElementOptionDatabaseHelper.COLUMN_ID +
                    " FROM " + ElementOptionDatabaseHelper.TEMP_TABLE_NAME + ")";
                cmd.ExecuteNonQuery();

                // update records that exist in both tables
                cmd.CommandText = "UPDATE " + ElementOptionDatabaseHelper.TABLE_NAME + " " +
                    "SET " + ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + " = t." + ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                    ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + " = t." + ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                    ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + " = t." + ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + ", " +
                    ElementOptionDatabaseHelper.COLUMN_VALUE + " = t." + ElementOptionDatabaseHelper.COLUMN_VALUE + ", " +
                    ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + " = t." + ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + ", " +
                    ElementOptionDatabaseHelper.COLUMN_VERSION + " = t." + ElementOptionDatabaseHelper.COLUMN_VERSION + ", " +
                    ElementOptionDatabaseHelper.TABLE_NAME + " JOIN " + ElementOptionDatabaseHelper.TEMP_TABLE_NAME + " t ON (" +
                    ElementOptionDatabaseHelper.TABLE_NAME + "." + ElementOptionDatabaseHelper.COLUMN_ID + " = t." + ElementOptionDatabaseHelper.COLUMN_ID + ")";
                cmd.ExecuteNonQuery();

                // insert records that do not exist in app parts table
                cmd.CommandText = "INSERT INTO " + ElementOptionDatabaseHelper.TABLE_NAME + " " +
                    "SELECT " + ElementOptionDatabaseHelper.COLUMN_ID + ", " +
                    ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                    ElementOptionDatabaseHelper.COLUMN_ELEMENT_ID + ", " +
                    ElementOptionDatabaseHelper.COLUMN_VALUE + ", " +
                    ElementOptionDatabaseHelper.COLUMN_DESCRIPTION + ", " +
                    ElementOptionDatabaseHelper.COLUMN_VERSION + 
                    " FROM " + AppPartDatabaseHelper.TEMP_TABLE_NAME + " t " +
                    "LEFT OUTER JOIN " + ElementOptionDatabaseHelper.TABLE_NAME + " a ON (t." + ElementOptionDatabaseHelper.COLUMN_ID + " = " +
                    "a." + ElementOptionDatabaseHelper.COLUMN_ID + ") " +
                    "WHERE a." + ElementOptionDatabaseHelper.COLUMN_ID + " IS NULL";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
