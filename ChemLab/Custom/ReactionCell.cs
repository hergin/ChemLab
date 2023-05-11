using ChemLab.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemLab.Custom
{
    [Serializable]
    public class ReactionCell : RectangleElement
    {
        public ReactionCell(int x, int y):this(x,y,600,100)
        {
        }

        public ReactionCell(int x, int y, int w, int h) : base(x, y, w, h, true)
        {
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
            BorderColor = Color.Black;
            BorderWidth = 2;

            Label = new LabelElement(x, y, w, h);
            //Label.Alignment = StringAlignment.Far;
            //Label.LineAlignment = StringAlignment.Near;
            label.Font = new Font("Trebuchet MS", h / 2);
            label.ForeColor1 = Color.Blue;
            Label.Text = "→";
        }

        public void Highlight()
        {
            BorderColor = Color.Yellow;
            BorderWidth = 4;
        }

        public void Unhighlight()
        {
            BorderColor = Color.Black;
            BorderWidth = 2;
        }

        public void ResizeToOriginal()
        {
            this.Size = new Size(600, 100);
            Label.Size = new Size(600, 100);
            label.Font = new Font("Trebuchet MS", 50);
            OnAppearanceChanged(new EventArgs());
        }
    }
}
