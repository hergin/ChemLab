using BirdCommand.Model;
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
        Compound compound;

        public IonCell(int x, int y,List<Ion> ions) : base(x, y, ions.Sum(i=>i.Radius)+15, ions.Select(i=>i.Radius).Max())
        {
            this.compound = new Compound { ions = ions };
            Name = compound.Symbol;
        }

        public IonCell(int x, int y, Compound compound) : base(x, y, compound.ions.Sum(i => i.Radius) + 15, compound.ions.Select(i => i.Radius).Max())
        {
            this.compound = compound;
            Name = compound.Symbol;
        }


        public void AddIon(Ion ion)
        {
            compound.ions.Add(ion);
            OnAppearanceChanged(new EventArgs());
            base.size.Width = compound.ions.Sum(x=> x.Radius) +5;
            base.size.Height = compound.ions.Max(x=> x.Radius) +5;
        }

        internal Compound GetCompound()
        {
            return this.compound;
        }

        public List<Ion> GetIons()
        {
            return this.compound.ions;
        }

        internal override void Draw(Graphics g)
        {
                int xOffset = 0;
                int largestRadius = compound.ions.Max(x=> x.Radius);
                foreach(Ion ion in compound.ions)
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
            foreach (var ion in compound.ions)
            {
                totalCharge += ion.Charge;
            }
            return totalCharge;
        }
    }



}
