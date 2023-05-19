using ChemLab.Model;
using ChemLab.Model.Ions;
using Dalssoft.DiagramNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ChemLab.Custom.Tests
{
    [TestClass()]
    public class TrafoUtilTests
    {


        [TestMethod()]
        public void FindPreConditionElementsChemistry_Test()
        {
            List<BaseElement> allElements = new List<BaseElement>();
            var rule = new ReactionCell(0, 0);
            allElements.Add(rule);
            allElements.Add(new IonCell(50, 10, new List<Ion> { new Sodium() }));
            allElements.Add(new IonCell(100, 10, new List<Ion> { new Chlorine() }));

            Assert.AreEqual(2, TrafoUtil.FindPreConditionElementsChemistry(allElements, rule).Count);
        }

        [TestMethod()]
        public void FindPostConditionElementsChemistry_Test()
        {
            List<BaseElement> allElements = new List<BaseElement>();
            var rule = new ReactionCell(0, 0);
            allElements.Add(rule);
            allElements.Add(new IonCell(50, 10, new List<Ion> { new Sodium() }));
            allElements.Add(new IonCell(100, 10, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(400, 10, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(400, 10, new List<Ion> { new Chlorine() }));

            Assert.AreEqual(2, TrafoUtil.FindPostConditionElementsChemistry(allElements, rule).Count);
        }

        [TestMethod()]
        public void FindPostConditionElementsChemistry_Test2()
        {
            List<BaseElement> allElements = new List<BaseElement>();
            var rule = new ReactionCell(0, 0);
            allElements.Add(rule);
            allElements.Add(new IonCell(50, 10, new List<Ion> { new Sodium() }));
            allElements.Add(new IonCell(100, 10, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(400, 10, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(400, 10, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(350, 10, new List<Ion> { new Chlorine(), new Sodium() }));

            Assert.AreEqual(3, TrafoUtil.FindPostConditionElementsChemistry(allElements, rule).Count);
        }

    }
}