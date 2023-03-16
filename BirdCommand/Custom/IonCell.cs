using BirdCommand.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    [Serializable]
    public class IonCell : ElipseElement
    {
        [NonSerialized]
        List<Ion> ions;

        public IonCell(int x, int y) : base(x, y, BirdCommandMain.CELL_SIZE, BirdCommandMain.CELL_SIZE)
        {
            FillColor1 = Color.Blue;
            FillColor2 = Color.LightBlue;
            ions = new List<Ion>();
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
            base.Draw(g);

            Pen p;
            p = new Pen(borderColor, borderWidth);

            if(this.GetTotalCharge() != 0)
            {
                g.DrawEllipse(p, new Rectangle(Location.X + 50, location.Y, 20, 20));
                g.DrawString(this.GetChargeString(), new Font("Trebuchet MS", 8), Brushes.Black, new PointF(Location.X + 53, Location.Y ));
            }
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
