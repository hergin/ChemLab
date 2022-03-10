using BirdCommand.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    public class LevelDesigner
    {
        public static void GenericLevelDesign(Designer theBoard, String boardString)
        {
            var rows = boardString.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 8; i++)
            {
                var cols = rows[i].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < 8; j++)
                {
                    var nextValue = cols[j];
                    switch (nextValue)
                    {
                        case "St":
                            theBoard.Document.AddElement(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.Stone));
                            break;
                        case "Wo":
                            theBoard.Document.AddElement(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.Wood));
                            break;
                        case "HW":
                            theBoard.Document.AddElement(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.HalfWood));
                            break;
                        case "HS":
                            theBoard.Document.AddElement(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.HalfStone));
                            break;
                        case "Gl":
                            theBoard.Document.AddElement(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.Glass));
                            break;
                        case "Tn":
                            theBoard.Document.AddElement(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.TNT));
                            break;
                        case "Em":
                            theBoard.Document.AddElement(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            break;
                        case "Bi":
                            theBoard.Document.AddElement(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            theBoard.Document.AddElement(new BirdCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            break;
                        case "BL":
                            theBoard.Document.AddElement(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            theBoard.Document.AddElement(new BirdCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE,Direction.Left));
                            break;
                        case "BR":
                            theBoard.Document.AddElement(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            theBoard.Document.AddElement(new BirdCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, Direction.Right));
                            break;
                        case "BU":
                            theBoard.Document.AddElement(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            theBoard.Document.AddElement(new BirdCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, Direction.Up));
                            break;
                        case "BD":
                            theBoard.Document.AddElement(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            theBoard.Document.AddElement(new BirdCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, Direction.Down));
                            break;
                        case "Pi":
                            theBoard.Document.AddElement(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            theBoard.Document.AddElement(new PigCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            break;
                    }
                }
            }

            theBoard.Document.BringToFrontElement(theBoard.Document.Elements.GetArray().Where(e => e is PigCell).First());
            theBoard.Document.BringToFrontElement(theBoard.Document.Elements.GetArray().Where(e => e is BirdCell).First());
        }

        public static void Level1(Designer theBoard)
        {
            GenericLevelDesign(theBoard, Resources.hoc1);
        }

        public static void Level2(Designer theBoard)
        {
            GenericLevelDesign(theBoard, Resources.hoc2);
        }

        public static void Level3(Designer theBoard)
        {
            GenericLevelDesign(theBoard, Resources.hoc3);
        }
    }
}
