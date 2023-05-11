using ChemLab.Model;
using Dalssoft.DiagramNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ChemLab.Custom.Tests
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
    }
}