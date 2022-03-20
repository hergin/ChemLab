using Microsoft.VisualStudio.TestTools.UnitTesting;
using BirdCommand.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalssoft.DiagramNet;
using System.Drawing;

namespace BirdCommand.Custom.Tests
{
    [TestClass()]
    public class PatternUtilTests
    {
        [TestMethod()]
        public void CloneTest()
        {
            var elements = new List<BaseElement>();
            elements.Add(new BirdCell(10, 20, Direction.Up));

            var result = PatternUtil.Clone(elements);
            Assert.AreEqual(1, result.Count);
            Assert.AreNotEqual(elements[0], result[0]);
            var firstBird = elements[0] as BirdCell;
            var secondBird = result[0] as BirdCell;
            Assert.AreEqual(firstBird.Direction, secondBird.Direction);
            Assert.AreEqual(firstBird.Location, secondBird.Location);
        }

        [TestMethod()]
        public void NormalizePatternTest()
        {
            var pattern = new List<BaseElement>();
            var bird = new BirdCell(10, 20);
            pattern.Add(bird);
            pattern.Add(new PigCell(80, 20));

            var result = PatternUtil.NormalizePattern(pattern);
            Assert.AreEqual(0, result[0].Location.X);
            Assert.AreEqual(0, result[0].Location.Y);
            Assert.AreEqual(70, result[1].Location.X);
            Assert.AreEqual(0, result[1].Location.Y);

            Assert.AreEqual(10, bird.Location.X);
        }

        [TestMethod()]
        public void Rotate90ClockwiseTest()
        {
            var pattern1 = new List<BaseElement>();
            pattern1.Add(new BirdCell(40, 60));
            pattern1.Add(new EmptyCell(40, 60));
            pattern1.Add(new EmptyCell(40, 110));
            pattern1.Add(new EmptyCell(40, 160));

            var rotated1 = PatternUtil.Rotate90Clockwise(pattern1);
            Assert.IsTrue(rotated1[0] is BirdCell);
            Assert.AreEqual(Direction.Left, (rotated1[0] as BirdCell).Direction);
            Assert.AreEqual(new Point(100, 0), rotated1[0].Location);
            Assert.AreEqual(new Point(100, 0), rotated1[1].Location);
            Assert.AreEqual(new Point(50, 0), rotated1[2].Location);
            Assert.AreEqual(new Point(0, 0), rotated1[3].Location);
        }
    }
}