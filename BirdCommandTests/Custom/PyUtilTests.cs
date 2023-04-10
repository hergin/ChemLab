using Microsoft.VisualStudio.TestTools.UnitTesting;
using BirdCommand.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdCommandTests.Properties;
using Dalssoft.DiagramNet;
using System.IO;
using BirdCommand.Model;

namespace BirdCommand.Custom.Tests
{
    [TestClass()]
    public class PyUtilTests
    {
        [TestInitialize()]
        public void ensurePY()
        {
            Assert.IsTrue(Directory.Exists(@"Resources\WPy64-39100\"), @"Make sure to extract portable python under Resources under bin folder! It should be like bin\Resources\WPy64-39100\");
        }

        [TestMethod()]
        public void IsPatternInTheModelChemistry_Test_SamePattern()
        {
            List<BaseElement> model = new List<BaseElement>();
            model.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            model.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));

            List<BaseElement> ruleElements = new List<BaseElement>();
            ruleElements.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            ruleElements.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));

            Assert.IsTrue(PyUtil.IsPatternInTheModelChemistry(model, ruleElements));
        }

        [TestMethod()]
        public void IsPatternInTheModelChemistry_Test_SameWithDifferentLocations()
        {
            List<BaseElement> model = new List<BaseElement>();
            model.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            model.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));

            List<BaseElement> ruleElements = new List<BaseElement>();
            ruleElements.Add(new IonCell(500, 50, new List<Ion> { new Sodium() }));
            ruleElements.Add(new IonCell(1000, 50, new List<Ion> { new Chlorine() }));

            Assert.IsTrue(PyUtil.IsPatternInTheModelChemistry(model, ruleElements));
        }

        [TestMethod()]
        public void IsPatternInTheModelChemistry_Test_LessElementsInRule()
        {
            List<BaseElement> model = new List<BaseElement>();
            model.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            model.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));

            List<BaseElement> ruleElements = new List<BaseElement>();
            ruleElements.Add(new IonCell(1000, 50, new List<Ion> { new Chlorine() }));

            Assert.IsTrue(PyUtil.IsPatternInTheModelChemistry(model, ruleElements));
        }

        [TestMethod()]
        public void IsPatternInTheModelChemistry_Test_LessElementsInModel()
        {
            List<BaseElement> model = new List<BaseElement>();
            model.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));

            List<BaseElement> ruleElements = new List<BaseElement>();
            ruleElements.Add(new IonCell(500, 50, new List<Ion> { new Sodium() }));
            ruleElements.Add(new IonCell(1000, 50, new List<Ion> { new Chlorine() }));

            Assert.IsFalse(PyUtil.IsPatternInTheModelChemistry(model, ruleElements));
        }

        [TestMethod()]
        public void IsPatternInTheModelTest()
        {
            var elementsOfSmallMaze = LevelDesigner.ProduceLevelElements(Resources.small_maze);
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(0, 50));
            lhs.Add(new BirdCell(0, 0));

            Assert.IsTrue(PyUtil.IsPatternInTheModel(elementsOfSmallMaze, lhs));
        }

        [TestMethod()]
        public void CallPythonTest()
        {
            var result = PyUtil.CallPython("hello.py", "world");
            Assert.AreEqual("Hello, world!", result.Trim());
        }

        [TestMethod()]
        public void FindChangesInTheRuleChemistry_TestAddDelete()
        {
            List<BaseElement> ruleLhs = new List<BaseElement>();
            ruleLhs.Add(new IonCell(1000, 50, new List<Ion> { new Chlorine() }));

            List<BaseElement> ruleRhs = new List<BaseElement>();
            ruleRhs.Add(new IonCell(1000, 50, new List<Ion> { new Sodium() }));

            var result = PyUtil.FindChangesInTheRuleChemistry(ruleLhs,ruleRhs);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(ChangeStepType.Add, result[0].Type);
            Assert.AreEqual("Na", result[0].Compound.Symbol);
            Assert.AreEqual(ChangeStepType.Delete, result[1].Type);
            Assert.AreEqual("Cl", result[1].Compound.Symbol);
        }

        [TestMethod()]
        public void FindChangesInTheRuleChemistry_Test_NoChange()
        {
            List<BaseElement> ruleLhs = new List<BaseElement>();
            ruleLhs.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            ruleLhs.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));

            List<BaseElement> ruleRhs = new List<BaseElement>();
            ruleRhs.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            ruleRhs.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));

            var result = PyUtil.FindChangesInTheRuleChemistry(ruleLhs, ruleRhs);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void FindChangesInTheRuleChemistry_Test_NoChangeInSomeElements()
        {
            List<BaseElement> ruleLhs = new List<BaseElement>();
            ruleLhs.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            ruleLhs.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));

            List<BaseElement> ruleRhs = new List<BaseElement>();
            ruleRhs.Add(new IonCell(50, 50, new List<Ion> { new Chlorine() }));
            ruleRhs.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));

            var result = PyUtil.FindChangesInTheRuleChemistry(ruleLhs, ruleRhs);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod()]
        public void FindChangesToTheBirdInTheRuleTest()
        {
            var ruleAndItsContents = new List<BaseElement>();
            RuleCell firstRule = new RuleCell(300, 20);
            ruleAndItsContents.Add(firstRule);
            ruleAndItsContents.Add(new EmptyCell(firstRule.Location.X + 50, firstRule.Location.Y + 50));
            ruleAndItsContents.Add(new EmptyCell(firstRule.Location.X + 100, firstRule.Location.Y + 50));
            ruleAndItsContents.Add(new BirdCell(firstRule.Location.X + 50, firstRule.Location.Y + 50, Direction.Right));
            ruleAndItsContents.Add(new EmptyCell(firstRule.Location.X + 250, firstRule.Location.Y + 50));
            ruleAndItsContents.Add(new EmptyCell(firstRule.Location.X + 300, firstRule.Location.Y + 50));
            ruleAndItsContents.Add(new BirdCell(firstRule.Location.X + 300, firstRule.Location.Y + 50, Direction.Down));

            var result = PyUtil.FindChangesToTheBirdInTheRule(ruleAndItsContents, firstRule);
            Assert.AreEqual(50, result.Item1.X);
            Assert.AreEqual(0, result.Item1.Y);
            Assert.AreEqual(Direction.Down, result.Item2);
        }
    }
}