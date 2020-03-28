namespace Spline {

    /// <summary>Catmull-Romスプライン</summary>
    public class CatmullRomSpline : CubicSplineNeighbor2 {

        /// <summary>コンストラクタ</summary>
        public CatmullRomSpline(EndType type = EndType.Open) : base(type) {
        }

        /// <summary>制御点における傾き</summary>
        protected override double Grad(double vm1, double v0, double vp1) {
            return (-vm1 + vp1) / 2;
        }
    }
}
