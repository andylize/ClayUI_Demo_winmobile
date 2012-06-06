using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace com.netinfocentral.ClayUI
{
    public partial class FlowLayoutPanel : Panel
    {
        public FlowLayoutPanel() : base()
        {
            InitializeComponent();
        }

        // override/overload parent OnPaint method
        protected override void OnPaint(PaintEventArgs e)
        {
            int nextTop = 0;
            int nextLeft = 0;
            int maxHeight = 0;
            int parentWidth;

            if (this.Parent != null)
            {
                parentWidth = this.Parent.Width;
            }
            else
            {
                parentWidth = this.Width;
            }

            // loop through controls and adjust layout values 
            foreach (Control control in this.Controls)
            {
                if (control.Visible == true) // ignore invisible controls
                {
                    if ((nextLeft + control.Width) > parentWidth)
                    {
                        nextTop += maxHeight;
                        nextLeft = 0;
                        // reset max height
                        maxHeight = 0;
                    }
                    control.Top = nextTop;
                    control.Left = nextLeft;

                    if (control.Height > maxHeight)
                    {
                        maxHeight = control.Height;
                    }
                    nextLeft += control.Width;
                }
            }
            this.AutoScrollPosition = new System.Drawing.Point(0, 0);
            base.OnPaint(e);
        }
    }
}
