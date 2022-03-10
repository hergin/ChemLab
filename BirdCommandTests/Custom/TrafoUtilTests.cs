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
    }
}