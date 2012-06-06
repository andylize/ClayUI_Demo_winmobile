using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace com.netinfocentral.ClayUI
{
    public class ClayUIAppBase
    {
        
        //Define member variables
        int _applicationID;
        string _applicationName;
        string _baseUri = "";
        string _connStr;

        // define local data sources
        private ClayUIDatabaseHelper dbHelper;
        private AppPartUtils appPartUtils;
        private ElementUtils elementUtils;
        
        

        // define default constructor
        public ClayUIAppBase(int applicationID, string baseURI)
        {
            this._applicationID = applicationID;
            this._baseUri = baseURI;

            // create database if necessary and get connection string
            dbHelper = new ClayUIDatabaseHelper(this._applicationID, this._baseUri);
            this._connStr = dbHelper.DatabaseConnStr;

        }
    }
}
