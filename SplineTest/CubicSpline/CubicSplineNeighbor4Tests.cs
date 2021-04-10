using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spline;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SplineTests {
    [TestClass()]
    public class CubicSplineNeighbor4Tests {
        readonly double[] v1 = { 12, 15, 15, 10, 10, 10, 10.5, 15, 50, 60, 85 };

        [TestMethod()]
        public void InsertTest1() {
            AkimaSpline sp1 = new(EndType.Open);
            AkimaSpline sp2 = new(EndType.Open);
            AkimaSpline sp3 = new(EndType.Open);

            for (int i = 0; i < v1.Length; i++) {
                sp1.Set(v1, i + 1);
                sp2.Insert(i, v1[i]);

                Assert.AreEqual(sp1, sp2);
            }

            sp3.Set(v1);

            Assert.AreEqual(sp1, sp3);
            Assert.AreEqual(sp2, sp3);
        }

        [TestMethod()]
        public void InsertTest2() {
            AkimaSpline sp1 = new(EndType.Open);
            AkimaSpline sp2 = new(EndType.Open);
            AkimaSpline sp3 = new(EndType.Open);

            for (int i = 0; i < v1.Length; i++) {
                sp1.Set(v1.Skip(v1.Length - i - 1).ToArray(), i + 1);
                sp2.Insert(0, v1[v1.Length - i - 1]);

                Assert.AreEqual(sp1, sp2);
            }

            sp3.Set(v1);

            Assert.AreEqual(sp1, sp3);
            Assert.AreEqual(sp2, sp3);
        }

        [TestMethod()]
        public void InsertTest3() {
            for (int test = 0; test < 100; test++) {
                AkimaSpline sp1 = new(EndType.Open);
                AkimaSpline sp2 = new(EndType.Open);

                List<double> v2 = new();
                Random random = new(test);

                for (int i = 0; i < 100; i++) {
                    int index = random.Next(i + 1);
                    double new_v = random.NextDouble();

                    v2.Insert(index, new_v);

                    sp1.Set(v2.ToArray());
                    sp2.Insert(index, new_v);

                    Assert.AreEqual(sp1, sp2);
                }
            }
        }

        [TestMethod()]
        public void InsertTest4() {
            AkimaSpline sp1 = new(EndType.Close);
            AkimaSpline sp2 = new(EndType.Close);
            AkimaSpline sp3 = new(EndType.Close);

            for (int i = 0; i < v1.Length; i++) {
                sp1.Set(v1, i + 1);
                sp2.Insert(i, v1[i]);

                Assert.AreEqual(sp1, sp2);
            }

            sp3.Set(v1);

            Assert.AreEqual(sp1, sp3);
            Assert.AreEqual(sp2, sp3);
        }

        [TestMethod()]
        public void InsertTest5() {
            AkimaSpline sp1 = new(EndType.Close);
            AkimaSpline sp2 = new(EndType.Close);
            AkimaSpline sp3 = new(EndType.Close);

            for (int i = 0; i < v1.Length; i++) {
                sp1.Set(v1.Skip(v1.Length - i - 1).ToArray(), i + 1);
                sp2.Insert(0, v1[v1.Length - i - 1]);

                Assert.AreEqual(sp1, sp2);
            }

            sp3.Set(v1);

            Assert.AreEqual(sp1, sp3);
            Assert.AreEqual(sp2, sp3);
        }

        [TestMethod()]
        public void InsertTest6() {
            for (int test = 0; test < 100; test++) {
                AkimaSpline sp1 = new(EndType.Close);
                AkimaSpline sp2 = new(EndType.Close);

                List<double> v2 = new();
                Random random = new(test);

                for (int i = 0; i < 100; i++) {
                    int index = random.Next(i + 1);
                    double new_v = random.NextDouble();

                    v2.Insert(index, new_v);

                    sp1.Set(v2.ToArray());
                    sp2.Insert(index, new_v);

                    Assert.AreEqual(sp1, sp2);
                }
            }
        }

        [TestMethod()]
        public void RemoveTest1() {
            AkimaSpline sp1 = new(EndType.Open);
            AkimaSpline sp2 = new(EndType.Open);

            List<double> v3 = v1.ToList();

            sp2.Set(v3.ToArray());

            while (v3.Count > 0) {
                v3.RemoveAt(0);

                sp1.Set(v3.ToArray());
                sp2.Remove(0);

                Assert.AreEqual(sp1, sp2);
            }
        }

        [TestMethod()]
        public void RemoveTest2() {
            AkimaSpline sp1 = new(EndType.Open);
            AkimaSpline sp2 = new(EndType.Open);

            List<double> v3 = v1.ToList();

            sp2.Set(v3.ToArray());

            while (v3.Count > 0) {
                v3.RemoveAt(v3.Count - 1);

                sp1.Set(v3.ToArray());
                sp2.Remove(sp2.Points - 1);

                Assert.AreEqual(sp1, sp2);
            }
        }

        [TestMethod()]
        public void RemoveTest3() {
            for (int test = 0; test < 100; test++) {
                AkimaSpline sp1 = new(EndType.Open);
                AkimaSpline sp2 = new(EndType.Open);

                Random random = new(test);
                List<double> v3 = (new double[100]).Select((_) => random.NextDouble()).ToList();

                sp2.Set(v3.ToArray());

                while (v3.Count > 0) {
                    int index = random.Next(v3.Count);
                    v3.RemoveAt(index);

                    sp1.Set(v3.ToArray());
                    sp2.Remove(index);

                    Assert.AreEqual(sp1, sp2);
                }
            }
        }

        [TestMethod()]
        public void RemoveTest4() {
            AkimaSpline sp1 = new(EndType.Close);
            AkimaSpline sp2 = new(EndType.Close);

            List<double> v3 = v1.ToList();

            sp2.Set(v3.ToArray());

            while (v3.Count > 0) {
                v3.RemoveAt(0);

                sp1.Set(v3.ToArray());
                sp2.Remove(0);

                Assert.AreEqual(sp1, sp2);
            }
        }

        [TestMethod()]
        public void RemoveTest5() {
            AkimaSpline sp1 = new(EndType.Close);
            AkimaSpline sp2 = new(EndType.Close);

            List<double> v3 = v1.ToList();

            sp2.Set(v3.ToArray());

            while (v3.Count > 0) {
                v3.RemoveAt(v3.Count - 1);

                sp1.Set(v3.ToArray());
                sp2.Remove(sp2.Points - 1);

                Assert.AreEqual(sp1, sp2);
            }
        }

        [TestMethod()]
        public void RemoveTest6() {
            for (int test = 0; test < 100; test++) {
                AkimaSpline sp1 = new(EndType.Close);
                AkimaSpline sp2 = new(EndType.Close);

                Random random = new(test);
                List<double> v3 = (new double[100]).Select((_) => random.NextDouble()).ToList();

                sp2.Set(v3.ToArray());

                while (v3.Count > 0) {
                    int index = random.Next(v3.Count);
                    v3.RemoveAt(index);

                    sp1.Set(v3.ToArray());
                    sp2.Remove(index);

                    Assert.AreEqual(sp1, sp2);
                }
            }
        }

        [TestMethod()]
        public void SetPointTest1() {
            for (int test = 0; test < 10; test++) {
                AkimaSpline sp1 = new(EndType.Open);
                AkimaSpline sp2 = new(EndType.Open);

                Random random = new(test);
                double[] v2 = (double[])v1.Clone();

                sp2.Set(v2);

                for (int i = 0; i < v1.Length; i++) {
                    double set_v = random.NextDouble();

                    v2[i] = set_v;

                    sp1.Set(v2);
                    sp2.SetPoint(i, set_v);

                    Assert.AreEqual(sp1, sp2);
                }
            }
        }

        [TestMethod()]
        public void SetPointTest2() {
            for (int test = 0; test < 10; test++) {
                AkimaSpline sp1 = new(EndType.Close);
                AkimaSpline sp2 = new(EndType.Close);

                Random random = new(test);
                double[] v2 = (double[])v1.Clone();

                sp2.Set(v2);

                for (int i = 0; i < v1.Length; i++) {
                    double set_v = random.NextDouble();

                    v2[i] = set_v;

                    sp1.Set(v2);
                    sp2.SetPoint(i, set_v);

                    Assert.AreEqual(sp1, sp2);
                }
            }
        }
    }
}