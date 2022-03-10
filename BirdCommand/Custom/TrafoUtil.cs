using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    public enum RuleType
    {
        MoveForward, TurnRight, TurnLeft, Turn180, DoNothing
    }

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

        public static RuleType IdentifyRuleType(List<BaseElement> lhs, List<BaseElement> rhs)
        {
            var filteredLhs = lhs.Where(l => l is BirdCell || l is EmptyCell);
            var filteredRhs = rhs.Where(r => r is BirdCell || r is EmptyCell);

            // 1) Patterns should have same number of elements
            if (filteredLhs.Count() != filteredRhs.Count())
                throw new Exception("Patterns do not have same number of elements!");

            // 2) There should be at most one bird at each pattern.
            //    0 is NOT acceptable for now, where the rule might be killing bird! TODO
            if (filteredLhs.Where(f => f is BirdCell).Count() != 1 || filteredRhs.Where(f => f is BirdCell).Count() != 1)
                throw new Exception("Patterns should only have one bird!");

            // Turn rules identification: There is only one bird and one empty cell first.
            if (filteredLhs.Where(f => f is BirdCell).Count() == 1 && filteredLhs.Where(f => f is EmptyCell).Count() == 1
                && filteredRhs.Where(f => f is BirdCell).Count() == 1 && filteredRhs.Where(f => f is EmptyCell).Count() == 1)
            {
                // Both the bird and the cell should be in the same position
                var birdLhs = filteredLhs.Where(f => f is BirdCell).First() as BirdCell;
                var emptyLhs = filteredLhs.Where(f => f is EmptyCell).First() as EmptyCell;
                var birdRhs = filteredRhs.Where(f => f is BirdCell).First() as BirdCell;
                var emptyRhs = filteredRhs.Where(f => f is EmptyCell).First() as EmptyCell;
                if (birdLhs.Location.Equals(emptyLhs.Location) && birdRhs.Location.Equals(emptyRhs.Location))
                {
                    // Identifying which way to turn
                    var difference = birdLhs.Direction - birdRhs.Direction;

                    if ((difference + 4) % 4 == 3)
                        return RuleType.TurnRight;
                    else if ((difference + 4) % 4 == 1)
                        return RuleType.TurnLeft;
                    else if ((difference + 4) % 4 == 2)
                        return RuleType.Turn180;
                }
            }

            return RuleType.DoNothing;
        }

        public static List<BaseElement> FindPreConditionElements(List<BaseElement> allElements, BaseElement parentElement)
        {
            List<BaseElement> result = new List<BaseElement>();
            foreach (var element in allElements)
            {
                BaseElement casted = element as BaseElement;
                if (casted is EmptyCell || casted is BirdCell)
                {
                    if (casted.Location.Y >= parentElement.Location.Y
                        && casted.Location.Y <= parentElement.Location.Y + parentElement.Size.Height
                        && casted.Location.Y + casted.Size.Height <= parentElement.Location.Y + parentElement.Size.Height
                        && casted.Location.X >= parentElement.Location.X
                        && casted.Location.X <= parentElement.Location.X + parentElement.Size.Width / 2
                        && casted.Location.X + casted.Size.Width <= parentElement.Location.X + parentElement.Size.Width / 2)
                    {
                        result.Add(casted);
                    }
                }
            }
            return result;
        }

        public static List<BaseElement> FindPostConditionElements(List<BaseElement> allElements, BaseElement parentElement)
        {
            List<BaseElement> result = new List<BaseElement>();
            foreach (var element in allElements)
            {
                BaseElement casted = element as BaseElement;
                if (casted is EmptyCell || casted is BirdCell)
                {
                    if (casted.Location.Y >= parentElement.Location.Y
                        && casted.Location.Y <= parentElement.Location.Y + parentElement.Size.Height
                        && casted.Location.Y + casted.Size.Height <= parentElement.Location.Y + parentElement.Size.Height
                        && casted.Location.X >= parentElement.Location.X + parentElement.Size.Width / 2
                        && casted.Location.X <= parentElement.Location.X + parentElement.Size.Width
                        && casted.Location.X + casted.Size.Width <= parentElement.Location.X + parentElement.Size.Width)
                    {
                        result.Add(casted);
                    }
                }
            }
            return result;
        }
    }
}
