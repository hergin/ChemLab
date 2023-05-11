using Dalssoft.DiagramNet;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ChemLab.Custom
{
    public class PatternUtil
    {
        public static List<BaseElement> Clone(List<BaseElement> elements)
        {
            return elements.Select(el => el.Clone()).ToList();
        }

        // Cut the empty X and Y (left and top) and align the whole pattern starting from 0,0
        public static List<BaseElement> NormalizePattern(List<BaseElement> pattern)
        {
            var adjustX = pattern.Select(e => e.Location.X).Min();
            var adjustY = pattern.Select(e => e.Location.Y).Min();
            var clone = Clone(pattern);
            foreach (var element in clone)
            {
                element.Location = new Point(element.Location.X - adjustX, element.Location.Y - adjustY);
            }
            return clone;
        }

    }
}
