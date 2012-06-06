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
            this.testMenu = new System.Windows.Forms.MenuItem();
            this.demoTab = new System.Windows.Forms.TabControl();
            this.contactsTab = new System.Windows.Forms.TabPage();
            this.productsTab = new System.Windows.Forms.TabPage();
            this.contactsPanel = new com.netinfocentral.ClayUI.FlowLayoutPanel();
            this.demoTab.SuspendLayout();
            this.contactsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.testMenu);
            // 
            // testMenu
            // 
            this.testMenu.Text = "Test";
            this.testMenu.Click += new System.EventHandler(this.testMenu_Click);
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
            this.contactsTab.Controls.Add(this.contactsPanel);
            this.contactsTab.Location = new System.Drawing.Point(0, 0);
            this.contactsTab.Name = "contactsTab";
            this.contactsTab.Size = new System.Drawing.Size(240, 239);
            this.contactsTab.Text = "Contacts";
            // 
            // productsTab
            // 
            this.productsTab.Location = new System.Drawing.Point(0, 0);
            this.productsTab.Name = "productsTab";
            this.productsTab.Size = new System.Drawing.Size(232, 236);
            this.productsTab.Text = "Products";
            // 
            // contactsPanel
            // 
            this.contactsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactsPanel.Location = new System.Drawing.Point(0, 0);
            this.contactsPanel.Name = "contactsPanel";
            this.contactsPanel.Size = new System.Drawing.Size(240, 239);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl demoTab;
        private System.Windows.Forms.TabPage contactsTab;
        private System.Windows.Forms.TabPage productsTab;
        private com.netinfocentral.ClayUI.FlowLayoutPanel contactsPanel;
        private System.Windows.Forms.MenuItem testMenu;
    }
}

