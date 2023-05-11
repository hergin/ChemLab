using BirdCommand.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Drawing;

namespace BirdCommand.Custom
{
    [Serializable]
    public class StartCell : RectangleElement
    {
        public StartCell() : base(230, 30, 99, 33)
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
