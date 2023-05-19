using ChemLab.Custom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemLab.Model.Ions
{
    internal class Nitrate : Ion
    {
        public Nitrate() : base()
        {
            this.Symbol = "NO3";
            this.Id = Guid.NewGuid();
            this.Color1 = Color.MediumSlateBlue;
            this.Color2 = Color.MediumSlateBlue;
            this.Name = "Nitrate";
            this.Charge = -1;
            this.Radius = 65;
        }
    }
}
