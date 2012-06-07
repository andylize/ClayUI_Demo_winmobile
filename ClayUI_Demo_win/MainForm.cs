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

            // instantiate app parts
            contactsAppPart = appBase.GetAppPart(1);
            productsAppPart = appBase.GetAppPart(2);

            // fetch app part elements
            contactsAppPart.FetchElements();
            contactsAppPart.RefreshPanel(this.contactsPanel);
            productsAppPart.FetchElements();
            productsAppPart.RefreshPanel(this.productsPanel);
        }

        private void testMenu_Click(object sender, EventArgs e)
        {

        }

        private void saveProduct_Click(object sender, EventArgs e)
        {
            this.appBase.SaveAppPartDataLocal(productsAppPart, productsPanel);
        }

        private void contactSaveLocal_Click(object sender, EventArgs e)
        {
            this.appBase.SaveAppPartDataLocal(contactsAppPart, contactsPanel);
        }

        private void syncSchema_Click(object sender, EventArgs e)
        {

            Cursor oldcursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            appBase.SyncLayoutStructure();
            contactsAppPart.FetchElements();
            contactsAppPart.RefreshPanel(contactsPanel);
            productsAppPart.FetchElements();
            productsAppPart.RefreshPanel(productsPanel);

            Cursor.Current = oldcursor;

            MessageBox.Show("Layout updated.", "Update",
                MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        private void syncData_Click(object sender, EventArgs e)
        {
            appBase.SaveAppPartDataWeb(contactsAppPart);
            appBase.SaveAppPartDataWeb(productsAppPart);
        }
    }
}