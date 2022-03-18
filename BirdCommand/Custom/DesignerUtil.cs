using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    public class DesignerUtil
    {
        // Return the elements outside the block panel (skip Snap and Start cells as well)
        internal static List<BaseElement> GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(Designer designer)
        {
            var allElements = designer.Document.Elements.GetArray();

            // Find the blockpanel if exists (it is type of RectangleNode)
            var possibleBlockPanel = allElements.Where(e => e is RectangleNode).FirstOrDefault();
            if(possibleBlockPanel != null)
            {
                // return the elements fully outside the block panel
                var blockPanel = possibleBlockPanel as RectangleNode;
                return allElements.Where(e => !IsSecondInsideFirst(blockPanel, e) && !e.Equals(blockPanel) && !(e is SnapCell) && !(e is StartCell)).ToList();
            }
            // return all elements, because there is no block panel
            return allElements.ToList();
        }

        internal static List<BaseElement> GetTrafoElementsOutsideBlockWithoutSnapOrBlock(Designer designer)
        {
            var allElements = designer.Document.Elements.GetArray();

            // Find the blockpanel if exists (it is type of RectangleNode)
            var possibleBlockPanel = allElements.Where(e => e is RectangleNode).FirstOrDefault();
            if (possibleBlockPanel != null)
            {
                // return the elements fully outside the block panel
                var blockPanel = possibleBlockPanel as RectangleNode;
                return allElements.Where(e => !IsSecondInsideFirst(blockPanel, e) && !(e is SnapCell) && !e.Equals(blockPanel)).ToList();
            }
            // return all elements, because there is no block panel
            return allElements.ToList();
        }

        public static bool IsSecondInsideFirst(BaseElement first, BaseElement second)
        {
            // If the second top-left point is inside and second bottom-right point is inside, then it is inside.
            if (second.Location.X > first.Location.X && second.Location.X < first.Location.X + first.Size.Width
                && second.Location.Y > first.Location.Y && second.Location.Y < first.Location.Y + first.Size.Height
                && second.Location.X + second.Size.Width > first.Location.X && second.Location.X + second.Size.Width < first.Location.X + first.Size.Width
                && second.Location.Y + second.Size.Height > first.Location.Y && second.Location.Y + second.Size.Height < first.Location.Y + first.Size.Height)
            {
                return true;
            }
            return false;
        }

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
                if (!(casted is StartCell || casted is SnapCell) && casted.Location.X > 200) // don't select those with rules even if it might be inside sometimes
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
