using ChemLab.Custom;
using System;
using System.Drawing;

namespace ChemLab.Model.Ions
{
    internal class Sodium : Ion
    {
        public Sodium() : base()
        {
            this.Symbol = "Na";
            this.Id = Guid.NewGuid();
            this.Color1 = Color.SeaGreen;
            this.Color2 = Color.SeaGreen;
            this.Name = "Sodium";
            this.Charge = 1;
            this.Radius = 50;
        }
    }
}
