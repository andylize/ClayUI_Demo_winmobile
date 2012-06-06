using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace com.netinfocentral.ClayUI
{
    class ElementUtils
    {
        // define memeber variables
        private int _applicationID;
        private string _baseUri = "";

        // define local data sources
        private ElementDataAdapter _elementDataAdapter;

        // deine local web service helper
        private ElementWebServiceHelper _elementWebServiceHelper;

        // default constructor
        public ElementUtils(int applicationID, string baseUri)
        {
            this._applicationID = applicationID;
            this._baseUri = baseUri;
            
            // instantiate datasource and web source
            this._elementDataAdapter = new ElementDataAdapter();
            this._elementWebServiceHelper = new ElementWebServiceHelper(this._applicationID, this._baseUri);
        }

        // method to sync local database with ClayUI
        public void Sync()
        {
            this._elementDataAdapter.SyncWithTempTable(this._elementWebServiceHelper.GetElements());
        }

        // method to return list array of elements
        public List<Element> GetElements()
        {
            return this._elementDataAdapter.GetAllElements();
        }

        
    }
}
