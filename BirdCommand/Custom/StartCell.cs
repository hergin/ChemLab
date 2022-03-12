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
    public class StartCell : RectangleElement
    {
        public StartCell() : base(20, 20, 99, 33)
        {
            Background = Resources.rule_start;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
        }

        public void Highlight()
        {
            Background = Resources.rule_start_highlighted;
        }

        public void Unhighlight()
        {
            Background = Resources.rule_start;
        }
    }
}
