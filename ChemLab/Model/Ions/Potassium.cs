using ChemLab.Custom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemLab.Model.Ions
{
    internal class Potassium : Ion
    {
        public Potassium() : base()
        {
            this.Symbol = "K";
            this.Id = Guid.NewGuid();
            this.Color1 = Color.Gold;
            this.Color2 = Color.Gold;
            this.Name = "Potassium";
            this.Charge = +1;
            this.Radius = 45;
        }
    }
}
