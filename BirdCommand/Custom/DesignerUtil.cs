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
        internal static void CopyLHStoRHS(Designer designer, RuleCell rule)
        {
            var lhsElements = TrafoUtil.FindPreConditionElements(DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(designer).ToList(),
                                rule);

            foreach (var element in lhsElements)
            {
                if (element is EmptyCell empty)
                {
                    designer.Document.AddElement(new EmptyCell(empty.Location.X + 200, empty.Location.Y));
                }
                else if (element is BirdCell bird)
                {
                    designer.Document.AddElement(new BirdCell(bird.Location.X + 200, bird.Location.Y, bird.Direction));
                }
                else if (element is PigCell pig)
                {
                    designer.Document.AddElement(new PigCell(pig.Location.X + 200, pig.Location.Y));
                }
            }
        }

        internal static RuleCell AddRuleToNextEmptySpot(Designer designer, Point ruleButtonLocation)
        {
            var highestY = 0;
            foreach (var element in designer.Document.Elements)
            {
                if ((element is RuleCell || element is StartCell) && ((BaseElement)element).Location != ruleButtonLocation)
                {
                    if (((BaseElement)element).Location.Y + ((BaseElement)element).Size.Height > highestY)
                    {
                        highestY = ((BaseElement)element).Location.Y + ((BaseElement)element).Size.Height;
                    }
                }
            }
            var newRule = new RuleCell(231, highestY - 5);
            designer.Document.AddElement(newRule);
            DesignerUtil.ArrangeTheOrder(designer);
            return newRule;
        }

        internal static void MoveRuleAndItsContents(Designer designer, RuleCell rule, int newX, int newY)
        {
            var ruleStarterPosition = rule.Location;

            var ruleContent = DesignerUtil.FindElementsWithin(designer, rule);
            foreach (var element in ruleContent)
            {
                // move the contents of the rule as well
                var differenceX = Math.Abs(element.Location.X - ruleStarterPosition.X);
                var differenceY = Math.Abs(element.Location.Y - ruleStarterPosition.Y);
                element.Location = new Point(newX + differenceX, newY + differenceY);
            }
        }

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

        internal static void SnapNewEmptyCellToExistingNeighbors(Designer designer_trafo, EmptyCell emptyCell, Point locationDropped)
        {
            var emptyUnderneathSouth = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(locationDropped.X, locationDropped.Y + BirdCommandMain.CELL_SIZE));
            if (emptyUnderneathSouth != null)
            {
                emptyCell.Location = new Point(emptyUnderneathSouth.Location.X, emptyUnderneathSouth.Location.Y - BirdCommandMain.CELL_SIZE);
                return;
            }
            var emptyUnderneathNorth = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(locationDropped.X, locationDropped.Y - BirdCommandMain.CELL_SIZE));
            if (emptyUnderneathNorth != null)
            {
                emptyCell.Location = new Point(emptyUnderneathNorth.Location.X, emptyUnderneathNorth.Location.Y + BirdCommandMain.CELL_SIZE);
                return;
            }
            var emptyUnderneathEast = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(locationDropped.X - BirdCommandMain.CELL_SIZE, locationDropped.Y));
            if (emptyUnderneathEast != null)
            {
                emptyCell.Location = new Point(emptyUnderneathEast.Location.X + BirdCommandMain.CELL_SIZE, emptyUnderneathEast.Location.Y);
                return;
            }
            var emptyUnderneathWest = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(locationDropped.X + BirdCommandMain.CELL_SIZE, locationDropped.Y));
            if (emptyUnderneathWest != null)
            {
                emptyCell.Location = new Point(emptyUnderneathWest.Location.X - BirdCommandMain.CELL_SIZE, emptyUnderneathWest.Location.Y);
                return;
            }
            var emptyUnderneathTopLeft = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(locationDropped.X - BirdCommandMain.CELL_SIZE, locationDropped.Y - BirdCommandMain.CELL_SIZE));
            if (emptyUnderneathTopLeft != null)
            {
                emptyCell.Location = new Point(emptyUnderneathTopLeft.Location.X + BirdCommandMain.CELL_SIZE, emptyUnderneathTopLeft.Location.Y + BirdCommandMain.CELL_SIZE);
                return;
            }
            var emptyUnderneathBottomLeft = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(locationDropped.X - BirdCommandMain.CELL_SIZE, locationDropped.Y + BirdCommandMain.CELL_SIZE));
            if (emptyUnderneathBottomLeft != null)
            {
                emptyCell.Location = new Point(emptyUnderneathBottomLeft.Location.X + BirdCommandMain.CELL_SIZE, emptyUnderneathBottomLeft.Location.Y - BirdCommandMain.CELL_SIZE);
                return;
            }
            var emptyUnderneathBottomRight = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(locationDropped.X + BirdCommandMain.CELL_SIZE, locationDropped.Y + BirdCommandMain.CELL_SIZE));
            if (emptyUnderneathBottomRight != null)
            {
                emptyCell.Location = new Point(emptyUnderneathBottomRight.Location.X - BirdCommandMain.CELL_SIZE, emptyUnderneathBottomRight.Location.Y - BirdCommandMain.CELL_SIZE);
                return;
            }
            var emptyUnderneathTopRight = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(locationDropped.X + BirdCommandMain.CELL_SIZE, locationDropped.Y - BirdCommandMain.CELL_SIZE));
            if (emptyUnderneathTopRight != null)
            {
                emptyCell.Location = new Point(emptyUnderneathTopRight.Location.X - BirdCommandMain.CELL_SIZE, emptyUnderneathTopRight.Location.Y + BirdCommandMain.CELL_SIZE);
                return;
            }
        }
    }
}
