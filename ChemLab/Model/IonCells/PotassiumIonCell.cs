using ChemLab.Custom;
using ChemLab.Model.Ions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemLab.Model.IonCells
{
    [Serializable]
    internal class PotassiumIonCell : IonCell
    {
        public PotassiumIonCell(int x, int y) : base(x, y, new List<Ion> { new Potassium() })
        {
        }
    }
}
