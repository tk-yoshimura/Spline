using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spline;

namespace SplineTests {
    [TestClass()]
    public class CubicSplineTests {
        readonly double[] v1 = { 12, 15, 15, 10, 10, 10, 10.5, 15, 50, 60, 85 };

        [TestMethod()]
        public void InitializeTest() {
            CatmullRomSpline sp1 = new(EndType.Close);
            CatmullRomSpline sp2 = new(EndType.Close);

            sp2.Set(v1);
            sp2.Initialize();

            Assert.AreEqual(sp1, sp2);

            sp1.Set(v1);
            sp2.Set(v1);

            Assert.AreEqual(sp1, sp2);
        }

        [TestMethod()]
        public void EqualTest() {
            AkimaSpline sp1 = new(EndType.Open);
            AkimaSpline sp2 = new(EndType.Close);
            AkimaSpline sp3 = new(EndType.Open);
            AkimaSpline sp4 = new(EndType.Open);
            AkimaSpline sp5 = new(EndType.Open);
            AkimaSpline sp6 = new(EndType.Open);

            sp1.Set(12, 15, 15, 10, 10, 10, 10.5, 15, 50, 60, 85);
            sp2.Set(12, 15, 15, 10, 10, 10, 10.5, 15, 50, 60, 85);
            sp3.Set(12, 15, 15, 10, 10, 10, 10.5, 15, 50, 60, 86);
            sp4.Set(12, 15, 15, 10, 10, 10, 10.5, 15, 50, 60);
            sp5.Set(12, 15, 15, 10, 10, 10, 10.5, 15, 50, 60, 85, 90);
            sp6.Set(12, 15, 15, 10, 10, 10, 10.5, 15, 50, 60, 85);

            Assert.IsTrue(sp1 != sp2);
            Assert.IsTrue(sp1 != sp3);
            Assert.IsTrue(sp1 != sp4);
            Assert.IsTrue(sp1 != sp5);
            Assert.IsTrue(sp1 == sp6);
            Assert.IsTrue(sp1 != null);
            Assert.IsFalse(sp1 == null);
        }
    }
}