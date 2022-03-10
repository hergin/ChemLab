using BirdCommand.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    [Serializable]
    public class PigCell : RectangleElement
    {
        public PigCell(int x, int y) : base(x, y, BirdCommandMain.CELL_SIZE, BirdCommandMain.CELL_SIZE)
        {
            Background = Resources.Pig;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
        }
    }
}
