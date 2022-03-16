using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    public class ConvertUtil
    {
        public static Graph PatternToGraph(List<BaseElement> elements)
        {
            var result = new Graph();
            var allEmptyElements = elements.Where(e => e is EmptyCell);
            var handled = new List<BaseElement>();
            foreach (var element in allEmptyElements)
            {
                result.Add(new Node() { Id = IdFromLocation(element.Location), Type = "Em" });
                
                var cellAbove = allEmptyElements.Where(empty => empty.Location.X == element.Location.X && empty.Location.Y == element.Location.Y - BirdCommandMain.CELL_SIZE).FirstOrDefault();
                if (cellAbove != null && !handled.Contains(cellAbove))
                {
                    result.Add(new Edge() { From = IdFromLocation(element.Location), To = IdFromLocation(cellAbove.Location) });
                }
                var cellBelow = allEmptyElements.Where(empty => empty.Location.X == element.Location.X && empty.Location.Y == element.Location.Y + BirdCommandMain.CELL_SIZE).FirstOrDefault();
                if (cellBelow != null && !handled.Contains(cellBelow))
                {
                    result.Add(new Edge() { From = IdFromLocation(element.Location), To = IdFromLocation(cellBelow.Location) });
                }
                var cellRight = allEmptyElements.Where(empty => empty.Location.X == element.Location.X + BirdCommandMain.CELL_SIZE && empty.Location.Y == element.Location.Y).FirstOrDefault();
                if (cellRight != null && !handled.Contains(cellRight))
                {
                    result.Add(new Edge() { From = IdFromLocation(element.Location), To = IdFromLocation(cellRight.Location) });
                }
                var cellLeft = allEmptyElements.Where(empty => empty.Location.X == element.Location.X - BirdCommandMain.CELL_SIZE && empty.Location.Y == element.Location.Y).FirstOrDefault();
                if (cellLeft != null && !handled.Contains(cellLeft))
                {
                    result.Add(new Edge() { From = IdFromLocation(element.Location), To = IdFromLocation(cellLeft.Location) });
                }

                handled.Add(element);
            }
            return result;
        }

        public static String IdFromLocation(Point Location)
        {
            return Location.X + "_" + Location.Y;
        }
    }
}
