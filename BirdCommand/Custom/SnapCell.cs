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
    public class SnapCell : RectangleElement
    {
        public SnapCell(int x, int y) : base(x, y, 30, 10)
        {
            Background = Resources.snap;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
        }
    }
}
