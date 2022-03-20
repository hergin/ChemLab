using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BirdCommand.Custom
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

        public static List<BaseElement> Rotate90Clockwise(List<BaseElement> pattern)
        {
            var normalizedClone = NormalizePattern(pattern);
            foreach (var element in normalizedClone)
            {
                var midX = element.Location.X + element.Size.Width / 2;
                var midY = element.Location.Y + element.Size.Height / 2;
                var newMidX = -midY;
                var newMidY = midX;
                var newX = newMidX - element.Size.Width / 2;
                var newY = newMidY - element.Size.Height / 2;
                element.Location = new Point(newX, newY);
                if (element is BirdCell bird)
                    bird.TurnRight();
            }
            return NormalizePattern(normalizedClone);
        }
    }
}
