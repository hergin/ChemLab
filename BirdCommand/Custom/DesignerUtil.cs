using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    internal class DesignerUtil
    {
        internal static Point FindPositionOfAnotherEmptyAround(Designer designer, EmptyCell empty)
        {
            foreach (var element in designer.Document.Elements)
            {
                if (element is EmptyCell result)
                {
                    // area-1: newly created is above the existing element
                    if (empty.Location.Y >= result.Location.Y - BirdCommandMain.CELL_SIZE && empty.Location.Y <= result.Location.Y
                        && empty.Location.X >= result.Location.X && empty.Location.X <= result.Location.X + result.Size.Width)
                    {
                        return new Point(result.Location.X, result.Location.Y - BirdCommandMain.CELL_SIZE);
                    }
                    else // area-2: newly created is below the existing element
                    if (empty.Location.Y >= result.Location.Y + BirdCommandMain.CELL_SIZE && empty.Location.Y <= result.Location.Y + result.Size.Height + BirdCommandMain.CELL_SIZE
                        && empty.Location.X >= result.Location.X && empty.Location.X <= result.Location.X + result.Size.Width)
                    {
                        return new Point(result.Location.X, result.Location.Y + BirdCommandMain.CELL_SIZE);
                    }
                    else // area-3: newly created is right of the existing element
                    if (empty.Location.Y >= result.Location.Y  && empty.Location.Y <= result.Location.Y + result.Size.Height
                        && empty.Location.X >= result.Location.X + BirdCommandMain.CELL_SIZE && empty.Location.X <= result.Location.X + result.Size.Width + BirdCommandMain.CELL_SIZE)
                    {
                        return new Point(result.Location.X + BirdCommandMain.CELL_SIZE, result.Location.Y);
                    }
                    else // area-4: newly created is left of the existing element
                    if (empty.Location.Y >= result.Location.Y && empty.Location.Y <= result.Location.Y + result.Size.Height
                        && empty.Location.X >= result.Location.X - BirdCommandMain.CELL_SIZE && empty.Location.X <= result.Location.X)
                    {
                        return new Point(result.Location.X - BirdCommandMain.CELL_SIZE, result.Location.Y);
                    }
                }
            }
            return empty.Location;
        }

        internal static EmptyCell FindCellUnderneath(Designer designer, BirdCell bird)
        {
            foreach (var element in designer.Document.Elements)
            {
                BaseElement casted = element as BaseElement;
                if (casted is EmptyCell result
                    && bird.Location.Y >= casted.Location.Y && bird.Location.Y < casted.Location.Y + casted.Size.Height
                    && bird.Location.X >= casted.Location.X && bird.Location.X < casted.Location.X + casted.Size.Width)
                {
                    return result;
                }
            }
            return null;
        }

        internal static EmptyCell FindCellUnderneath(Designer designer, EmptyCell empty, Point point)
        {
            foreach (var element in designer.Document.Elements)
            {
                BaseElement casted = element as BaseElement;
                if (casted is EmptyCell result && !result.Equals(empty)
                    && point.Y >= casted.Location.Y && point.Y < casted.Location.Y + casted.Size.Height
                    && point.X >= casted.Location.X && point.X < casted.Location.X + casted.Size.Width)
                {
                    return result;
                }
            }
            return null;
        }

        internal static EmptyCell FindCellUnderneath(Designer designer, Point point)
        {
            foreach (var element in designer.Document.Elements)
            {
                BaseElement casted = element as BaseElement;
                if (casted is EmptyCell result
                    && point.Y >= casted.Location.Y && point.Y < casted.Location.Y + casted.Size.Height
                    && point.X >= casted.Location.X && point.X < casted.Location.X + casted.Size.Width)
                {
                    return result;
                }
            }
            return null;
        }

        internal static List<BaseElement> FindElementsWithin(Designer designer, BaseElement parentElement)
        {
            List<BaseElement> result = new List<BaseElement>();
            foreach (var element in designer.Document.Elements)
            {
                BaseElement casted = element as BaseElement;
                if (!(casted is StartCell || casted is SnapCell)) // don't select those with rules even if it might be inside sometimes
                {
                    if (casted.Location.Y >= parentElement.Location.Y && casted.Location.Y <= parentElement.Location.Y + parentElement.Size.Height
                        && casted.Location.Y + casted.Size.Height <= parentElement.Location.Y + parentElement.Size.Height
                        && casted.Location.X >= parentElement.Location.X && casted.Location.X <= parentElement.Location.X + parentElement.Size.Width
                        && casted.Location.X + casted.Size.Width <= parentElement.Location.X + parentElement.Size.Width)
                    {
                        result.Add(casted);
                    }
                }
            }
            return result;
        }

        internal static void ArrangeTheOrder(Designer designer)
        {
            var emptyCells = designer.Document.Elements.GetArray().Where(e => e is EmptyCell);
            foreach (var emptyCell in emptyCells)
            {
                designer.Document.BringToFrontElement(emptyCell);
            }
            var pigCells = designer.Document.Elements.GetArray().Where(e => e is PigCell);
            foreach (var pigCell in pigCells)
            {
                designer.Document.BringToFrontElement(pigCell);
            }
            var birdCells = designer.Document.Elements.GetArray().Where(e => e is BirdCell);
            foreach (var birdCell in birdCells)
            {
                designer.Document.BringToFrontElement(birdCell);
            }
        }
    }
}
