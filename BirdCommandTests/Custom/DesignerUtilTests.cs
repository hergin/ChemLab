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
    public class DesignerUtilTests
    {
        [TestMethod()]
        public void IsFirstInsideSecondTest()
        {
            var first = new RectangleElement(100, 200, 200, 200);
            var second = new RectangleElement(101, 201, 10, 10);
            Assert.IsTrue(DesignerUtil.IsSecondInsideFirst(first, second));

            second = new RectangleElement(100, 200, 10, 10);
            Assert.IsFalse(DesignerUtil.IsSecondInsideFirst(first, second));

            second = new RectangleElement(101, 201, 200, 100);
            Assert.IsFalse(DesignerUtil.IsSecondInsideFirst(first, second));
        }
    }
}