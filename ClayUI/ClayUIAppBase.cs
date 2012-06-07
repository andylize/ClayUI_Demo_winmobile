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
        private ClayUIDatabaseHelper _dbHelper;
        private AppPartUtils _appPartUtils;
        private ElementUtils _elementUtils;
        private ElementOptionUtils _elementOptionUtils;
        
        

        // define default constructor
        public ClayUIAppBase(int applicationID, string baseURI)
        {
            this._applicationID = applicationID;
            this._baseUri = baseURI;

            // create database if necessary and get connection string
            _dbHelper = new ClayUIDatabaseHelper(this._applicationID, this._baseUri);
            this._connStr = _dbHelper.DatabaseConnStr;

            // instantiate data util objects
            _appPartUtils = new AppPartUtils(this._applicationID, this._baseUri);
            _elementUtils = new ElementUtils(this._applicationID, this._baseUri);
            _elementOptionUtils = new ElementOptionUtils(this._applicationID, this._baseUri);

            // load app part data
            this.SyncLayoutStructure();
        }

        // method to sync ClayUI structure
        public void SyncLayoutStructure()
        {
            this._appPartUtils.Sync();
            this._elementUtils.Sync();
            this._elementOptionUtils.Sync();
        }

        // method to return all app parts
        public List<AppPart> GetAppParts()
        {
            return this._appPartUtils.GetAppParts();
        }
        // method to get an app part
        public AppPart GetAppPart(int appPartID)
        {
            return this._appPartUtils.GetAppPart(appPartID);
        }
        // method to return all elements
        public List<Element> Elements()
        {
            return this._elementUtils.GetElements();
        }

        // method to save the app part data
        public void SaveAppPartDataLocal(AppPart appPart, FlowLayoutPanel layout)
        {
            this._appPartUtils.SaveAppPartDataLocal(appPart.AppPartName, layout);
        }

        // method to save app part data to web service
        public int SaveAppPartDataWeb(AppPart appPart)
        {
            int retval = 0;

            if (this._appPartUtils.SaveAppPartDataToWeb(appPart.AppPartID) == 0)
            {
                this._appPartUtils.UpdateSentToWebStatus(appPart.AppPartID);
            }
            else
            {
                retval = 1;
            }

            if (retval == 1)
            {
                System.Windows.Forms.MessageBox.Show("Error sending data to web service.",
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Exclamation,
                    System.Windows.Forms.MessageBoxDefaultButton.Button1);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Data sent to web successfully.",
                    "Success",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.None,
                    System.Windows.Forms.MessageBoxDefaultButton.Button1);
            }
            return retval;
        }
    }
}
