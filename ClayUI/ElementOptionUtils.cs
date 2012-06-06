using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace com.netinfocentral.ClayUI
{
    class ElementOptionUtils
    {
        // define memeber variables
        private int _applicationID;
        private string _baseUri;

        // define local data sources
        private ElementOptionDataAdapter _elementOptionDataAdapter;

        // define local web service helper
        private ElementOptionWebServiceHelper _elementOptionWebServiceHelper;

        // default constructor
        public ElementOptionUtils(int applicationID, string baseUri)
        {
            this._applicationID = applicationID;
            this._baseUri = baseUri;

            // instantiate data and web sources
            _elementOptionDataAdapter = new ElementOptionDataAdapter();
            _elementOptionWebServiceHelper = new ElementOptionWebServiceHelper(this._applicationID, this._baseUri);
        }
        
        // method to sync local database with ClayUI
        public void Sync()
        {
            this._elementOptionDataAdapter.SyncWithTempTable(this._elementOptionWebServiceHelper.GetElementOptions());
        }

        // method to return a list array of element options
        public List<ElementOption> GetElementOptions()
        {
            return this._elementOptionDataAdapter.GetAllElementOptions();
        }
    }
}
