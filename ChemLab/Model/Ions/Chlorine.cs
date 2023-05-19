using ChemLab.Custom;
using System;
using System.Drawing;

namespace ChemLab.Model.Ions
{
    internal class Chlorine : Ion
    {
        public Chlorine() : base()
        {
            this.Symbol = "Cl";
            this.Id = Guid.NewGuid();
            this.Color1 = Color.Goldenrod;
            this.Color2 = Color.Goldenrod;
            this.Name = "Chlorine";
            this.Charge = -1;
            this.Radius = 55;
        }
    }
}
