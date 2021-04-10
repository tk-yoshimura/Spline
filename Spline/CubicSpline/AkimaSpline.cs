using System;

namespace Spline {

    /// <summary>秋間スプライン</summary>
    public class AkimaSpline : CubicSplineNeighbor4 {

        /// <summary>コンストラクタ</summary>
        public AkimaSpline(EndType type = EndType.Open) : base(type) { }

        /// <summary>制御点における傾き</summary>
        protected override double Grad(double vm2, double vm1, double v0, double vp1, double vp2) {
            double mm2 = vm1 - vm2, mm1 = v0 - vm1, mp1 = vp1 - v0, mp2 = vp2 - vp1;

            if (mm1 == mp1) {
                return mm1;
            }

            if (mm2 == mm1 && mp1 == mp2) {
                return 0.5 * (mm1 + mp1);
            }

            if (mm1 == mm2) {
                return mm1;
            }

            if (mp1 == mp2) {
                return mp1;
            }

            double mm = Math.Abs(mm2 - mm1);
            double mp = Math.Abs(mp1 - mp2);

            return (mp1 * mm + mm1 * mp) / (mm + mp);
        }
    }
}
