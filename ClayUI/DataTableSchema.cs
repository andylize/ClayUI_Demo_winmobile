using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace com.netinfocentral.ClayUI
{
    class DataTableSchema
    {
        // define member variables
        private string _columnName;
        private string _dataType;
        private int _length;
        private bool _isPrimaryKey;

        // define default constructor
        public DataTableSchema(string columnName, string dataType, int length, int isPrimaryKey)
        {
            this._columnName = columnName;
            this._dataType = dataType;
            this._length = length;

            if (Math.Abs(isPrimaryKey) == 1)
            {
                this._isPrimaryKey = true;
            }
            else
            {
                this._isPrimaryKey = false;
            }
        }

        // define properties
        public string ColumnName
        {
            get
            {
                return this._columnName;
            }
        }
        public string DataTaype
        {
            get
            {
                return this._dataType;
            }
        }
        public int Length
        {
            get
            {
                return this._length;
            }
        }
        public bool IsPrimaryKey
        {
            get
            {
                return this._isPrimaryKey;
            }
        }

        public override string ToString()
        {
            return "DataTableSchema [columnName=" + this._columnName + ", dataType=" + this._dataType + ", length=" + this._length + ", isPrimaryKey=" + this._isPrimaryKey + "]";
        }
    }


}
