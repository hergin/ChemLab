using ChemLab.Custom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemLab.Model.Ions
{
    internal class Silver : Ion
    {
        public Silver() : base()
        {
            this.Symbol = "Ag";
            this.Id = Guid.NewGuid();
            this.Color1 = Color.Red;
            this.Color2 = Color.Red;
            this.Name = "Silver";
            this.Charge = +1;
            this.Radius = 50;
        }
    }
}
