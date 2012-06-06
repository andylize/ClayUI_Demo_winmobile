using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace com.netinfocentral.ClayUI
{
    class AppPartUtils
    {
        // define member variables
        private int _applicationID;
        private string _baseUri = "";

        // define local data source
        private AppPartDataAdapter _appPartDataAdapter;

        // define local web service helpers
        private AppPartWebServiceHelper _appPartWebServiceHelper;

        // default cosntructor
        public AppPartUtils(int applicationID, string baseUri)
        {
            this._applicationID = applicationID;
            this._baseUri = baseUri;

            // instantiate data sources
            this._appPartDataAdapter = new AppPartDataAdapter(this._applicationID, this._baseUri);

            // instantiate web service helper
            this._appPartWebServiceHelper = new AppPartWebServiceHelper(this._applicationID, this._baseUri);
        }

        // method to synchronize local database with ClayUI
        public void Sync()
        {
            this._appPartDataAdapter.SyncWithTempTable(_appPartWebServiceHelper.GetAppParts());
        }

        // method to save current form data to local database
        public void SaveAppPartDataLocal(string appPartName, FlowLayoutPanel layout)
        {
            this._appPartDataAdapter.SaveAppPartData(appPartName, layout);
        }

        // method to save current app part data to web service
        public int SaveAppPartDataToWeb(int appPartID)
        {
            // return value
            int retval = 0;

            // get app part object
            AppPart appPart = this.GetAppPart(appPartID);

            // get list of datatable records
            List<DataTableRecord> records = this._appPartDataAdapter.GetAppPartData(appPart.AppPartName);

            // loop through list and post via DataTableWebService
            DataTableWebServiceHelper helper = new DataTableWebServiceHelper(this._applicationID, appPartID, this._baseUri);
            foreach (DataTableRecord record in records)
            {
                helper.SendTableData(record.Columns, record.Values);
            }
            return retval;
        }

        // method to update data table's sent to web status
        public void UpdateSentToWebStatus(int appPartID)
        {
            AppPart appPart = this.GetAppPart(appPartID);
            this._appPartDataAdapter.UpdateSentToWebStatus(appPart.AppPartName);
        }

        // method to get a list array of AppParts
        public List<AppPart> GetAppParts()
        {
            return this._appPartDataAdapter.GetAllAppParts();
        }

        // method to get a single AppPart object
        public AppPart GetAppPart(int appPartID)
        {
            return this._appPartDataAdapter.GetAppPart(appPartID);
        }
    }
}
