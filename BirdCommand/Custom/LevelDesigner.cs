using BirdCommand.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using BirdCommand.Model;

namespace BirdCommand.Custom
{
    public class LevelDesigner
    {
        
        public static List<BaseElement> ProduceLevelElements(String boardString)
        {
            var result = new List<BaseElement>();
            var rows = boardString.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < rows.Count(); i++)
            {
                var cols = rows[i].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < cols.Count(); j++)
                {
                    var nextValue = cols[j];
                    switch (nextValue)
                    {
                        case "St":
                            result.Add(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.Stone));
                            break;
                        case "Wo":
                            result.Add(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.Wood));
                            break;
                        case "HW":
                            result.Add(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.HalfWood));
                            break;
                        case "HS":
                            result.Add(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.HalfStone));
                            break;
                        case "Gl":
                            result.Add(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.Glass));
                            break;
                        case "Tn":
                            result.Add(new BlockCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, BlockType.TNT));
                            break;
                        case "Em":
                            result.Add(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            break;
                        case "Bi":
                            result.Add(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            result.Add(new BirdCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            break;
                        case "BL":
                            result.Add(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            result.Add(new BirdCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, Direction.Left));
                            break;
                        case "BR":
                            result.Add(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            result.Add(new BirdCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, Direction.Right));
                            break;
                        case "BU":
                            result.Add(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            result.Add(new BirdCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, Direction.Up));
                            break;
                        case "BD":
                            result.Add(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            result.Add(new BirdCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE, Direction.Down));
                            break;
                        case "Pi":
                            result.Add(new EmptyCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            result.Add(new PigCell(j * BirdCommandMain.CELL_SIZE, i * BirdCommandMain.CELL_SIZE));
                            break;
                    }
                }
            }
            return result;
        }

        internal static (List<Compound>,int) ProduceIons(String boardString)
        {
            var result = new List<BaseElement>();
            var ions = new List<Compound>();
            var rows = boardString.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int uniqueElementsCount = rows.Count();
            for (int i = 0; i < rows.Count(); i++)
            {
                var cols = rows[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
                int amount = int.Parse(cols[0]);
                string symbol = cols[1];
                for(int j=0; j < amount; j++)
                { 
                     switch(symbol)
                    {
                        case "Na":
                            ions.Add( new Compound { ions = new List<Ion> { new Sodium() } });
                            break;
                        case "Cl":
                            ions.Add(new Compound { ions = new List<Ion> { new Chlorine() } });
                            break;
                        case "NaCl":
                            ions.Add(new Compound { ions = new List<Ion> { new Sodium(), new Chlorine() } });
                            break;
                    }

                }
            }
            return (ions, uniqueElementsCount);
        }

        internal static List<IonCell> PlaceIonCells(List<Compound> compounds,int uniqueCount)
        {
            List<IonCell> ionCells = new List<IonCell>();
            Dictionary<String, int> compoundYoffsetDict = new Dictionary<String, int>();
            int elementsPlaced = 0;
            foreach (var compound in compounds)
            {
                if(!compoundYoffsetDict.ContainsKey(compound.Symbol))
                {
                    compoundYoffsetDict.Add(compound.Symbol, 0);
                    elementsPlaced +=1;
                }
                int xCoord = elementsPlaced * (350 / (uniqueCount +1));
                compoundYoffsetDict.TryGetValue(compound.Symbol,out int compoundYoffset);
                IonCell ioncell = new IonCell(xCoord, compoundYoffset, compound);
                ionCells.Add(ioncell);
                compoundYoffsetDict[compound.Symbol] = compoundYoffset + 5;
            }
            return ionCells;
        }

        public static void GenericLevelDesign(Designer theBoard, String boardString)
        {
            var cells = ProduceLevelElements(boardString);
            foreach (var cell in cells)
            {
                theBoard.Document.AddElement(cell);
            }
            
            theBoard.Document.BringToFrontElement(theBoard.Document.Elements.GetArray().Where(e => e is PigCell).First());
            theBoard.Document.BringToFrontElement(theBoard.Document.Elements.GetArray().Where(e => e is BirdCell).First());
        }

        public static void GenericLabLevelDesign(Designer theBoard,String boardString)
        {
            var (cells,uniqueCount) = ProduceIons(boardString);
            var placedCells = PlaceIonCells(cells,uniqueCount);

            foreach (var cell in placedCells)
            {
                theBoard.Document.AddElement(cell);
            }

             theBoard.Document.BringToFrontManyElements(theBoard.Document.Elements.GetArray().Where(e => e is IonCell).ToList());
        }

        public static void Level1(Designer theBoard)
        {
            GenericLabLevelDesign(theBoard,Resources.Lab1);
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
