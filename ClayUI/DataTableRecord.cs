using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace com.netinfocentral.ClayUI
{
    class DataTableRecord
    {
        // define member variables
        private List<string> _columns;
        private List<string> _values;

        // default constructor
        public DataTableRecord(List<string> columns, List<string> values)
        {
            this._columns = columns;
            this._values = values;
        }

        // define properties
        public List<string> Columns
        {
            get
            {
                return this._columns;
            }
        }
        public List<string> Values
        {
            get
            {
                return this._values;
            }
        }
    }
}
