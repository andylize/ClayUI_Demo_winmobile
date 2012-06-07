using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace com.netinfocentral.ClayUI
{
    public class AppPart
    {
        // define member variables
        private int _appPartID;
        private string _appPartName;
        private int _version;
        private List<Element> _elements;

        // default constructor
        public AppPart(int appPartID, string appPartName, int version)
        {
            this._appPartID = appPartID;
            this._appPartName = appPartName;
            this._version = version;
        }

        // define properties
        public int AppPartID
        {
            get
            {
                return this._appPartID;
            }
        }
        public string AppPartName
        {
            get
            {
                return this._appPartName;
            }
        }
        public int Version
        {
            get
            {
                return this._version;
            }
        }
        public List<Element> Elements
        {
            get { return this._elements; }
        }
        public int ElementCount
        {
            get
            {
                if (this._elements == null)
                {
                    return 0;
                }
                else
                {
                    return this._elements.Count;
                }
            }
        }

        /**
         * Method to add an element
         **/
        public void AddElement(Element element)
        {
            this._elements.Add(element);
        }
        /**
         * Method to bulk add elements from listarray
         **/
        public void AddElements(List<Element> elements)
        {
            foreach (Element element in elements)
            {
                this.AddElement(element);
            }
        }
        /**
         * Method to clear elements from list array
         **/
        public void ClearElements()
        {
            this._elements.Clear();
        }
        /**
         * Method to fetch elements for this app part from the local database
         **/
        public void FetchElements()
        {
            ElementDataAdapter adapter = new ElementDataAdapter();
            this._elements = adapter.GetAllElements(this._appPartID);
        }

        /**
         * Method to refresh container
         **/
        public void RefreshPanel(FlowLayoutPanel panel)
        {
            // clear existing panel itmes
            panel.Controls.Clear();

            // loop through elements and push to panel
            if (this._elements.Count() > 0)
            {
                foreach (Element element in this._elements)
                {
                    switch (element.ElementType)
                    {
                        case ElementType.CLAYUI_TEXTBOX:
                            panel.Controls.Add(this.CreateLabelUIElement(element));
                            panel.Controls.Add(this.CreateTextBoxUIElement(element, element.ElementID));
                            break;
                        case ElementType.CLAYUI_LABEL:
                            panel.Controls.Add(this.CreateLabelUIElement(element, element.ElementID));
                            break;
                        case ElementType.CLAYUI_COMBOBOX: 
                            panel.Controls.Add(this.CreateLabelUIElement(element));
                            panel.Controls.Add(this.CreateComboBoxUIElement(element, element.ElementID));
                            break;
                        case ElementType.CLAYUI_RADIOBUTTON:
                            panel.Controls.Add(this.CreateLabelUIElement(element));
                            FlowLayoutPanel radioPanel = this.CreateRadioGroupUIElement(element, element.ElementID);
                            radioPanel.Width = panel.Width;
                            panel.Controls.Add(radioPanel);
                            break;
                        case ElementType.CLAYUI_CHECKBOX:
                            panel.Controls.Add(this.CreateCheckBoxUIElement(element, element.ElementID));
                            break;
                    }
                }
            }
            else 
            {
                System.Console.WriteLine("Attempt to refresh layout with 0 elements.  Did you forget to fetch elements?");
            }
        }

        /**
         * Method to create label with ID
         **/
        private Label CreateLabelUIElement(Element element, int viewID)
        {
            Label label = new Label();
            label.Name = viewID.ToString();
            label.Text = element.ElementLabel;
            return label;
        }
        /**
         * Method to create a label without an ID
         **/
        private Label CreateLabelUIElement(Element element)
        {
            Label label = new Label();
            label.Text = element.ElementLabel;
            return label;
        }
        /**
         * Method to create text box
         **/
        private TextBox CreateTextBoxUIElement(Element element, int viewID)
        {
            TextBox textBox = new TextBox();
            textBox.Name = viewID.ToString();
            return textBox;
        }
        /**
         * Method to create a combo box element
         **/
        private ComboBox CreateComboBoxUIElement(Element element, int viewID)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Name = viewID.ToString();


            // fetch element options
            if (element.HasOptions == false)
            {
                element.FetchElementOptions();
            }

            foreach (string option in element.ElementOptions)
            {
                comboBox.Items.Add(option);
            }
            return comboBox;
        }
        /**
         * Method to create a radio button group with radio buttons.  We'll use the FlowLayout Panel to group the radio buttons
         **/
        private FlowLayoutPanel CreateRadioGroupUIElement(Element element, int viewID)
        {
            FlowLayoutPanel panel = new FlowLayoutPanel();
           
            // fetch element options for each radio button
            if (element.HasOptions == false)
            {
                element.FetchElementOptions();
            }

            // apply element options
            foreach (string option in element.ElementOptions)
            {
                RadioButton rb = new RadioButton();
                rb.Text = option.ToString();
                panel.Controls.Add(rb);
            }
            return panel;
        }
        /**
         * Method to create checkbox
         **/
        private CheckBox CreateCheckBoxUIElement(Element element, int viewID)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Name = viewID.ToString();
            checkBox.Text = element.ElementLabel;
            return checkBox;
        }

        public override string ToString()
        {
            return "AppPart [recordID=" + this._appPartID + ", appPartName=" + this._appPartName + ", version=" + this._version + "]";
        }
    }
}
