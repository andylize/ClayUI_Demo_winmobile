using System;
using System.Data.SqlServerCe;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace com.netinfocentral.ClayUI
{
    class ElementOptionDatabaseHelper
    {
        // define class vaiables
        private const int DATABASE_VERSION = 1;
        private const string DATABASE_NAME = "ClayUI.db";
        public const string TABLE_NAME = "ElementOptions";
        public const string TEMP_TABLE_NAME = "TEMP_ElementOptions";
        private string _dbPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().GetName().CodeBase), DATABASE_NAME);
        
        // column definitions
        public const string COLUMN_ID = "_id";
        public const string COLUMN_APP_PART_ID = "AppPartID";
        public const string COLUMN_ELEMENT_ID = "ElementID";
        public const string COLUMN_VALUE = "Value";
        public const string COLUMN_DESCRIPTION = "Description";
        public const string COLUMN_VERSION = "Version";
        
        // commands to create the tables
        public const string TABLE_CREATE =
	        "CREATE TABLE " + TABLE_NAME + " (" +
                COLUMN_ID + " int NOT NULL primary key, " +
		        COLUMN_APP_PART_ID + " int, " +
		        COLUMN_ELEMENT_ID + " int, " +
		        COLUMN_VALUE + " nvarchar(255), " + 
		        COLUMN_DESCRIPTION + " nvarchar(255), " + 
		        COLUMN_VERSION + " int);";
        public const string TEMP_TABLE_CREATE =
	        "CREATE TABLE " + TEMP_TABLE_NAME + " (" +
                COLUMN_ID + " int NOT NULL primary key, " +
		        COLUMN_APP_PART_ID + " int, " +
		        COLUMN_ELEMENT_ID + " int, " +
		        COLUMN_VALUE + " nvarchar(255), " + 
		        COLUMN_DESCRIPTION + " nvarchar(255), " + 
		        COLUMN_VERSION + " int);";
        
        // commands to delete the tables
        public const string TABLE_DELETE = 
	        "DROP TABLE IF EXISTS " + TABLE_NAME + ";";
        public const string TEMP_TABLE_DELETE = 
	        "DROP TABLE IF EXISTS " + TEMP_TABLE_NAME + ";";

        // default constructor
        public ElementOptionDatabaseHelper() { }

        public string DatabaseConnStr
        {
            get
            {
                return @"Data Source = " + this._dbPath;
            }
        }
    }
}
