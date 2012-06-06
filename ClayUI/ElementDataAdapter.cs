using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace com.netinfocentral.ClayUI
{
    class ElementDataAdapter
    {
        // define member variables
        private SqlCeDataAdapter _db = null;
        private SqlCeConnection _conn = null;
        private ElementDatabaseHelper _dbHelper = null;
        private string[] _columns = { ElementDatabaseHelper.COLUMN_ID,
	                                    ElementDatabaseHelper.COLUMN_APP_PART_ID,
	                                    ElementDatabaseHelper.COLUMN_ELEMENT_NAME,
	                                    ElementDatabaseHelper.COLUMN_ELEMENT_TYPE,
	                                    ElementDatabaseHelper.COLUMN_ELEMENT_LABEL,
	                                    ElementDatabaseHelper.COLUMN_LIST_ORDER,
	                                    ElementDatabaseHelper.COLUMN_VERSION};
        // default constructor
        public ElementDataAdapter()
        {
            this._dbHelper = new ElementDatabaseHelper();
            this._db = new SqlCeDataAdapter();
        }

        // method to clear element options table
        public void ClearDatabase()
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + ElementDatabaseHelper.TABLE_NAME;
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

        // method to add an element record to the database
        public Element CreateElement(int elementID, int appPartID, string elementName, int elementType,
            string elementLabel, int listOrder, int version)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO " + ElementDatabaseHelper.TABLE_NAME + " (" +
                        ElementDatabaseHelper.COLUMN_ID + ", " +
                        ElementDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_NAME + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + ", " +
                        ElementDatabaseHelper.COLUMN_LIST_ORDER + ", " +
                        ElementDatabaseHelper.COLUMN_VERSION + ") VALUES (" +
                        "@" + ElementDatabaseHelper.COLUMN_ID + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_ELEMENT_NAME + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_LIST_ORDER + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_VERSION + ")";
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = elementID;
                    SqlCeParameter p_appID = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_APP_PART_ID, SqlDbType.Int);
                    p_appID.Value = appPartID;
                    SqlCeParameter p_elementName = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ELEMENT_NAME, SqlDbType.NVarChar);
                    p_elementName.Value = elementName;
                    SqlCeParameter p_elementType = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ELEMENT_TYPE, SqlDbType.Int);
                    p_elementType.Value = elementType;
                    SqlCeParameter p_elementLabel = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ELEMENT_LABEL, SqlDbType.NVarChar);
                    p_elementLabel.Value = elementLabel;
                    SqlCeParameter p_listOrder = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_LIST_ORDER, SqlDbType.Int);
                    p_listOrder.Value = listOrder;
                    SqlCeParameter p_version = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_VERSION, SqlDbType.Int);
                    p_version.Value = version;
                    adp.InsertCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    DataTable table = ds.Tables[0];
                    DataRow row = table.Rows[0];
                    Element element = DataRowToElement(row);
                    ds.Dispose();
                    return element;
                }
                catch (Exception e)
                {
                    Element element = new Element(0, 0, "", 0, "", 0, 0);
                    Console.WriteLine(e.Message);
                    return element;
                }
            }
        }

        // method to update existing element
        public Element UpdateElement(int elementID, int appPartID, string elementName, int elementType,
            string elementLabel, int listOrder, int version)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "UPDATE " + ElementDatabaseHelper.TABLE_NAME + "SET " +
                        ElementDatabaseHelper.COLUMN_APP_PART_ID + " = @" + ElementDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_NAME + " = @" + ElementDatabaseHelper.COLUMN_ELEMENT_NAME + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + " = @" + ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + " = @" + ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + ", " +
                        ElementDatabaseHelper.COLUMN_LIST_ORDER + " = @" + ElementDatabaseHelper.COLUMN_LIST_ORDER + ", " +
                        ElementDatabaseHelper.COLUMN_VERSION + " = @" + ElementDatabaseHelper.COLUMN_VERSION + ", " +
                        "WHERE " + ElementDatabaseHelper.COLUMN_ID + " = @" + ElementDatabaseHelper.COLUMN_ID;
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = elementID;
                    SqlCeParameter p_appID = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_APP_PART_ID, SqlDbType.Int);
                    p_appID.Value = appPartID;
                    SqlCeParameter p_elementName = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ELEMENT_NAME, SqlDbType.NVarChar);
                    p_elementName.Value = elementName;
                    SqlCeParameter p_elementType = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ELEMENT_TYPE, SqlDbType.Int);
                    p_elementType.Value = elementType;
                    SqlCeParameter p_elementLabel = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ELEMENT_LABEL, SqlDbType.NVarChar);
                    p_elementLabel.Value = elementLabel;
                    SqlCeParameter p_listOrder = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_LIST_ORDER, SqlDbType.Int);
                    p_listOrder.Value = listOrder;
                    SqlCeParameter p_version = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_VERSION, SqlDbType.Int);
                    p_version.Value = version;
                    adp.UpdateCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    DataTable table = ds.Tables[0];
                    DataRow row = table.Rows[0];
                    Element element = DataRowToElement(row);
                    ds.Dispose();
                    return element;
                }
                catch (Exception e)
                {
                    Element element = new Element(0, 0, "", 0, "", 0, 0);
                    Console.WriteLine(e.Message);
                    return element;
                }
            }
        }

        // method to delete an existing element
        public void DeleteElement(int elementID)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + ElementDatabaseHelper.TABLE_NAME +
                        "WHERE " + ElementDatabaseHelper.COLUMN_ID + " = @" + ElementDatabaseHelper.COLUMN_ID;
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = elementID;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // return listarray of all element objects
        public List<Element> GetAllElements()
        {
            List<Element> elements = new List<Element>();

            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "SELECT " + ElementDatabaseHelper.COLUMN_ID + ", " +
                        ElementDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_NAME + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + ", " +
                        ElementDatabaseHelper.COLUMN_LIST_ORDER + ", " +
                        ElementDatabaseHelper.COLUMN_VERSION + " FROM " +
                        ElementDatabaseHelper.TABLE_NAME;
                    adp.SelectCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Element element = DataRowToElement(row);
                            elements.Add(element);
                        }
                    }

                    ds.Dispose();
                    return elements;
                }
                catch (Exception e)
                {
                    Element element = new Element(0, 0, "", 0, "", 0, 0);
                    elements.Add(element);
                    Console.WriteLine(e.Message);
                    return elements;
                }
            }
        }

        // return listarray of all element objects for an app part
        public List<Element> GetAllElements(int appPartID)
        {
            List<Element> elements = new List<Element>();

            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "SELECT " + ElementDatabaseHelper.COLUMN_ID + ", " +
                        ElementDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_NAME + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + ", " +
                        ElementDatabaseHelper.COLUMN_LIST_ORDER + ", " +
                        ElementDatabaseHelper.COLUMN_VERSION + " FROM " +
                        ElementDatabaseHelper.TABLE_NAME + " WHERE " +
                        ElementDatabaseHelper.COLUMN_APP_PART_ID + " = " + appPartID;
                    adp.SelectCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Element element = DataRowToElement(row);
                            elements.Add(element);
                        }
                    }

                    ds.Dispose();
                    return elements;
                }
                catch (Exception e)
                {
                    Element element = new Element(0, 0, "", 0, "", 0, 0);
                    elements.Add(element);
                    Console.WriteLine(e.Message);
                    return elements;
                }
            }
        }

        // method to return an element object from the local database
        public Element GetElement(int elementID)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeDataAdapter adp = new SqlCeDataAdapter();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "SELECT " + ElementDatabaseHelper.COLUMN_ID + ", " +
                        ElementDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_NAME + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + ", " +
                        ElementDatabaseHelper.COLUMN_LIST_ORDER + ", " +
                        ElementDatabaseHelper.COLUMN_VERSION + " FROM " +
                        ElementDatabaseHelper.TABLE_NAME + " WHERE " +
                        ElementDatabaseHelper.COLUMN_ID + " = " + elementID;
                    adp.SelectCommand = cmd;

                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    DataTable table = ds.Tables[0];
                    DataRow row = table.Rows[0];
                    Element element = DataRowToElement(row);

                    ds.Dispose();
                    return element;
                }
                catch (Exception e)
                {
                    Element element = new Element(0, 0, "", 0, "", 0, 0);
                    Console.WriteLine(e.Message);
                    return element;
                }
            }
        }

        // method to convert datarow to Element
        public Element DataRowToElement(DataRow dr)
        {
            Element element = new Element(int.Parse(dr[0].ToString()),
                int.Parse(dr[1].ToString()),
                dr[2].ToString(),
                int.Parse(dr[3].ToString()),
                dr[4].ToString(),
                int.Parse(dr[5].ToString()),
                int.Parse(dr[6].ToString()));
            return element;
        }

        /**
         ** Methods to handle syncing TEMP tables with actual elements table
         **/
        
        // method to load the temp table from array from web service
        public void SyncWithTempTable(List<Element> elements)
        {
            // clear temp table
            this.DeleteTempElements();

            // loop through list array and add to elements
            foreach (Element element in elements)
            {
                this.CreateTempElement(element.ElementID, element.AppPartID, element.ElementName, 
                    element.ElementTypeInt, element.ElementLabel, element.ListOrder, element.Version);
            }
            this.Sync();
        }

        // method to add an element record to the database
        public void CreateTempElement(int elementID, int appPartID, string elementName, int elementType,
            string elementLabel, int listOrder, int version)
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO " + ElementDatabaseHelper.TEMP_TABLE_NAME + " (" +
                        ElementDatabaseHelper.COLUMN_ID + ", " +
                        ElementDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_NAME + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + ", " +
                        ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + ", " +
                        ElementDatabaseHelper.COLUMN_LIST_ORDER + ", " +
                        ElementDatabaseHelper.COLUMN_VERSION + ") VALUES (" +
                        "@" + ElementDatabaseHelper.COLUMN_ID + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_ELEMENT_NAME + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_LIST_ORDER + ", " +
                        "@" + ElementDatabaseHelper.COLUMN_VERSION + ")";
                    SqlCeParameter p_id = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ID, SqlDbType.Int);
                    p_id.Value = elementID;
                    SqlCeParameter p_appID = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_APP_PART_ID, SqlDbType.Int);
                    p_appID.Value = appPartID;
                    SqlCeParameter p_elementName = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ELEMENT_NAME, SqlDbType.NVarChar);
                    p_elementName.Value = elementName;
                    SqlCeParameter p_elementType = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ELEMENT_TYPE, SqlDbType.Int);
                    p_elementType.Value = elementType;
                    SqlCeParameter p_elementLabel = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_ELEMENT_LABEL, SqlDbType.NVarChar);
                    p_elementLabel.Value = elementLabel;
                    SqlCeParameter p_listOrder = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_LIST_ORDER, SqlDbType.Int);
                    p_listOrder.Value = listOrder;
                    SqlCeParameter p_version = cmd.Parameters.Add("@" + ElementDatabaseHelper.COLUMN_VERSION, SqlDbType.Int);
                    p_version.Value = version;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // method to delete all elements in from temp table
        private void DeleteTempElements()
        {
            using (this._conn = new SqlCeConnection(this._dbHelper.DatabaseConnStr))
            {
                try
                {
                    this._conn.Open();
                    SqlCeCommand cmd = this._conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + ElementDatabaseHelper.TEMP_TABLE_NAME;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        // method to sync temp table with elements table
        private void Sync()
        {
            try
            {
                this._conn.Open();
                SqlCeCommand cmd = this._conn.CreateCommand();

                //delete records in app part table which are not in temp table
                cmd.CommandText = "DELETE FROM " + ElementDatabaseHelper.TABLE_NAME + " " +
                    "WHERE " + ElementDatabaseHelper.COLUMN_ID + " NOT IN (SELECT " + ElementDatabaseHelper.COLUMN_ID + 
                    " FROM " + ElementDatabaseHelper.TEMP_TABLE_NAME + ")";
                cmd.ExecuteNonQuery();

                // update records that exist in both tables
                cmd.CommandText = "UPDATE " + ElementDatabaseHelper.TABLE_NAME + " " +
                    "SET " + ElementDatabaseHelper.COLUMN_APP_PART_ID + " = t." + ElementOptionDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                    "SET " + ElementDatabaseHelper.COLUMN_ELEMENT_NAME + " = t." + ElementDatabaseHelper.COLUMN_ELEMENT_NAME + ", " +
                    "SET " + ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + " = t." + ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + ", " +
                    "SET " + ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + " = t." + ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + ", " +
                    "SET " + ElementDatabaseHelper.COLUMN_LIST_ORDER + " = t." + ElementDatabaseHelper.COLUMN_LIST_ORDER + ", " +
                    "SET " + ElementDatabaseHelper.COLUMN_VERSION + " = t." + ElementDatabaseHelper.COLUMN_VERSION + " FROM " +
                    ElementDatabaseHelper.TABLE_NAME + " JOIN " + ElementDatabaseHelper.TEMP_TABLE_NAME + " t ON (" +
                    ElementDatabaseHelper.TABLE_NAME + "." + ElementDatabaseHelper.COLUMN_ID + " = t." + ElementDatabaseHelper.COLUMN_ID + ")";
                cmd.ExecuteNonQuery();

                // insert records that do not exist in app parts table
                cmd.CommandText = "INSERT INTO " + ElementDatabaseHelper.TABLE_NAME + " " +
                    "SELECT " +
                    ElementDatabaseHelper.COLUMN_ID + ", " +
                    ElementDatabaseHelper.COLUMN_APP_PART_ID + ", " +
                    ElementDatabaseHelper.COLUMN_ELEMENT_NAME + ", " +
                    ElementDatabaseHelper.COLUMN_ELEMENT_TYPE + ", " +
                    ElementDatabaseHelper.COLUMN_ELEMENT_LABEL + ", " +
                    ElementDatabaseHelper.COLUMN_LIST_ORDER + ", " +
                    ElementDatabaseHelper.COLUMN_VERSION + " FROM " + ElementDatabaseHelper.TEMP_TABLE_NAME + " t " +
                    "LEFT OUTER JOIN " + ElementDatabaseHelper.TABLE_NAME + " a ON (t." + ElementDatabaseHelper.COLUMN_ID + " = " +
                    "a." + ElementDatabaseHelper.COLUMN_ID + ") " +
                    "WHERE a." + ElementDatabaseHelper.COLUMN_ID + " IS NULL";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
