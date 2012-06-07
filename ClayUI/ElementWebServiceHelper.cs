using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace com.netinfocentral.ClayUI
{
    class ElementWebServiceHelper : ClayUIWebServiceHelper
    {
        // define member variables
        private const string SERVICE_URI = "services/GetElements.php?AppID=";

        // default constructor
        public ElementWebServiceHelper(int applicationID, string uri)
            : base(applicationID, uri + ElementWebServiceHelper.SERVICE_URI){}

        // return arraylist of Elements from JSON delivered by ClayUI Web Service
        public List<Element> GetElements()
        {
            List<Element> elements = new List<Element>();

            try
            {
                JArray array = JArray.Parse(GetWebServiceData(this._uri));

                // loop through array, pull JSON objects out and push the values into App Part objects
                for (int i = 0; i < array.Count; i++)
                {
                    JObject jObject = JObject.Parse(array[i].ToString());
                    Element element = new Element(int.Parse(jObject["ElementID"].ToString().Replace("\"", "")),
                        int.Parse(jObject["AppPartID"].ToString().Replace("\"", "")),
                        jObject["ElementName"].ToString().Replace("\"", ""),
                        int.Parse(jObject["ElementType"].ToString().Replace("\"", "")),
                        jObject["Label"].ToString().Replace("\"", ""),
                        int.Parse(jObject["ListOrder"].ToString().Replace("\"", "")),
                        int.Parse(jObject["Version"].ToString().Replace("\"", "")));

                    // add to arraylist
                    elements.Add(element);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return elements;
        }
    }
}
