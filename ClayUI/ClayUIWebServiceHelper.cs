using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;


namespace com.netinfocentral.ClayUI
{
    public abstract class ClayUIWebServiceHelper
    {
        // define member variables
        protected int _applicationID;
        protected string _uri;
        protected string _jsonResult;


        // define default constructor
        public ClayUIWebServiceHelper(int applicationID, string uri)
        {
            this._applicationID = applicationID;
            this._uri = uri + this._applicationID;
        }

        // generic web service retrieval to return a JSON formatted string
        protected string GetWebServiceData(string uri)
        {
            string jsonString = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(uri);
                WebResponse response = request.GetResponse();

                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    jsonString = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return jsonString;
        }
    }
}
