using BirdCommand.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Model
{
    public class Compound
    {
        public List<Ion> ions { get; set; }

        // TODO the ions should be sorted in someway to have same symbol for NA-CL or CL-NA regardless of the array position
        public String Symbol { get { return string.Join("",ions.Select(x => x.Symbol).ToList()); } }

        public int GetWidth()
        {
            return this.ions.Sum(x => x.Radius);
        }

        public int GetHalfWidth()
        {
            return this.ions.Sum(x => x.Radius) /2;
        }
        public override string ToString()
        {
            return Symbol;
        }
    }
}
