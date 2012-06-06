using System;
using System.Data.SqlServerCe;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace com.netinfocentral.ClayUI
{
    class ElementDatabaseHelper
    {
        // define member variables
        private const string DATABASE_NAME = "ClayUI.sdf";
        private const int DATABASE_VERSION = 1;
        public const string TABLE_NAME = "Elements";
        public const string TEMP_TABLE_NAME = "TEMP_Elements"; 
        private string _dbPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().GetName().CodeBase), DATABASE_NAME);
        

        // column definitions
  	    public const string COLUMN_ID = "_id";
  	    public const string COLUMN_APP_PART_ID = "AppPartID";
  	    public const string COLUMN_ELEMENT_NAME = "ElementName";
  	    public const string COLUMN_ELEMENT_TYPE = "ElementType";
  	    public const string COLUMN_ELEMENT_LABEL = "ElementLabel";
  	    public const string COLUMN_LIST_ORDER = "ListOrder";
  	    public const string COLUMN_VERSION = "Version";

  	    // command to create the table
  	    public const string TABLE_CREATE =
  		    "CREATE TABLE " + TABLE_NAME + " (" +
  			    COLUMN_ID + " int primary key, " +
  			    COLUMN_APP_PART_ID + " int, " +
  			    COLUMN_ELEMENT_NAME + " nvarchar(255), " +
  			    COLUMN_ELEMENT_TYPE + " int, " +
  			    COLUMN_ELEMENT_LABEL + " nvarchar(255), " +
  			    COLUMN_LIST_ORDER + " int, " +
  			    COLUMN_VERSION + " int);";
  	    public const string TEMP_TABLE_CREATE =
  		    "CREATE TABLE " + TEMP_TABLE_NAME + " (" +
  			    COLUMN_ID + " int primary key, " +
  			    COLUMN_APP_PART_ID + " int, " +
  			    COLUMN_ELEMENT_NAME + " nvarchar(255), " +
  			    COLUMN_ELEMENT_TYPE + " int, " +
  			    COLUMN_ELEMENT_LABEL + " nvarchar(255), " +
  			    COLUMN_LIST_ORDER + " int, " +
  			    COLUMN_VERSION + " int);";

  	    // commands to delete the tables
  	    public const string TABLE_DELETE = 
  		    "DROP TABLE IF EXISTS " + TABLE_NAME + ";";
  	    public const string TEMP_TABLE_DELETE =
  		    "DROP TABLE IF EXISTS " + TEMP_TABLE_NAME + ";";

        // default contstructor
        public ElementDatabaseHelper() { }

        public string DatabaseConnStr
        {
            get
            {
                return @"Data Source = " + this._dbPath;
            }
        }
    }
}
