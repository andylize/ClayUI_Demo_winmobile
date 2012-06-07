using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace com.netinfocentral.ClayUI
{
    // class to help create/update the data table for an app part
    class DataTableDatabaseHelper
    {
        // define member variables
        private const int DATABASE_VERSION = 1;
        private const string DATABASE_NAME = "ClayUI.sdf";
        private int _applicationID;
        private int _appPartID;
        private string _Uri;
        private List<DataTableSchema> _webSchema;
        private DataTableWebServiceHelper _webServiceHelper;
        private string _tableCreate;
        private string _tableDelete;

        // default constructor
        public DataTableDatabaseHelper(int applicationID, int appPartID, string uri)
        {
            this._applicationID = applicationID;
            this._appPartID = appPartID;
            this._Uri = uri;
            this._webServiceHelper = new DataTableWebServiceHelper(this._applicationID, this._appPartID, this._Uri);
            this._webSchema = this._webServiceHelper.GetTableSchema();
            
        }

        // method to dynamically generate create table statement
        private void SetCreateStatement()
        {
            AppPartDataAdapter adapter = new AppPartDataAdapter(this._applicationID, this._Uri);
            AppPart appPart = adapter.GetAppPart(this._appPartID);

            foreach (DataTableSchema schema in this._webSchema)
            {
                this._tableCreate = this._tableCreate + "'" + schema.ColumnName + "' ";

                if (schema.DataTaype.Equals("int"))
                {
                    this._tableCreate = this._tableCreate + "int ";
                }
                else if (schema.DataTaype.Equals("decimal"))
                {
                    this._tableCreate = this._tableCreate + "numeric ";
                }
                else
                {
                    this._tableCreate = this._tableCreate + "nvarchar(255) ";
                }

                if (schema.IsPrimaryKey)
                {
                    this._tableCreate = this._tableCreate + "primary key";
                }
                this._tableCreate = this._tableCreate + ", ";
            }

            // add column for storing web-save status
            this._tableCreate = this._tableCreate + "sentToWeb int default 0);";
        }

        // method to return table create statement
        public string GetTableCreate()
        {
            return this._tableCreate;
        }

        // method to dynamically generate a drop table statement
        private void SetDeleteStatement()
        {
            AppPartDataAdapter adapter = new AppPartDataAdapter(this._applicationID, this._Uri);
            AppPart appPart = adapter.GetAppPart(this._applicationID);
            this._tableDelete = "DROP TABLE IF EXISTS " + appPart.AppPartName;
        }

        // method to return table delet statement
        private string GetTableDelete()
        {
            return this._tableDelete;
        }

        // method to check if a table exists in SQLCE
        private bool TableExists(int appPartID)
        {
            AppPartDataAdapter adapter = new AppPartDataAdapter(this._applicationID, this._Uri);
            AppPart appPart = adapter.GetAppPart(appPartID);
            return adapter.DataTableExists(appPart.AppPartName);
        }
    }
}
