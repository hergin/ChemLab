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