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
        public static List<BaseElement> FindPattern(List<BaseElement> model, List<BaseElement> pattern)
        {
            // this should be generic but for the sake of prototype, it will start with a bird in the pattern and look for the cells under or around it.
            // and it only works for a pattern with bird and empty cells
            var birdInPattern = pattern.Where(p => p is BirdCell).First() as BirdCell;
            var birdInModel = model.Where(m => m is BirdCell).First() as BirdCell;

            // If birds are not facing the same direction
            if (birdInPattern.Direction != birdInModel.Direction)
                return new List<BaseElement>();

            var result = new List<BaseElement>();

            foreach (var cell in pattern.Where(p => p is EmptyCell))
            {
                Point expectedPosition = new Point(birdInModel.Location.X - (birdInPattern.Location.X - cell.Location.X), birdInModel.Location.Y - (birdInPattern.Location.Y - cell.Location.Y));
                var emptyCell = GetTheEmptyCell(model, expectedPosition);
                if (emptyCell == null)
                    return new List<BaseElement>(); // any of the pattern elements not being in the model should return empty, regardless of how many matched
                else
                    result.Add(emptyCell);
            }
            return result;
        }

        public static bool DoesPatternExist(List<BaseElement> model, List<BaseElement> pattern)
        {
            // this should be generic but for the sake of prototype, it will start with a bird in the pattern and look for the cells under or around it.
            // and it only works for a pattern with bird and empty cells
            return FindPattern(model, pattern).Count() > 0;
        }

        public static BaseElement GetTheEmptyCell(List<BaseElement> model, Point point)
        {
            foreach (var element in model)
            {
                if (element is EmptyCell && element.Location.Equals(point))
                    return element;
            }
            return null;
        }

        public static bool IsThereAnEmptyCell(List<BaseElement> model, Point point)
        {
            return GetTheEmptyCell(model, point) == null;
        }
    }
}
