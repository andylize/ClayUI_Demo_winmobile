using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;

namespace com.netinfocentral.ClayUI
{
    class DataTableWebServiceHelper : ClayUIWebServiceHelper
    {
        // define member variables
        private int _appPartID;
        private const string SERVICE_URI = "services/GetDataTableSchema.php?AppID=";
        private const string SERVICE_URI_2 = "&AppPartID=";
        private const string POST_URI = "services/PutTableData.php?AppID=";
        private string _postUri = "";

        // default constructor
        public DataTableWebServiceHelper(int applicationID, int appPartID, string uri)
            : base(applicationID, uri + DataTableWebServiceHelper.SERVICE_URI)
        {
            this._appPartID = appPartID;
            this._uri = this._uri + DataTableWebServiceHelper.SERVICE_URI_2 + this._appPartID;
            this._postUri = this._uri + DataTableWebServiceHelper.POST_URI + this._applicationID +
                DataTableWebServiceHelper.SERVICE_URI_2 + this._appPartID;
        }

        // method to return an arraylist of a data table schema from a JSON delivered from ClayUI Web Service
        public List<DataTableSchema> GetTableSchema()
        {
            List<DataTableSchema> tableSchema = new List<DataTableSchema>();

            try
            {
                JArray array = JArray.Parse(GetWebServiceData(this._uri));

                // loop through array, pull JSON objects out and push the values into App Part objects
                for (int i = 0; i < array.Count; i++)
                {
                    JObject jObject = JObject.Parse(array[i].ToString());
                    DataTableSchema schema = new DataTableSchema(jObject["column_name"].ToString(),
                        jObject["data_type"].ToString(),
                        int.Parse(jObject["length"].ToString()),
                        int.Parse(jObject["is_primary_key"].ToString()));

                    // add to arraylist
                    tableSchema.Add(schema);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return tableSchema;
        }

        // generic data table post method
        public int SendTableData(List<String> columns, List<String> values)
        {
            // return values
            int retval = 0;

            // create string from columns list array
            string columnCSV = "'";
            foreach (string column in columns)
            {
                columnCSV = columnCSV + column + ", ";
            }
            // trim trailing ", "
            columnCSV = columnCSV.Substring(0, columnCSV.Length - 2) + "'";

            // create string from values list array
            string valuesCSV = "'";
            foreach (string value in values)
            {
                valuesCSV = valuesCSV + value + "'', ";
            }
            // trim trailing "', "
            valuesCSV = valuesCSV.Substring(0, valuesCSV.Length - 2) + "'";

            // push values to JSON and post to web service
            try
            {
                WebRequest request = WebRequest.Create(this._postUri);
                request.Method = "POST";

                JObject jObject = new JObject();
                jObject.Add("appID", this._applicationID.ToString());
                jObject.Add("appPartID", this._appPartID.ToString());
                jObject.Add("columnsCSV", columnCSV);
                jObject.Add("valuesCSV", valuesCSV);
                JArray jArray = new JArray();
                jArray.Add(jObject);

                request.Headers.Add("json", jObject.ToString());
                byte[] byteArray = Encoding.UTF8.GetBytes(jArray.ToString());
                request.ContentType = "jsonpost";

                // TODO: COMPLETE SECTION

                WebResponse response = request.GetResponse();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retval;

        }
    }
}
