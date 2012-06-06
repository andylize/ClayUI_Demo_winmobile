using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace com.netinfocentral.ClayUI
{
    class ElementOption
    {
        // define method variables
        private int _elementOptionID;
        private int _appPartID;
        private int _elementID;
        private string _value;
        private string _description;
        private int _version;

        // default constructor
        public ElementOption(int elementOptionID, int appPartID, int elementID, string value,
            string description, int version)
        {
            this._elementOptionID = elementOptionID;
            this._appPartID = appPartID;
            this._elementID = elementID;
            this._value = value;
            this._description = description;
            this._version = version;
        }

        // define properties
        public int ElementOptionID
        {
            get { return this._elementOptionID; }
        }
        public int AppPartID
        {
            get { return this._appPartID; }
        }
        public int ElementID
        {
            get { return this._elementID; }
        }
        public string Value
        {
            get { return this._value; }
        }
        public string Description
        {
            get { return this._description; }
        }
        public int Version
        {
            get { return this._version; }
        }

        public override string ToString()
        {
            return "AppPart [recordID=" + this._elementOptionID + ", appPartID=" + this._appPartID + ", elementID=" + 
                this._elementID + ", value=" + this._value + ", description=" + this._description + ", version=" + 
                this._version + "]";
        }
    }
}
