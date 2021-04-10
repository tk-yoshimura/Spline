namespace Spline {

    /// <summary>3次スプライン区間</summary>
    public struct CubicSegment {
        internal double a, b, c, d;

        /// <summary>コンストラクタ</summary>
        private CubicSegment(double a, double b, double c, double d) {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        /// <summary>左端の区間</summary>
        public static CubicSegment LeftEnd(double v0, double v1, double g1) {
            double a, b, c;

            a = v0;
            b = -2 * (v0 - v1) - g1;
            c = v0 - v1 + g1;

            return new CubicSegment(a, b, c, 0);
        }

        /// <summary>右端の区間</summary>
        public static CubicSegment RightEnd(double v0, double v1, double g0) {
            double a, b, c;

            a = v0;
            b = g0;
            c = -v0 + v1 - g0;

            return new CubicSegment(a, b, c, 0);
        }

        /// <summary>端を含まない区間</summary>
        public static CubicSegment Interval(double v0, double v1, double g0, double g1) {
            double a, b, c, d;

            a = v0;
            b = g0;
            c = -3 * (v0 - v1) - 2 * g0 - g1;
            d = 2 * (v0 - v1) + g0 + g1;

            return new CubicSegment(a, b, c, d);
        }

        /// <summary>制御点数2のときの区間</summary>
        public static CubicSegment Linear(double v0, double v1) {
            double a, b;

            a = v0;
            b = v1 - v0;

            return new CubicSegment(a, b, 0, 0);
        }

        /// <summary>制御点数1のときの区間</summary>
        public static CubicSegment Constant(double v) {
            return new CubicSegment(v, 0, 0, 0);
        }

        /// <summary>補間値</summary>
        public double Value(double t) {
            return a + t * (b + t * (c + t * d));
        }

        /// <summary>補間微分値</summary>
        public double Diff(double t) {
            return b + t * (2 * c + t * 3 * d);
        }

        /// <summary>補間N次微分値</summary>
        public double Diff(double t, uint n) {
            if (n == 0)
                return Value(t);
            if (n == 1)
                return Diff(t);
            if (n > 3)
                return 0;

            return (n == 2) ? (2 * c + t * 6 * d) : (6 * d);
        }

        /// <summary>等しいか判定</summary>
        public static bool operator ==(CubicSegment s1, CubicSegment s2) {
            return s1.a == s2.a && s1.b == s2.b && s1.c == s2.c && s1.d == s2.d;
        }

        /// <summary>異なるか判定</summary>
        public static bool operator !=(CubicSegment s1, CubicSegment s2) {
            return !(s1 == s2);
        }

        /// <summary>等しいか判定</summary>
        public override bool Equals(object obj) {
            return obj is CubicSegment ? (CubicSegment)obj == this : false;
        }

        /// <summary>ハッシュ値</summary>
        public override int GetHashCode() {
            return a.GetHashCode() ^ b.GetHashCode() ^ c.GetHashCode() ^ d.GetHashCode();
        }
    }
}
