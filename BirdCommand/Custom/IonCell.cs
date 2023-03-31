using BirdCommand.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    [Serializable]
    public class IonCell : RectangleElement
    {
        [NonSerialized]
        List<Ion> ions;

        public IonCell(int x, int y,List<Ion> ions) : base(x, y, ions.Sum(i=>i.Radius)+15, ions.Select(i=>i.Radius).Max())
        {
            this.ions = ions;
        }
       
        
        public void AddIon(Ion ion)
        {
            ions.Add(ion);
            OnAppearanceChanged(new EventArgs());
            base.size.Width = ions.Sum(x=> x.Radius) +5;
            base.size.Height = ions.Max(x=> x.Radius) +5;
        }

        public List<Ion> GetIons()
        {
            return this.ions;
        }

        internal override void Draw(Graphics g)
        {
                int xOffset = 0;
                int largestRadius = ions.Max(x=> x.Radius);
                foreach(Ion ion in ions)
                {
                Rectangle r = GetUnsignedRectangle(new Rectangle(location.X +xOffset, location.Y+(largestRadius-ion.Radius)/2, ion.Radius, ion.Radius));
                Rectangle rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
                Brush b = new LinearGradientBrush(rb, Color.FromArgb((int)(255.0f * (opacity / 100.0f)), ion.Color1), Color.FromArgb((int)(255.0f * (opacity / 100.0f)), ion.Color2), LinearGradientMode.Horizontal);
                g.FillEllipse(b, r);
                Pen p = new Pen(borderColor, borderWidth);
                g.DrawEllipse(p, r);

                g.DrawString(ion.Symbol, new Font("Trebuchet MS", 12), Brushes.Black, new PointF(Location.X + xOffset+  ion.Radius / 2 - 10, Location.Y + ion.Radius / 2 - 10));
                xOffset= xOffset + ion.Radius;
                }
                g.DrawString(this.GetChargeString(), new Font("Trebuchet MS", 12), Brushes.Black, new PointF(Location.X + xOffset, Location.Y-5));
        }
       
        private string GetChargeString()
        {
            int charge = this.GetTotalCharge();
            if(charge == 0)
            {
                return "";
            }
            if(charge < 0)
            {
                return Math.Abs(charge).ToString()+"-";
            }
            return charge.ToString()+ "+";
        }

        private int GetTotalCharge()
        {
            int totalCharge = 0;
            foreach (var ion in ions)
            {
                totalCharge += ion.Charge;
            }
            return totalCharge;
        }
    }



}
