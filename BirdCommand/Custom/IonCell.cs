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
    public class IonCell : ElipseNode
    {
        [NonSerialized]
        List<Ion> ions;

        public IonCell(int x, int y,List<Ion> ions) : base(x, y, ions.Sum(i=>i.Radius), ions.Sum(i => i.Radius))
        {
            FillColor1 = Color.Blue;
            FillColor2 = Color.LightBlue;
            this.ions = ions;
            Label = new LabelElement(new Rectangle(x,y+BirdCommandMain.CELL_SIZE/4,BirdCommandMain.CELL_SIZE,BirdCommandMain.CELL_SIZE/3));
            label.Font = new Font("Trebuchet MS", 12);
            label.ForeColor1 = Color.White;
            label.BackColor1=Color.Red;
            Label.Text = ions[0].Symbol;
        }
       
        
        public void AddIon(Ion ion)
        {
            this.ions.Add(ion);
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
                elipse = new ElipseElement(location.X, location.Y, ion.Radius, ion.Radius);
                Rectangle r = GetUnsignedRectangle(new Rectangle(location.X, location.Y, ion.Radius, ion.Radius));
                Rectangle rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
                Brush b = new LinearGradientBrush(rb, Color.FromArgb((int)(255.0f * (opacity / 100.0f)), ion.Color1), Color.FromArgb((int)(255.0f * (opacity / 100.0f)), ion.Color2), LinearGradientMode.Horizontal);
                g.FillEllipse(b, r);
                Pen p = new Pen(borderColor, borderWidth);
                g.DrawEllipse(p, r);
            }
            else if (ions.Count == 2)
            {
                var sodiumIon = ions.Find(io=>io.Name=="Sodium");
                elipse = new ElipseElement(location.X, location.Y, sodiumIon.Radius, sodiumIon.Radius);
                Rectangle r = GetUnsignedRectangle(new Rectangle(location.X, location.Y, sodiumIon.Radius, sodiumIon.Radius));
                Rectangle rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
                Brush b = new LinearGradientBrush(rb, Color.FromArgb((int)(255.0f * (opacity / 100.0f)), sodiumIon.Color1), Color.FromArgb((int)(255.0f * (opacity / 100.0f)), sodiumIon.Color2), LinearGradientMode.Horizontal);
                g.FillEllipse(b, r);
                Pen p = new Pen(borderColor, borderWidth);
                g.DrawEllipse(p, r);

                var chlorineIon = ions.Find(io => io.Name == "Chlorine");
                elipse = new ElipseElement(location.X+sodiumIon.Radius, location.Y - sodiumIon.Radius/2, chlorineIon.Radius, chlorineIon.Radius);
                r = GetUnsignedRectangle(new Rectangle(location.X + sodiumIon.Radius, location.Y - sodiumIon.Radius / 2, chlorineIon.Radius, chlorineIon.Radius));
                rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
                b = new LinearGradientBrush(rb, Color.FromArgb((int)(255.0f * (opacity / 100.0f)), chlorineIon.Color1), Color.FromArgb((int)(255.0f * (opacity / 100.0f)), chlorineIon.Color2), LinearGradientMode.Horizontal);
                g.FillEllipse(b, r);
                p = new Pen(borderColor, borderWidth);
                g.DrawEllipse(p, r);
            }

                /*

                if(this.GetTotalCharge() != 0)
                {
                    g.DrawEllipse(p, new Rectangle(Location.X + 50, location.Y, 20, 20));
                    g.DrawString(this.GetChargeString(), new Font("Trebuchet MS", 8), Brushes.Black, new PointF(Location.X + 53, Location.Y ));
                }*/
            }
       
        private string GetChargeString()
        {
            int charge = this.GetTotalCharge();
            if(charge < 0)
            {
                return charge.ToString();
            }
            return "+" + charge.ToString();
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
