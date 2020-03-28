namespace Spline {

    /// <summary>単調スプライン</summary>
    public class MonotoneSpline : CubicSplineNeighbor2 {

        /// <summary>コンストラクタ</summary>
        public MonotoneSpline(EndType type = EndType.Open) : base(type) {
        }

        /// <summary>制御点における傾き</summary>
        protected override double Grad(double vm1, double v0, double vp1) {
            double vv = (vm1 - v0) * (v0 - vp1);
            
            return (vv > 0) ? (2 * vv / (vp1 - vm1)) : 0;
        }
    }
}
