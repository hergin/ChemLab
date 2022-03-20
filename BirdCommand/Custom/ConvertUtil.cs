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
        public static Graph PatternToGraph(List<BaseElement> elements)
        {
            var result = new Graph();
            var allEmptyAndBlockCells = elements.Where(e => e is EmptyCell || e is BlockCell);
            var adjustX = elements.Select(e => e.Location.X).Min();
            var adjustY = elements.Select(e => e.Location.Y).Min();

            foreach (var element in allEmptyAndBlockCells)
            {
                var elementId = IdFromLocation(element.Location,adjustX,adjustY);
                result.Add(new Node() { Id = elementId, Type = GetShortTypeOf(element) });
                
                var cellAbove = allEmptyAndBlockCells.Where(empty => empty.Location.X == element.Location.X && empty.Location.Y == element.Location.Y - BirdCommandMain.CELL_SIZE).FirstOrDefault();
                if (cellAbove != null)
                {
                    result.Add(new Edge() { From = elementId, To = IdFromLocation(cellAbove.Location, adjustX, adjustY), Type="Up" });
                }
                var cellBelow = allEmptyAndBlockCells.Where(empty => empty.Location.X == element.Location.X && empty.Location.Y == element.Location.Y + BirdCommandMain.CELL_SIZE).FirstOrDefault();
                if (cellBelow != null)
                {
                    result.Add(new Edge() { From = elementId, To = IdFromLocation(cellBelow.Location, adjustX, adjustY), Type = "Down" });
                }
                var cellRight = allEmptyAndBlockCells.Where(empty => empty.Location.X == element.Location.X + BirdCommandMain.CELL_SIZE && empty.Location.Y == element.Location.Y).FirstOrDefault();
                if (cellRight != null)
                {
                    result.Add(new Edge() { From = elementId, To = IdFromLocation(cellRight.Location, adjustX, adjustY), Type = "Right" });
                }
                var cellLeft = allEmptyAndBlockCells.Where(empty => empty.Location.X == element.Location.X - BirdCommandMain.CELL_SIZE && empty.Location.Y == element.Location.Y).FirstOrDefault();
                if (cellLeft != null)
                {
                    result.Add(new Edge() { From = elementId, To = IdFromLocation(cellLeft.Location, adjustX, adjustY), Type = "Left" });
                }
            }

            var possibleBird = elements.Where(e => e is BirdCell).FirstOrDefault();
            if (possibleBird != null)
            {
                var bird = possibleBird as BirdCell;
                var locationIdUnderBird = IdFromLocation(bird.Location, adjustX, adjustY);
                var birdId = "Bird_" + bird.Direction + "_" + IdFromLocation(bird.Location, adjustX, adjustY);
                result.Add(new Node() { Id = birdId, Type = GetShortTypeOf(bird) });
                if (result.Nodes.Where(n => n.Id == birdId).Count() > 0)
                    result.Add(new Edge() { From = locationIdUnderBird, To = birdId, Type = "Bird_" + bird.Direction });
            }

            var possiblePig = elements.Where(e => e is PigCell).FirstOrDefault();
            if (possiblePig != null)
            {
                var pig = possiblePig as PigCell;
                var locationIdUnderPig = IdFromLocation(pig.Location, adjustX, adjustY);
                var pigId = "Pig_" + IdFromLocation(pig.Location, adjustX, adjustY);
                result.Add(new Node() { Id = pigId, Type = GetShortTypeOf(pig) });
                if (result.Nodes.Where(n => n.Id == pigId).Count() > 0)
                    result.Add(new Edge() { From = locationIdUnderPig, To = pigId, Type = "Pig" });
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

        public static String IdFromLocation(Point Location, int adjustX = 0, int adjustY = 0)
        {
            return (Location.X - adjustX) + "_" + (Location.Y - adjustY);
        }
    }
}
