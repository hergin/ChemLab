using Dalssoft.DiagramNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChemLab.Custom.Tests
{
    [TestClass()]
    public class DesignerUtilTests
    {
        [TestMethod()]
        public void IsSecondInsideFirstTest()
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