using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace com.netinfocentral.ClayUI
{
    public enum ElementType : int
    {
        CLAYUI_TEXTBOX,
        CLAYUI_LABEL,
        CLAYUI_COMBOBOX,
        CLAYUI_RADIOBUTTON,
        CLAYUI_CHECKBOX
    }

    public class Element
    {
        // define member variables
        private int _elementID;
        private int _appPartID;
        private string _elementName;
        private int _elementType;
        private string _elementLabel;
        private int _listOrder;
        private int _version;
        private List<string> _elementOptions;

        // default constructor
        public Element(int elementID, int appPartID, string elementName, int elementType, string elementLabel, int listOrder, int version)
        {
            this._elementID = elementID;
            this._appPartID = appPartID;
            this._elementName = elementName;
            this._elementType = elementType;
            this._elementLabel = elementLabel;
            this._listOrder = listOrder;
            this._version = version;
            this._elementOptions = new List<string>();
        }

        // define properties
        public int ElementID
        {
            get { return this._elementID; }
        }
        public int AppPartID
        {
            get { return this._appPartID; }
        }
        public string ElementName
        {
            get { return this._elementName; }
        }
        public ElementType ElementType
        {
            get { return (ElementType)this._elementType; }
        }
        public int ElementTypeInt
        {
            get { return this._elementType; }
        }
        public string ElementLabel
        {
            get { return this._elementLabel; }
        }
        public int ListOrder
        {
            get { return this._listOrder; }
        }
        public int Version
        {
            get { return this._version; }
        }
        public List<string> ElementOptions
        {
            get
            {
                if (this._elementOptions == null || this._elementOptions.Count() == 0)
                {
                    this._elementOptions.Add("Empty Option");
                    return this._elementOptions;
                }
                else
                {
                    return this._elementOptions;
                }
            }
        }
        public bool HasOptions
        {
            get
            {
                if (this._elementOptions == null || this._elementOptions.Count() == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        // methods
        public void FetchElementOptions()
        {
            //TODO Add copde
        }

        public override string ToString()
        {
            return "Element [recordID=" + this._elementID + ", appPartID=" + this._appPartID + ", elementName=" + this._elementName +
                    ", elementType=" + this._elementType + ", elementLabel=" + this._elementLabel + ", listOrder=" + this._listOrder + 
                    ", version=" + this._version + "]";
        } 
    }
}
