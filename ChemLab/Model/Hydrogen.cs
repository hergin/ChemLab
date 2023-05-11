using ChemLab.Custom;
using System;
using System.Drawing;

namespace ChemLab.Model
{
    internal class Hyrdogen : Ion
    {
        public Hyrdogen() : base()
        {
            this.Symbol = "H";
            this.Id = Guid.NewGuid();
            this.Color1 = Color.Purple;
            this.Color2 = Color.MediumPurple;
            this.Name = "Hyrdogen";
            this.Charge = +1;
            this.Radius = 30;
        }
    }
}
