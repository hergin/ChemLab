using Microsoft.VisualStudio.TestTools.UnitTesting;
using BirdCommand.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalssoft.DiagramNet;
using BirdCommand.Model;

namespace BirdCommand.Custom.Tests
{
    [TestClass()]
    public class TrafoUtilTests
    {
        [TestMethod()]
        public void PatternMatchTests1()
        {
            List<BaseElement> model = new List<BaseElement>();
            model.Add(new EmptyCell(0, 0));
            model.Add(new EmptyCell(50, 0));
            model.Add(new EmptyCell(0, 50));
            model.Add(new BirdCell(0, 50));
            model.Add(new EmptyCell(50, 50));

            List<BaseElement> pattern = new List<BaseElement>();
            pattern.Add(new EmptyCell(0, 0));
            pattern.Add(new BirdCell(0, 0));

            Assert.IsTrue(TrafoUtil.DoesPatternExist(model, pattern));
        }

        [TestMethod()]
        public void PatternMatchTests2()
        {
            List<BaseElement> model = new List<BaseElement>();
            model.Add(new EmptyCell(0, 0));
            model.Add(new EmptyCell(50, 0));
            model.Add(new EmptyCell(0, 50));
            model.Add(new BirdCell(50, 0));
            model.Add(new EmptyCell(50, 50));

            List<BaseElement> pattern = new List<BaseElement>();
            pattern.Add(new EmptyCell(0, 0));
            pattern.Add(new EmptyCell(50, 0));
            pattern.Add(new BirdCell(50, 0));

            Assert.IsTrue(TrafoUtil.DoesPatternExist(model, pattern));
        }

        [TestMethod()]
        public void PatternMatchTests3()
        {
            List<BaseElement> model = new List<BaseElement>();
            model.Add(new EmptyCell(0, 0));
            model.Add(new EmptyCell(50, 0));
            model.Add(new EmptyCell(0, 50));
            model.Add(new BirdCell(50, 0));
            model.Add(new EmptyCell(50, 50));

            List<BaseElement> pattern = new List<BaseElement>();
            pattern.Add(new EmptyCell(0, 50));
            pattern.Add(new EmptyCell(50, 50));
            pattern.Add(new BirdCell(50, 50));

            Assert.IsTrue(TrafoUtil.DoesPatternExist(model, pattern));
        }

        [TestMethod()]
        public void PatternMatchTests4()
        {
            List<BaseElement> model = new List<BaseElement>();
            model.Add(new EmptyCell(0, 0));
            model.Add(new EmptyCell(50, 0));
            model.Add(new EmptyCell(0, 50));
            model.Add(new BirdCell(50, 0));
            model.Add(new EmptyCell(50, 50));

            List<BaseElement> pattern = new List<BaseElement>();
            pattern.Add(new EmptyCell(0, 50));
            pattern.Add(new EmptyCell(50, 50));
            pattern.Add(new BirdCell(0, 50));

            Assert.IsFalse(TrafoUtil.DoesPatternExist(model, pattern));
        }

        [TestMethod()]
        public void IdentifyRuleTypeTests1()
        {
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(0, 50));
            lhs.Add(new BirdCell(0, 0));

            List<BaseElement> rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new EmptyCell(0, 50));
            rhs.Add(new BirdCell(0, 0));

            Assert.AreEqual(RuleType.NotSupportedYet, TrafoUtil.IdentifyRuleType(lhs, rhs));
        }

        [TestMethod()]
        public void IdentifyRuleTypeTests2()
        {
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(0, 50));
            lhs.Add(new BirdCell(0, 0));

            List<BaseElement> rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new EmptyCell(0, 50));
            rhs.Add(new BirdCell(0, 50));

            Assert.AreEqual(RuleType.MoveForward, TrafoUtil.IdentifyRuleType(lhs, rhs));
        }

        [TestMethod()]
        public void IdentifyRuleTypeTests_DifferentNumberOfLhsAndRhs()
        {
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(0, 50));
            lhs.Add(new EmptyCell(50, 0));
            lhs.Add(new BirdCell(0, 0));

            List<BaseElement> rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new EmptyCell(0, 50));
            rhs.Add(new BirdCell(0, 50));

            Assert.ThrowsException<Exception>(() => { TrafoUtil.IdentifyRuleType(lhs, rhs); });
        }

        [TestMethod()]
        public void IdentifyRuleTypeTests_MoreThanOneBird()
        {
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(0, 50));
            lhs.Add(new EmptyCell(50, 0));
            lhs.Add(new BirdCell(0, 0));

            List<BaseElement> rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new EmptyCell(0, 50));
            rhs.Add(new BirdCell(0, 50));
            rhs.Add(new BirdCell(0, 50));

            Assert.ThrowsException<Exception>(() => { TrafoUtil.IdentifyRuleType(lhs, rhs); });
        }

        [TestMethod()]
        public void IdentifyRuleTypeTests_TurnRight()
        {
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0));

            List<BaseElement> rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Left));

            Assert.AreEqual(RuleType.TurnRight, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0, Direction.Up));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Right));

            Assert.AreEqual(RuleType.TurnRight, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0, Direction.Right));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Down));

            Assert.AreEqual(RuleType.TurnRight, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0, Direction.Left));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Up));

            Assert.AreEqual(RuleType.TurnRight, TrafoUtil.IdentifyRuleType(lhs, rhs));
        }

        [TestMethod()]
        public void IdentifyRuleTypeTests_Turns_onlyOneBird()
        {
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new BirdCell(0, 0));

            List<BaseElement> rhs = new List<BaseElement>();
            rhs.Add(new BirdCell(0, 0, Direction.Right));

            Assert.AreEqual(RuleType.TurnLeft, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new BirdCell(0, 0, Direction.Up));

            rhs = new List<BaseElement>();
            rhs.Add(new BirdCell(0, 0, Direction.Left));

            Assert.AreEqual(RuleType.TurnLeft, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new BirdCell(0, 0, Direction.Left));

            rhs = new List<BaseElement>();
            rhs.Add(new BirdCell(0, 0, Direction.Up));

            Assert.AreEqual(RuleType.TurnRight, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new BirdCell(0, 0, Direction.Right));

            rhs = new List<BaseElement>();
            rhs.Add(new BirdCell(0, 0, Direction.Left));

            Assert.AreEqual(RuleType.Turn180, TrafoUtil.IdentifyRuleType(lhs, rhs));
        }

        [TestMethod()]
        public void IdentifyRuleTypeTests_TurnLeft()
        {
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0));

            List<BaseElement> rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Right));

            Assert.AreEqual(RuleType.TurnLeft, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0, Direction.Up));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Left));

            Assert.AreEqual(RuleType.TurnLeft, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0, Direction.Right));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Up));

            Assert.AreEqual(RuleType.TurnLeft, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0, Direction.Left));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Down));

            Assert.AreEqual(RuleType.TurnLeft, TrafoUtil.IdentifyRuleType(lhs, rhs));
        }

        [TestMethod()]
        public void IdentifyRuleTypeTests_Turn180()
        {
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0));

            List<BaseElement> rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Up));

            Assert.AreEqual(RuleType.Turn180, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0, Direction.Right));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Left));

            Assert.AreEqual(RuleType.Turn180, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0, Direction.Right));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Left));

            Assert.AreEqual(RuleType.Turn180, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new BirdCell(0, 0, Direction.Up));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Down));

            Assert.AreEqual(RuleType.Turn180, TrafoUtil.IdentifyRuleType(lhs, rhs));
        }

        [TestMethod()]
        public void IdentifyRuleTypeTests_MoveForward()
        {
            List<BaseElement> lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(0, 50));
            lhs.Add(new BirdCell(0, 0, Direction.Down));

            List<BaseElement> rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new EmptyCell(0, 50));
            rhs.Add(new BirdCell(0, 50, Direction.Down));

            Assert.AreEqual(RuleType.MoveForward, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(0, 50));
            lhs.Add(new BirdCell(0, 50, Direction.Up));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new EmptyCell(0, 50));
            rhs.Add(new BirdCell(0, 0, Direction.Up));

            Assert.AreEqual(RuleType.MoveForward, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(50, 0));
            lhs.Add(new BirdCell(0, 0, Direction.Right));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new EmptyCell(50, 0));
            rhs.Add(new BirdCell(50, 0, Direction.Right));

            Assert.AreEqual(RuleType.MoveForward, TrafoUtil.IdentifyRuleType(lhs, rhs));

            lhs = new List<BaseElement>();
            lhs.Add(new EmptyCell(0, 0));
            lhs.Add(new EmptyCell(50, 0));
            lhs.Add(new BirdCell(50, 0, Direction.Left));

            rhs = new List<BaseElement>();
            rhs.Add(new EmptyCell(0, 0));
            rhs.Add(new EmptyCell(50, 0));
            rhs.Add(new BirdCell(0, 0, Direction.Left));

            Assert.AreEqual(RuleType.MoveForward, TrafoUtil.IdentifyRuleType(lhs, rhs));
        }

        [TestMethod()]
        public void FindPreConditionElementsChemistry_Test()
        {
            List<BaseElement> allElements = new List<BaseElement>();
            var rule = new RuleCell(0, 0);
            allElements.Add(rule);
            allElements.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            allElements.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));

            Assert.AreEqual(2, TrafoUtil.FindPreConditionElementsChemistry(allElements, rule).Count);
        }

        [TestMethod()]
        public void FindPostConditionElementsChemistry_Test()
        {
            List<BaseElement> allElements = new List<BaseElement>();
            var rule = new RuleCell(0, 0);
            allElements.Add(rule);
            allElements.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            allElements.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(300, 50, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(300, 50, new List<Ion> { new Chlorine() }));

            Assert.AreEqual(2, TrafoUtil.FindPostConditionElementsChemistry(allElements, rule).Count);
        }

        [TestMethod()]
        public void FindPostConditionElementsChemistry_Test2()
        {
            List<BaseElement> allElements = new List<BaseElement>();
            var rule = new RuleCell(0, 0);
            allElements.Add(rule);
            allElements.Add(new IonCell(50, 50, new List<Ion> { new Sodium() }));
            allElements.Add(new IonCell(100, 50, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(300, 50, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(300, 50, new List<Ion> { new Chlorine() }));
            allElements.Add(new IonCell(250, 50, new List<Ion> { new Chlorine(), new Sodium() }));

            Assert.AreEqual(3, TrafoUtil.FindPostConditionElementsChemistry(allElements, rule).Count);
        }

        [TestMethod()]
        public void FindPreConditionElementsTest()
        {
            List<BaseElement> allElements = new List<BaseElement>();
            var rule = new RuleCell(0, 0);
            allElements.Add(rule);
            allElements.Add(new EmptyCell(50, 50));
            allElements.Add(new EmptyCell(100, 50));
            allElements.Add(new EmptyCell(50, 100));
            allElements.Add(new BirdCell(50, 50));
            allElements.Add(new EmptyCell(100, 100));
            allElements.Add(new EmptyCell(700, 700));
            allElements.Add(new EmptyCell(300, 100));
            allElements.Add(new BirdCell(350, 100));

            Assert.AreEqual(5, TrafoUtil.FindPreConditionElements(allElements, rule).Count);
        }

        [TestMethod()]
        public void FindPostConditionElementsTest()
        {
            List<BaseElement> allElements = new List<BaseElement>();
            var rule = new RuleCell(0, 0);
            allElements.Add(rule);
            allElements.Add(new EmptyCell(50, 50));
            allElements.Add(new EmptyCell(100, 50));
            allElements.Add(new EmptyCell(50, 100));
            allElements.Add(new BirdCell(50, 50));
            allElements.Add(new EmptyCell(100, 100));
            allElements.Add(new EmptyCell(700, 700));
            allElements.Add(new EmptyCell(300, 100));
            allElements.Add(new BirdCell(350, 100));

            Assert.AreEqual(2, TrafoUtil.FindPostConditionElements(allElements, rule).Count);
        }
    }
}