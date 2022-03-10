using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    public class TrafoUtil
    {
        public static bool DoesPatternExist(List<BaseElement> model, List<BaseElement> pattern)
        {
            // this should be generic but for the sake of prototype, it will start with a bird in the pattern and look for the cells under or around it.
            // and it only works for a pattern with bird and empty cells
            var birdInPattern = pattern.Where(p => p is BirdCell).First();
            var birdInModel = model.Where(m => m is BirdCell).First();

            foreach (var cell in pattern.Where(p => !(p is BirdCell)))
            {
                Point expectedPosition = new Point(birdInPattern.Location.X - cell.Location.X, birdInPattern.Location.Y - cell.Location.Y);
                if (!IsThereAnEmptyCell(model, expectedPosition))
                    return false;
            }
            return true;
        }

        public static bool IsThereAnEmptyCell(List<BaseElement> model, Point point)
        {
            foreach (var element in model)
            {
                if (element is EmptyCell && element.Location.Equals(point))
                    return true;
            }
            return false;
        }
    }
}
