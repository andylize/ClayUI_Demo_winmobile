using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace com.netinfocentral.ClayUI
{
    class ElementOptionWebServiceHelper : ClayUIWebServiceHelper
    {
        // define member variables
        private const string SERVICE_URI = "services/GetElementOptions.php?AppID=";

        //default constructor
        public ElementOptionWebServiceHelper(int applicationID, string Uri)
            : base(applicationID, Uri + ElementOptionWebServiceHelper.SERVICE_URI) {}

        // method to return arraylist of Element Options from JSON delivered by ClayUI Web Service
        public List<ElementOption> GetElementOptions()
        {
            List<ElementOption> options = new List<ElementOption>();

            try
            {
                JArray array = JArray.Parse(GetWebServiceData(this._uri));

                // loop through array, pull JSON objects out and push the values into App Part objects
                for (int i = 0; i < array.Count; i++)
                {
                    JObject jObject = JObject.Parse(array[i].ToString());
                    ElementOption option = new ElementOption(int.Parse(jObject["ElementOptionID"].ToString()),
                        int.Parse(jObject["AppPartID"].ToString()),
                        int.Parse(jObject["ElementID"].ToString()),
                        jObject["Value"].ToString(),
                        jObject["Description"].ToString(),
                        int.Parse(jObject["Version"].ToString()));

                    // add to arraylist
                    options.Add(option);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return options;
        }
    }
}
