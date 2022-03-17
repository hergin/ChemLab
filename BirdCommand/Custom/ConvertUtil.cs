using BirdCommand.Properties;
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
        // TODO generic links between the blocks doesn't give the right result.
        //          There should be direction links.
        //          For example: this pattern exists in this model in the current system.
        //                       because it looks like 3 connected empty cells.
        //
        //                              E                    W-E-W
        //                              E-E                  W-E-W
        //                                                   W-E-W
        public static Graph PatternToGraph(List<BaseElement> elements)
        {
            var result = new Graph();
            var allEmptyAndBlockCells = elements.Where(e => e is EmptyCell || e is BlockCell);
            var handled = new List<BaseElement>();
            foreach (var element in allEmptyAndBlockCells)
            {
                result.Add(new Node() { Id = IdFromLocation(element.Location), Type = GetShortTypeOf(element) });
                
                var cellAbove = allEmptyAndBlockCells.Where(empty => empty.Location.X == element.Location.X && empty.Location.Y == element.Location.Y - BirdCommandMain.CELL_SIZE).FirstOrDefault();
                if (cellAbove != null && !handled.Contains(cellAbove))
                {
                    result.Add(new Edge() { From = IdFromLocation(element.Location), To = IdFromLocation(cellAbove.Location) });
                }
                var cellBelow = allEmptyAndBlockCells.Where(empty => empty.Location.X == element.Location.X && empty.Location.Y == element.Location.Y + BirdCommandMain.CELL_SIZE).FirstOrDefault();
                if (cellBelow != null && !handled.Contains(cellBelow))
                {
                    result.Add(new Edge() { From = IdFromLocation(element.Location), To = IdFromLocation(cellBelow.Location) });
                }
                var cellRight = allEmptyAndBlockCells.Where(empty => empty.Location.X == element.Location.X + BirdCommandMain.CELL_SIZE && empty.Location.Y == element.Location.Y).FirstOrDefault();
                if (cellRight != null && !handled.Contains(cellRight))
                {
                    result.Add(new Edge() { From = IdFromLocation(element.Location), To = IdFromLocation(cellRight.Location) });
                }
                var cellLeft = allEmptyAndBlockCells.Where(empty => empty.Location.X == element.Location.X - BirdCommandMain.CELL_SIZE && empty.Location.Y == element.Location.Y).FirstOrDefault();
                if (cellLeft != null && !handled.Contains(cellLeft))
                {
                    result.Add(new Edge() { From = IdFromLocation(element.Location), To = IdFromLocation(cellLeft.Location) });
                }

                handled.Add(element);
            }

            var possibleBird = elements.Where(e => e is BirdCell).FirstOrDefault();
            if (possibleBird != null)
            {
                var bird = possibleBird as BirdCell;
                result.Add(new Node() { Id = "Bird", Type = GetShortTypeOf(bird) });
                result.Add(new Edge() { From = IdFromLocation(bird.Location), To = "Bird" });
            }

            var possiblePig = elements.Where(e => e is PigCell).FirstOrDefault();
            if (possiblePig != null)
            {
                var pig = possiblePig as PigCell;
                result.Add(new Node() { Id = "Pig", Type = GetShortTypeOf(pig) });
                result.Add(new Edge() { From = IdFromLocation(pig.Location), To = "Pig" });
            }

            return result;
        }

        public static String GetShortTypeOf(BaseElement baseElement)
        {
            if (baseElement is EmptyCell cell)
                return "Em";
            else if (baseElement is BlockCell blockCell)
            {
                switch (blockCell.BlockType)
                {
                    case BlockType.Glass:
                        return "Gl";
                    case BlockType.HalfStone:
                        return "HS";
                    case BlockType.HalfWood:
                        return "HW";
                    case BlockType.Stone:
                        return "St";
                    case BlockType.TNT:
                        return "Tn";
                    case BlockType.Wood:
                        return "Wo";
                }
            }
            else if (baseElement is BirdCell birdCell)
            {
                switch (birdCell.Direction)
                {
                    case Direction.Up:
                        return "BU";
                    case Direction.Right:
                        return "BR";
                    case Direction.Down:
                        return "BD";
                    case Direction.Left:
                        return "BL";
                }
            }
            else if (baseElement is PigCell pigCell)
                return "Pi";
            return "";
        }

        public static String IdFromLocation(Point Location)
        {
            return Location.X + "_" + Location.Y;
        }
    }
}
