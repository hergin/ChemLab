using BirdCommand.Custom;
using BirdCommand.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BirdCommandTests.Custom
{

    [TestClass]
    public class LevelDesignerTests
    {

        [TestMethod]
        public void PlaceTwoCompoundsEvenly()
        {
            List<Compound> compounds = new List<Compound> { new Compound { ions = new List<Ion> { new Sodium() } }, new Compound { ions = new List<Ion> { new Chlorine() } } };

            List<IonCell> placedCells = LevelDesigner.PlaceIonCells(compounds, 2);
            int oneThirdxCoord = 400 / 3;
            Assert.AreEqual(oneThirdxCoord - compounds[0].GetHalfWidth(), placedCells[0].Location.X);
            Assert.AreEqual((oneThirdxCoord * 2) - compounds[1].GetHalfWidth(), placedCells[1].Location.X);

        }
        [TestMethod]
        public void PlaceThreeCompoundsEvenly()
        {
            List<Compound> compounds = new List<Compound>
            {
            new Compound { ions = new List<Ion> { new Sodium() } },
            new Compound { ions = new List<Ion> { new Chlorine() } },
            new Compound {ions = new List<Ion> {new Hyrdogen()}}
             };

            List<IonCell> placedCells = LevelDesigner.PlaceIonCells(compounds, 3);
            int oneForthxCoord = 400 / 4;
            Assert.AreEqual(oneForthxCoord - compounds[0].GetHalfWidth(), placedCells[0].Location.X);
            Assert.AreEqual((oneForthxCoord * 2) - compounds[1].GetHalfWidth(), placedCells[1].Location.X);
            Assert.AreEqual((oneForthxCoord * 3) - compounds[2].GetHalfWidth(), placedCells[2].Location.X);

        }

        [TestMethod]
        public void PlaceThreeCompoundsTwoAreUnique()
        {
            List<Compound> compounds = new List<Compound>
            { new Compound { ions = new List<Ion> { new Sodium() } },
            new Compound { ions = new List<Ion> { new Chlorine() } },
            new Compound { ions = new List<Ion> { new Chlorine() } },
            };

            List<IonCell> placedCells = LevelDesigner.PlaceIonCells(compounds, 2);
            int oneThirdxCoord = 400 / 3;
            int initialYValue = 5;
            Assert.AreEqual(oneThirdxCoord - compounds[0].GetHalfWidth(), placedCells[0].Location.X);
            Assert.AreEqual(oneThirdxCoord * 2 - compounds[1].GetHalfWidth(), placedCells[1].Location.X);
            Assert.AreEqual(initialYValue, placedCells[1].Location.Y);
            Assert.AreEqual(oneThirdxCoord * 2 - compounds[2].GetHalfWidth(), placedCells[2].Location.X);
            Assert.AreEqual(placedCells[1].Location.Y + 5, placedCells[2].Location.Y);
        }

    }
}
