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
        }

        public List<Ion> GetIons()
        {
            return this.ions;
        }

        internal override void Draw(Graphics g)
        {
            //base.Draw(g);


            if (ions.Count == 1)
            {
                var ion = ions[0];
                //rectangle = new RectangleElement(location.X, location.Y, ion.Radius, ion.Radius);
                Rectangle r = GetUnsignedRectangle(new Rectangle(location.X, location.Y, ion.Radius, ion.Radius));
                Rectangle rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
                Brush b = new LinearGradientBrush(rb, Color.FromArgb((int)(255.0f * (opacity / 100.0f)), ion.Color1), Color.FromArgb((int)(255.0f * (opacity / 100.0f)), ion.Color2), LinearGradientMode.Horizontal);
                g.FillEllipse(b, r);
                Pen p = new Pen(borderColor, borderWidth);
                g.DrawEllipse(p, r);

                g.DrawString(this.GetChargeString(), new Font("Trebuchet MS", 12), Brushes.Black, new PointF(Location.X + ion.Radius, Location.Y-5));

                g.DrawString(ion.Symbol, new Font("Trebuchet MS", 12), Brushes.Black, new PointF(Location.X + ion.Radius/2-10, Location.Y + ion.Radius/2-10));

            }
            else if (ions.Count == 2)
            {
                var sodiumIon = ions.Find(io=>io.Name=="Sodium");
                var chlorineIon = ions.Find(io => io.Name == "Chlorine");


                //rectangle = new RectangleElement(location.X, location.Y, sodiumIon.Radius, sodiumIon.Radius);
                Rectangle r = GetUnsignedRectangle(new Rectangle(location.X, location.Y+(chlorineIon.Radius-sodiumIon.Radius)/2, sodiumIon.Radius, sodiumIon.Radius));
                Rectangle rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
                Brush b = new LinearGradientBrush(rb, Color.FromArgb((int)(255.0f * (opacity / 100.0f)), sodiumIon.Color1), Color.FromArgb((int)(255.0f * (opacity / 100.0f)), sodiumIon.Color2), LinearGradientMode.Horizontal);
                g.FillEllipse(b, r);
                Pen p = new Pen(borderColor, borderWidth);
                g.DrawEllipse(p, r);

                g.DrawString(sodiumIon.Symbol, new Font("Trebuchet MS", 12), Brushes.Black, new PointF(Location.X + sodiumIon.Radius / 2 - 10, Location.Y + sodiumIon.Radius / 2 - 10));


                //rectangle = new RectangleElement(location.X+sodiumIon.Radius, location.Y - sodiumIon.Radius/2, chlorineIon.Radius, chlorineIon.Radius);
                r = GetUnsignedRectangle(new Rectangle(location.X + sodiumIon.Radius, location.Y, chlorineIon.Radius, chlorineIon.Radius));
                rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
                b = new LinearGradientBrush(rb, Color.FromArgb((int)(255.0f * (opacity / 100.0f)), chlorineIon.Color1), Color.FromArgb((int)(255.0f * (opacity / 100.0f)), chlorineIon.Color2), LinearGradientMode.Horizontal);
                g.FillEllipse(b, r);
                p = new Pen(borderColor, borderWidth);
                g.DrawEllipse(p, r);

                g.DrawString(chlorineIon.Symbol, new Font("Trebuchet MS", 12), Brushes.Black, new PointF(Location.X + sodiumIon.Radius + chlorineIon.Radius / 2 - 10, Location.Y + chlorineIon.Radius / 2 - 10));

                g.DrawString(this.GetChargeString(), new Font("Trebuchet MS", 12), Brushes.Black, new PointF(Location.X + ions.Sum(i=>i.Radius), Location.Y - 5));
            }
        }
       
        private string GetChargeString()
        {
            int charge = this.GetTotalCharge();
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
