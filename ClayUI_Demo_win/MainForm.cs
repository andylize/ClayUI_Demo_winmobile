using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.netinfocentral.ClayUI;

namespace com.netinfocentral.ClayUI_Demo_win
{
    public partial class MainForm : Form
    {
        ClayUIAppBase appBase;
        AppPart contactsAppPart;
        AppPart productsAppPart;

        const string BASE_URI_LOCAL = "http://192.168.102.80/ClayUI/";
        const string BASE_URI_INTERNET = "http://nicentral.dyndns.org:8888/ClayUI/";

        public MainForm()
        {
            InitializeComponent();
            
            // instantiate ClayUI AppBase
            appBase = new ClayUIAppBase(1, BASE_URI_INTERNET);

        }

        private void testMenu_Click(object sender, EventArgs e)
        {
            
        }
    }
}