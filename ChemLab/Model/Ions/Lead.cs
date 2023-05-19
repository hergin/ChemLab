using ChemLab.Custom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemLab.Model.Ions
{
    internal class Lead : Ion
    {
        public Lead() : base()
        {
            this.Symbol = "Pb";
            this.Id = Guid.NewGuid();
            this.Color1 = Color.Red;
            this.Color2 = Color.Red;
            this.Name = "Lead";
            this.Charge = +2;
            this.Radius = 55;
        }
    }
}
