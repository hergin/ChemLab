using BirdCommand.Custom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Model
{
    internal class Sodium : Ion
    {
        public Sodium():base()
        {
            this.Symbol = "Na";
            this.Id = Guid.NewGuid();
            this.Color1 = Color.Blue;
            this.Color2 = Color.LightBlue;
            this.Name = "Sodium";
            this.Charge = 1;
            this.Radius = 50;
        }
    }
}
