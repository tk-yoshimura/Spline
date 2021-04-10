using System;

namespace Spline {
    public class PixelSpline : CubicSplineNeighbor4 {
        public PixelSpline(EndType type = EndType.Open) : base(type) {
        }

        protected override double Grad(double vm2, double vm1, double v0, double vp1, double vp2) {
            double mm2 = vm1 - vm2, mm1 = v0 - vm1, mp1 = vp1 - v0, mp2 = vp2 - vp1;

            if (mm1 * mp2 < 0 != mp1 * mm2 < 0) {
                if (mm1 * mp2 < 0) {
                    double t = 0.5 - (Math.Abs(mm1 / (mm1 - mp2)) - 0.5) * 0.7;

                    return +((t * t * t - 3 * t + 2) * vp1 - t * t * t * v0) / (t * t * t - 2 * t * t + t);
                }

                if (mp1 * mm2 < 0) {
                    double t = 0.5 - (Math.Abs(mp1 / (mp1 - mm2)) - 0.5) * 0.7;

                    return -((t * t * t - 3 * t + 2) * vm1 - t * t * t * v0) / (t * t * t - 2 * t * t + t);
                }
            }

            double vv = (vm1 - v0) * (v0 - vp1);

            return (vv > 0) ? (2 * vv / (vp1 - vm1)) : 0;
        }
    }
}
