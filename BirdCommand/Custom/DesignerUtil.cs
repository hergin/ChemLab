﻿using BirdCommand.Model;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BirdCommand.Custom
{
    public class DesignerUtil
    {
        static int placementXOffset = 0;
        static int placementYOffset = 0;
        internal static Boolean ApplyChanges(Designer designer, List<ChangeStep> changes)
        {
            foreach (ChangeStep changeStep in changes)
            {
                Boolean isChangeStepRan = HandleChangeStep(designer, changeStep);
                if (!isChangeStepRan)
                {
                    return false;
                }
            }
            return true;
        }

        internal static Boolean HandleChangeStep(Designer designer, ChangeStep changeStep)
        {
            Random rnd = new Random();
            if(changeStep.Type == ChangeStepType.Add )
            {
                IonCell ionCell = new IonCell((placementXOffset %250),260 + placementYOffset, changeStep.Compound.ions);
                placementXOffset += 5;
                placementYOffset +=5;
                designer.Document.AddElement(ionCell);
                return true;
            }
            if(changeStep.Type ==ChangeStepType.Delete)
            {
                BaseElement foundElement = designer.Document.Elements.GetArray().Where(x=> x.Name == changeStep.Compound.Symbol).First();
               
                if(foundElement == null)
                {
                    return false;
                }
                designer.Document.DeleteElement(foundElement);
                return true;
            }
            return false;

        }


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

        internal static RuleCell AddRuleToNextEmptySpot(Designer designer)
        {
            var highestY = 0;
            foreach (var element in GetTrafoElementsOutsideBlockWithoutSnapOrBlock(designer))
            {
                if (element is RuleCell || element is StartCell)
                {
                    if (element.Location.Y + element.Size.Height > highestY)
                    {
                        highestY = element.Location.Y + element.Size.Height;
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

        internal static IonCell FindIonCellUnderneath(Designer designer, IonCell ionCell)
        {
            int rightBorder = ionCell.Location.X + ionCell.Size.Width;
            int leftBorder = ionCell.Location.X;
            int topBorder = ionCell.Location.Y;
            int bottomBorder = ionCell.Location.Y + ionCell.Size.Height;

            foreach (var element in designer.Document.Elements)
            {
                BaseElement casted = element as BaseElement;
                if ((casted is IonCell result && !result.GetIons()[0].Id.Equals(ionCell.GetIons()[0].Id))
                    &&(leftBorder < result.Location.X + result.Size.Width && rightBorder > result.Location.X
                      && topBorder < result.Location.Y + result.Size.Height && bottomBorder > result.Location.Y))
                {
                    return result;
                }
            }
            return null;
        }

        internal static void SnapIonCellToNeighbor(Designer designer_trafo, IonCell ionCell)
        {
            var ionCellUnderneath = DesignerUtil.FindIonCellUnderneath(designer_trafo, ionCell);
            if (ionCellUnderneath != null)
            {                
                ionCellUnderneath.AddIon(ionCell.GetIons()[0]);
                designer_trafo.Document.DeleteElement(ionCell);
                return;
            }

            return;

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
            var ionCells = designer.Document.Elements.GetArray().Where(e => e is IonCell);
            foreach (var ionCell in ionCells)
            {
                designer.Document.BringToFrontElement(ionCell);
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
            
        }



        internal static void SolveMaze3(Designer designer)
        {
            RuleCell firstRule = DesignerUtil.AddRuleToNextEmptySpot(designer);
            firstRule.IncreaseRuleCount();
            designer.Document.AddElement(new EmptyCell(firstRule.Location.X + 50, firstRule.Location.Y + 50));
            designer.Document.AddElement(new EmptyCell(firstRule.Location.X + 100, firstRule.Location.Y + 50));
            designer.Document.AddElement(new BirdCell(firstRule.Location.X + 50, firstRule.Location.Y + 50, Direction.Right));
            designer.Document.AddElement(new EmptyCell(firstRule.Location.X + 250, firstRule.Location.Y + 50));
            designer.Document.AddElement(new EmptyCell(firstRule.Location.X + 300, firstRule.Location.Y + 50));
            designer.Document.AddElement(new BirdCell(firstRule.Location.X + 300, firstRule.Location.Y + 50, Direction.Right));

            RuleCell secondRule = DesignerUtil.AddRuleToNextEmptySpot(designer);
            designer.Document.AddElement(new BirdCell(secondRule.Location.X + 50, secondRule.Location.Y + 50, Direction.Right));
            designer.Document.AddElement(new BirdCell(secondRule.Location.X + 250, secondRule.Location.Y + 50, Direction.Down));

            RuleCell thirdRule = DesignerUtil.AddRuleToNextEmptySpot(designer);
            designer.Document.AddElement(new EmptyCell(thirdRule.Location.X + 50, thirdRule.Location.Y + 50));
            designer.Document.AddElement(new EmptyCell(thirdRule.Location.X + 50, thirdRule.Location.Y + 100));
            designer.Document.AddElement(new BirdCell(thirdRule.Location.X + 50, thirdRule.Location.Y + 50, Direction.Down));
            designer.Document.AddElement(new EmptyCell(thirdRule.Location.X + 250, thirdRule.Location.Y + 50));
            designer.Document.AddElement(new EmptyCell(thirdRule.Location.X + 250, thirdRule.Location.Y + 100));
            designer.Document.AddElement(new BirdCell(thirdRule.Location.X + 250, thirdRule.Location.Y + 100, Direction.Down));
        }
    }
}
