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
    public class RuleCell : RectangleElement
    {
        public RuleCell(int x, int y) : base(x, y, 400, 200)
        {
            Background = Resources.rule;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
        }
    }
}
