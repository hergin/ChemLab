using Microsoft.VisualStudio.TestTools.UnitTesting;
using BirdCommand.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalssoft.DiagramNet;
using BirdCommandTests.Properties;
using BirdCommand.Model;

namespace BirdCommand.Custom.Tests
{
    [TestClass()]
    public class ConvertUtilTests
    {
        [TestMethod()]
        public void PatternToGraphChemistryTest()
        {
            List<BaseElement> allElements = new List<BaseElement>();
            allElements.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            allElements.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(300, 50, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(300, 50, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(250, 50, new List<Ion> { new Chlorine(), new Sodium() }));

            var result = ConvertUtil.PatternToGraphChemistry(allElements);
            Assert.AreEqual(0, result.Edges.Count);
            Assert.AreEqual(5, result.Nodes.Count);

            Assert.AreEqual("Na", result.Nodes[0].Type);
            Assert.AreEqual("ClNa", result.Nodes[4].Type);
        }

        [TestMethod()]
        public void PatternToGraphTest_OnlyBirdOrPig()
        {
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new BirdCell(30, 30));

            var result = ConvertUtil.PatternToGraph(lhs);

            Assert.AreEqual(0,result.Edges.Count);
            Assert.AreEqual(1,result.Nodes.Count);

            lhs = new List<BaseElement>();
            lhs.Add(new PigCell(30, 30));

            result = ConvertUtil.PatternToGraph(lhs);

            Assert.AreEqual(0, result.Edges.Count);
            Assert.AreEqual(1, result.Nodes.Count);
        }

        [TestMethod()]
        public void PatternToGraphTest_OnlyEmpty()
        {
            //   X
            //   X
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(0, 50));

            var result = ConvertUtil.PatternToGraph(lhs);

            Assert.AreEqual(2, result.Nodes.Count);
            Assert.AreEqual("0_0", result.Nodes[0].Id);
            Assert.AreEqual("0_50", result.Nodes[1].Id);
            Assert.AreEqual("Em", result.Nodes[0].Type);
            Assert.AreEqual(2, result.Edges.Count);
            Assert.AreEqual("0_0", result.Edges[0].From);
            Assert.AreEqual("0_50", result.Edges[0].To);
            Assert.AreEqual("Down", result.Edges[0].Type);
            Assert.AreEqual("0_50", result.Edges[1].From);
            Assert.AreEqual("0_0", result.Edges[1].To);
            Assert.AreEqual("Up", result.Edges[1].Type);

            //  X X
            //  X
            lhs.Add(new EmptyCell(50, 0));
            result = ConvertUtil.PatternToGraph(lhs);

            Assert.AreEqual(3, result.Nodes.Count);
            Assert.AreEqual(4, result.Edges.Count);

            //  X X
            //  X X
            lhs.Add(new EmptyCell(50, 50));
            result = ConvertUtil.PatternToGraph(lhs);

            Assert.AreEqual(4, result.Nodes.Count);
            Assert.AreEqual(8, result.Edges.Count);

            //  X
            //  X
            //  X
            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(0, 50));
            lhs.Add(new EmptyCell(0, 100));

            result = ConvertUtil.PatternToGraph(lhs);

            Assert.AreEqual(3, result.Nodes.Count);
            Assert.AreEqual("0_0", result.Nodes[0].Id);
            Assert.AreEqual("0_50", result.Nodes[1].Id);
            Assert.AreEqual("0_100", result.Nodes[2].Id);
            Assert.AreEqual("Em", result.Nodes[2].Type);
            Assert.AreEqual(4, result.Edges.Count);
            Assert.AreEqual("0_0", result.Edges[0].From);
            Assert.AreEqual("0_50", result.Edges[0].To);
            Assert.AreEqual("Down", result.Edges[0].Type);
            Assert.AreEqual("0_50", result.Edges[1].From);
            Assert.AreEqual("0_0", result.Edges[1].To);
            Assert.AreEqual("Up", result.Edges[1].Type);
            Assert.AreEqual("0_50", result.Edges[2].From);
            Assert.AreEqual("0_100", result.Edges[2].To);
            Assert.AreEqual("Down", result.Edges[2].Type);
            Assert.AreEqual("0_100", result.Edges[3].From);
            Assert.AreEqual("0_50", result.Edges[3].To);
            Assert.AreEqual("Up", result.Edges[3].Type);

            //  X
            //  X
            //  X
            //  X
            lhs.Add(new EmptyCell(0, 150));

            result = ConvertUtil.PatternToGraph(lhs);

            Assert.AreEqual(4, result.Nodes.Count);
            Assert.AreEqual(6, result.Edges.Count);
            Assert.AreEqual(3, result.Edges.Where(e => e.Type == "Down").Count());
        }

        [TestMethod()]
        public void PatternToGraphTest_Mazes()
        {
            var elementsOfSmallMaze = LevelDesigner.ProduceLevelElements(Resources.small_maze);
            var result = ConvertUtil.PatternToGraph(elementsOfSmallMaze);

            Assert.AreEqual(14, result.Nodes.Count);
            Assert.AreEqual(36, result.Edges.Count);

            var elementsOfMaze1 = LevelDesigner.ProduceLevelElements(Resources.hoc1);
            result = ConvertUtil.PatternToGraph(elementsOfMaze1);

            Assert.AreEqual(66, result.Nodes.Count, "Including bird and pig now!");
            Assert.AreEqual(226, result.Edges.Count);
        }
    }
}