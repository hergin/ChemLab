using BirdCommand.Model;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
            if (changeStep.Type == ChangeStepType.Add)
            {
                IonCell ionCell = new IonCell((placementXOffset % 250), 260 + placementYOffset, changeStep.Compound.ions);
                placementXOffset += 5;
                placementYOffset += 5;
                designer.Document.AddElement(ionCell);
                return true;
            }
            if (changeStep.Type == ChangeStepType.Delete)
            {
                BaseElement foundElement = designer.Document.Elements.GetArray().Where(x => x.Name == changeStep.Compound.Symbol).First();

                if (foundElement == null)
                {
                    return false;
                }
                designer.Document.DeleteElement(foundElement);
                return true;
            }
            return false;

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

        internal static void MoveReactionAndItsContents(Designer designer, ReactionCell reaction, int newX, int newY)
        {
            var reactionStarterPosition = reaction.Location;

            var reactionContent = DesignerUtil.FindElementsWithin(designer, reaction);
            foreach (var element in reactionContent)
            {
                // move the contents of the rule as well
                var differenceX = Math.Abs(element.Location.X - reactionStarterPosition.X);
                var differenceY = Math.Abs(element.Location.Y - reactionStarterPosition.Y);
                element.Location = new Point(newX + differenceX, newY + differenceY);
            }
        }

        // Return the elements outside the block panel (skip Snap and Start cells as well)
        internal static List<BaseElement> GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(Designer designer)
        {
            var allElements = designer.Document.Elements.GetArray();

            // Find the blockpanel if exists (it is type of RectangleNode)
            var possibleBlockPanel = allElements.Where(e => e is RectangleNode).FirstOrDefault();
            if (possibleBlockPanel != null)
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
                    && (leftBorder < result.Location.X + result.Size.Width && rightBorder > result.Location.X
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
            var ionCells = designer.Document.Elements.GetArray().Where(e => e is IonCell);
            foreach (var ionCell in ionCells)
            {
                designer.Document.BringToFrontElement(ionCell);
            }
        }

    }
}
