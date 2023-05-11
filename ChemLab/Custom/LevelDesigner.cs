using ChemLab.Model;
using ChemLab.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChemLab.Custom
{
    public class LevelDesigner
    {

        internal static (List<Compound>, int) ProduceIons(String boardString)
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
                for (int j = 0; j < amount; j++)
                {
                    switch (symbol)
                    {
                        case "Na":
                            ions.Add(new Compound { ions = new List<Ion> { new Sodium() } });
                            break;
                        case "Cl":
                            ions.Add(new Compound { ions = new List<Ion> { new Chlorine() } });
                            break;
                        case "NaCl":
                            ions.Add(new Compound { ions = new List<Ion> { new Sodium(), new Chlorine() } });
                            break;
                        case "H":
                            ions.Add(new Compound { ions = new List<Ion> { new Hyrdogen() } });
                            break;
                    }

                }
            }
            return (ions, uniqueElementsCount);
        }

        internal static List<IonCell> PlaceIonCells(List<Compound> compounds, int uniqueCount)
        {
            List<IonCell> ionCells = new List<IonCell>();
            Dictionary<String, int> compoundYoffsetDict = new Dictionary<String, int>();
            int elementsPlaced = 0;
            foreach (var compound in compounds)
            {
                if (!compoundYoffsetDict.ContainsKey(compound.Symbol))
                {
                    compoundYoffsetDict.Add(compound.Symbol, 5);
                    elementsPlaced += 1;
                }
                int xCoord = elementsPlaced * (400 / (uniqueCount + 1)) - (compound.GetHalfWidth());
                compoundYoffsetDict.TryGetValue(compound.Symbol, out int compoundYoffset);
                IonCell ioncell = new IonCell(xCoord, compoundYoffset, compound);
                ionCells.Add(ioncell);
                compoundYoffsetDict[compound.Symbol] = compoundYoffset + 5;
            }
            return ionCells;
        }


        public static void GenericLabLevelDesign(Designer theBoard, String boardString)
        {
            var (cells, uniqueCount) = ProduceIons(boardString);
            var placedCells = PlaceIonCells(cells, uniqueCount);

            foreach (var cell in placedCells)
            {
                theBoard.Document.AddElement(cell);
            }

            theBoard.Document.BringToFrontManyElements(theBoard.Document.Elements.GetArray().Where(e => e is IonCell).ToList());
        }

        public static void Level1(Designer theBoard)
        {
            GenericLabLevelDesign(theBoard, Resources.Lab1);
        }

    }
}
