using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace com.netinfocentral.ClayUI
{
    class AppPartWebServiceHelper : ClayUIWebServiceHelper
    {
        // define member variables
        private const string SERVICE_URI = "services/GetAppParts.php?AppID=";
        
        // default constructor
        public AppPartWebServiceHelper(int applicationID, string uri)
            : base(applicationID, uri + AppPartWebServiceHelper.SERVICE_URI){}

        // method to return an arraylist of app parts from JSON delivered by ClayUI web service
        public List<AppPart> GetAppParts()
        {
            List<AppPart> appParts = new List<AppPart>();

            try
            {
                JArray array = JArray.Parse(GetWebServiceData(this._uri));

                // loop through array, pull JSON objects out and push the values into App Part objects
                for (int i = 0; i < array.Count; i++)
                {
                    JObject jObject = JObject.Parse(array[i].ToString());
                    AppPart appPart = new AppPart(int.Parse(jObject["AppPartID"].ToString()),
                        jObject["AppPartName"].ToString(),
                        int.Parse(jObject["Version"].ToString()));

                    // add to arraylist
                    appParts.Add(appPart);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return appParts;
        }
    }
}
