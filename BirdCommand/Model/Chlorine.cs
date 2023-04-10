using BirdCommand.Custom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Model
{
    internal class Chlorine : Ion
    {
        public Chlorine():base()
        {
            this.Symbol = "Cl";
            this.Id = Guid.NewGuid();
            this.Color1 = Color.Green;
            this.Color2 = Color.LightGreen;
            this.Name = "Chlorine";
            this.Charge = -1;
            this.Radius = 75;
        }
    }
}
