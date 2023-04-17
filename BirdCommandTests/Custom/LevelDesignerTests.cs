using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BirdCommand.Custom;
using BirdCommand.Model;

namespace BirdCommandTests.Custom
{

    [TestClass]
    public class LevelDesignerTests
    {

        [TestMethod]
        public void PlaceTwoCompoundsEvenly()
        {
             List<Compound> compounds = new List<Compound> { new Compound { ions = new List<Ion> { new Sodium() } }, new Compound { ions = new List<Ion> { new Chlorine() } } };
            
            List<IonCell> placedCells =  LevelDesigner.PlaceIonCells(compounds,2);
            int oneThirdxCoord = 350 / 3;
            Assert.AreEqual(oneThirdxCoord,placedCells[0].Location.X);
            Assert.AreEqual(oneThirdxCoord * 2,placedCells[1].Location.X);

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
            int oneForthxCoord = 350 / 4;
            Assert.AreEqual(oneForthxCoord, placedCells[0].Location.X);
            Assert.AreEqual(oneForthxCoord * 2, placedCells[1].Location.X);
            Assert.AreEqual(oneForthxCoord * 3, placedCells[2].Location.X);

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
            int oneThirdxCoord = 350 / 3;
            Assert.AreEqual(oneThirdxCoord, placedCells[0].Location.X);
            Assert.AreEqual(oneThirdxCoord * 2, placedCells[1].Location.X);
            Assert.AreEqual(0, placedCells[1].Location.Y);
            Assert.AreEqual(oneThirdxCoord * 2, placedCells[2].Location.X);
            Assert.AreEqual(placedCells[1].Location.Y +5, placedCells[2].Location.Y);
        }

    }
}
