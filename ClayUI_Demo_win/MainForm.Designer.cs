namespace com.netinfocentral.ClayUI_Demo_win
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.dataMenu = new System.Windows.Forms.MenuItem();
            this.demoTab = new System.Windows.Forms.TabControl();
            this.contactsTab = new System.Windows.Forms.TabPage();
            this.productsTab = new System.Windows.Forms.TabPage();
            this.contactsPanel = new com.netinfocentral.ClayUI.FlowLayoutPanel();
            this.productsPanel = new com.netinfocentral.ClayUI.FlowLayoutPanel();
            this.syncSchema = new System.Windows.Forms.MenuItem();
            this.syncData = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contactSaveLocal = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.saveProduct = new System.Windows.Forms.Button();
            this.demoTab.SuspendLayout();
            this.contactsTab.SuspendLayout();
            this.productsTab.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.dataMenu);
            // 
            // dataMenu
            // 
            this.dataMenu.MenuItems.Add(this.syncSchema);
            this.dataMenu.MenuItems.Add(this.syncData);
            this.dataMenu.Text = "Menu";
            this.dataMenu.Click += new System.EventHandler(this.testMenu_Click);
            // 
            // demoTab
            // 
            this.demoTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.demoTab.Controls.Add(this.contactsTab);
            this.demoTab.Controls.Add(this.productsTab);
            this.demoTab.Dock = System.Windows.Forms.DockStyle.None;
            this.demoTab.Location = new System.Drawing.Point(0, 4);
            this.demoTab.Name = "demoTab";
            this.demoTab.SelectedIndex = 0;
            this.demoTab.Size = new System.Drawing.Size(240, 262);
            this.demoTab.TabIndex = 0;
            // 
            // contactsTab
            // 
            this.contactsTab.Controls.Add(this.panel1);
            this.contactsTab.Controls.Add(this.contactsPanel);
            this.contactsTab.Location = new System.Drawing.Point(0, 0);
            this.contactsTab.Name = "contactsTab";
            this.contactsTab.Size = new System.Drawing.Size(240, 239);
            this.contactsTab.Text = "Contacts";
            // 
            // productsTab
            // 
            this.productsTab.Controls.Add(this.panel2);
            this.productsTab.Controls.Add(this.productsPanel);
            this.productsTab.Location = new System.Drawing.Point(0, 0);
            this.productsTab.Name = "productsTab";
            this.productsTab.Size = new System.Drawing.Size(240, 239);
            this.productsTab.Text = "Products";
            // 
            // contactsPanel
            // 
            this.contactsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.contactsPanel.Location = new System.Drawing.Point(0, 0);
            this.contactsPanel.Name = "contactsPanel";
            this.contactsPanel.Size = new System.Drawing.Size(240, 207);
            // 
            // productsPanel
            // 
            this.productsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.productsPanel.Location = new System.Drawing.Point(0, -4);
            this.productsPanel.Name = "productsPanel";
            this.productsPanel.Size = new System.Drawing.Size(240, 209);
            // 
            // syncSchema
            // 
            this.syncSchema.Text = "Sync Schema";
            this.syncSchema.Click += new System.EventHandler(this.syncSchema_Click);
            // 
            // syncData
            // 
            this.syncData.Text = "Sync Data";
            this.syncData.Click += new System.EventHandler(this.syncData_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.contactSaveLocal);
            this.panel1.Location = new System.Drawing.Point(3, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 26);
            // 
            // contactSaveLocal
            // 
            this.contactSaveLocal.Location = new System.Drawing.Point(50, 3);
            this.contactSaveLocal.Name = "contactSaveLocal";
            this.contactSaveLocal.Size = new System.Drawing.Size(146, 20);
            this.contactSaveLocal.TabIndex = 0;
            this.contactSaveLocal.Text = "Save Contact";
            this.contactSaveLocal.Click += new System.EventHandler(this.contactSaveLocal_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.saveProduct);
            this.panel2.Location = new System.Drawing.Point(3, 207);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(237, 29);
            // 
            // saveProduct
            // 
            this.saveProduct.Location = new System.Drawing.Point(63, 4);
            this.saveProduct.Name = "saveProduct";
            this.saveProduct.Size = new System.Drawing.Size(110, 20);
            this.saveProduct.TabIndex = 0;
            this.saveProduct.Text = "Save Product";
            this.saveProduct.Click += new System.EventHandler(this.saveProduct_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.demoTab);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "ClayUI Demo";
            this.demoTab.ResumeLayout(false);
            this.contactsTab.ResumeLayout(false);
            this.productsTab.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl demoTab;
        private System.Windows.Forms.TabPage contactsTab;
        private System.Windows.Forms.TabPage productsTab;
        private com.netinfocentral.ClayUI.FlowLayoutPanel contactsPanel;
        private System.Windows.Forms.MenuItem dataMenu;
        private com.netinfocentral.ClayUI.FlowLayoutPanel productsPanel;
        private System.Windows.Forms.MenuItem syncSchema;
        private System.Windows.Forms.MenuItem syncData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button contactSaveLocal;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button saveProduct;
    }
}

