using System;
using System.Data.SqlServerCe;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace com.netinfocentral.ClayUI
{
    class ClayUIDatabaseHelper
    {
        // define member variables
        private const string DATABASE_NAME = "ClayUI.sdf";
        private DatabaseHelper _dbHelper;
        private int _appID;
        private string _baseURI;
        private string _dbPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().GetName().CodeBase), DATABASE_NAME);
        

        // define AppPart DDL strings
        private const string CREATE_APP_PARTS_TABLE = AppPartDatabaseHelper.TABLE_CREATE;
        private const string CREATE_APP_PARTS_TEMP_TABLE = AppPartDatabaseHelper.TEMP_TABLE_CREATE;
        private const string DELETE_APP_PARTS_TABLE = AppPartDatabaseHelper.TABLE_DELETE;
        private const string DELETE_APP_PARTS_TEMP_TABLE = AppPartDatabaseHelper.TEMP_TABLE_DELETE;

        // define Element DDL strings
        private const string CREATE_ELEMENTS_TABLE = ElementDatabaseHelper.TABLE_CREATE;
        private const string CREATE_ELEMENTS_TEMP_TABLE = ElementDatabaseHelper.TEMP_TABLE_CREATE;
        private const string DELETE_ELEMENTS_TABLE = ElementDatabaseHelper.TABLE_DELETE;
        private const string DELETE_ELEMENTS_TEMP_TABLE = ElementDatabaseHelper.TEMP_TABLE_DELETE;
        
        // define ElementOption DDL strings
        private const string CREATE_ELEMENT_OPTIONS_TABLE = ElementOptionDatabaseHelper.TABLE_CREATE;
        private const string CREATE_ELEMENT_OPTIONS_TEMP_TABLE = ElementOptionDatabaseHelper.TEMP_TABLE_CREATE;
        private const string DELETE_ELEMENT_OPTIONS_TABLE = ElementOptionDatabaseHelper.TABLE_DELETE;
        private const string DELETE_ELEMENT_OPTIONS_TEMP_TABLE = ElementOptionDatabaseHelper.TEMP_TABLE_DELETE;

        public ClayUIDatabaseHelper(int appID, string baseURI)
        {
            this._appID = appID;
            this._baseURI = baseURI;
            this._dbHelper = new DatabaseHelper(this._appID, this._baseURI, this._dbPath);
        }

        public string DatabaseConnStr
        {
            get
            {
                return @"Data Source = " + this._dbPath;
            }
        }

        private class DatabaseHelper 
        {
            // define member variables
            private int _appID;
            private string _baseURI;
            private string _dbPath;

            // default constructor
            public DatabaseHelper(int appID, string baseURI, string dbPath)
            {
                this._appID = appID;
                this._baseURI = baseURI;
                this._dbPath = dbPath;
                this.CreateDatabase();
            }

            public void CreateDatabase()
            {
                // check if file exists if not create it
                if (System.IO.File.Exists(this._dbPath) == false)
                {
                    string connStr = @"Data Source = " + this._dbPath;

                    // create database
                    SqlCeEngine engine = new SqlCeEngine(connStr);
                    engine.CreateDatabase();

                    // create tables
                    using (SqlCeConnection conn = new SqlCeConnection(connStr))
                    {                        
                        try
                        {
                            conn.Open();
                            SqlCeCommand command = new SqlCeCommand();
                            command.Connection = conn;
                            command.CommandText = CREATE_APP_PARTS_TABLE;
                            command.ExecuteNonQuery();
                            command.CommandText = CREATE_APP_PARTS_TEMP_TABLE;
                            command.ExecuteNonQuery();
                            command.CommandText = CREATE_ELEMENTS_TABLE;
                            command.ExecuteNonQuery();
                            command.CommandText = CREATE_ELEMENTS_TEMP_TABLE;
                            command.ExecuteNonQuery();
                            command.CommandText = CREATE_ELEMENT_OPTIONS_TABLE;
                            command.ExecuteNonQuery();
                            command.CommandText = CREATE_ELEMENT_OPTIONS_TEMP_TABLE;
                            command.ExecuteNonQuery();
                            
                        }
                        catch (SqlCeException e)
                        {
                            System.Console.WriteLine(e.Message);
                        }
                    }
                }
            }
        }
    }
}
