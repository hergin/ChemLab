using Microsoft.VisualStudio.TestTools.UnitTesting;
using BirdCommand.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalssoft.DiagramNet;

namespace BirdCommand.Custom.Tests
{
    [TestClass()]
    public class ConvertUtilTests
    {
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
            Assert.AreEqual(1, result.Edges.Count);
            Assert.AreEqual("0_0", result.Edges[0].From);
            Assert.AreEqual("0_50", result.Edges[0].To);

            //  X X
            //  X
            lhs.Add(new EmptyCell(50, 0));
            result = ConvertUtil.PatternToGraph(lhs);

            Assert.AreEqual(3, result.Nodes.Count);
            Assert.AreEqual(2, result.Edges.Count);

            //  X X
            //  X X
            lhs.Add(new EmptyCell(50, 50));
            result = ConvertUtil.PatternToGraph(lhs);

            Assert.AreEqual(4, result.Nodes.Count);
            Assert.AreEqual(4, result.Edges.Count);

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
            Assert.AreEqual(2, result.Edges.Count);
            Assert.AreEqual("0_0", result.Edges[0].From);
            Assert.AreEqual("0_50", result.Edges[0].To);
            Assert.AreEqual("0_50", result.Edges[1].From);
            Assert.AreEqual("0_100", result.Edges[1].To);

            //  X
            //  X
            //  X
            //  X
            lhs.Add(new EmptyCell(0, 150));

            result = ConvertUtil.PatternToGraph(lhs);

            Assert.AreEqual(4, result.Nodes.Count);
            Assert.AreEqual(3, result.Edges.Count);
        }
    }
}