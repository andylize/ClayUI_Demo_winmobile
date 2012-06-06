namespace com.netinfocentral.ClayUI
{
    class AppPartDatabaseHelper
    {
        // define member variables
        private const int DATABASE_VERSION = 1;
        private const string DATABASE_NAME = "ClayUI.sdf";
        public const string TABLE_NAME = "AppParts";
        public const string TEMP_TABLE_NAME = "TEMP_AppParts";

        // column definitions
        public const string COLUMN_ID = "_id";
        public const string COLUMN_APP_PART_NAME = "AppPartName";
        public const string COLUMN_VERSION = "Version";

        // DDL to create the tables
        public const string TABLE_CREATE =
            "CREATE TABLE " + TABLE_NAME + " (" +
            COLUMN_ID + " int NOT NULL primary key, " +
            COLUMN_APP_PART_NAME + " nvarchar(255), " +
            COLUMN_VERSION + " int);";
        public const string TEMP_TABLE_CREATE =
            "CREATE TABLE " + TEMP_TABLE_NAME + " (" +
            COLUMN_ID + " int NOT NULL primary key, " +
            COLUMN_APP_PART_NAME + " nvarchar(255), " +
            COLUMN_VERSION + " int);";

        // DDL to delete the tables
        public const string TABLE_DELETE =
	    "DROP TABLE IF EXISTS " + TABLE_NAME + ";";
        public const string TEMP_TABLE_DELETE =
            "DROP TABLE IF EXISTS " + TEMP_TABLE_NAME + ";";

        // default constructor
        public AppPartDatabaseHelper()
        {
        }

    }
}
