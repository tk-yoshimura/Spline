using System;
using System.Collections.Generic;
using System.Linq;

namespace Spline {

    /// <summary>3次スプライン補間基本クラス</summary>
    public abstract class CubicSpline : Spline {

        /// <summary>区間リスト</summary>
        protected List<CubicSegment> segment_list;

        /// <summary>コンストラクタ</summary>
        public CubicSpline(EndType type = EndType.Open) : base(type) {
            this.segment_list = new List<CubicSegment>();
        }

        /// <summary>制御点を設定</summary>
        public sealed override void Set(double[] v, int points) {
            Initialize();

            if(points == 0) {
                return;
            }

            if(v == null || v.Length <= 0 || v.Length < points) {
                throw new ArgumentException();
            }

            this.v.AddRange(v.Take(points));

            segment_list = new List<CubicSegment>(points);

            for(int i = 0; i < points; i++) {
                segment_list.Add(new CubicSegment());
            }

            for(int i = 0; i < Points; i++) {
                ReflashSegment(i);
            }
        }

        /// <summary>初期化</summary>
        public sealed override void Initialize() {
            base.Initialize();
            segment_list.Clear();
        }

        /// <summary>補間値</summary>
        public sealed override double Value(double t) {
            if(Points <= 0 || double.IsNaN(t) || double.IsInfinity(t)) {
                return double.NaN;
            }

            if(EndType == EndType.Open) {
                int pos = t < (Points - 1) ? (int)t : (Points - 1);
                if(t >= 0) {
                    double h = t - pos;
                    return segment_list[pos].Value(h);
                }
                return segment_list[0].a + t * segment_list[0].b;
            }
            else {
                double h = t - Math.Floor(t);
                int pos = ((int)(Math.Floor(t)) % Points + Points) % Points;
                return segment_list[pos].Value(h);
            }
        }

        /// <summary>補間微分値</summary>
        public sealed override double Diff(double t) {
            if(Points <= 0 || double.IsNaN(t) || double.IsInfinity(t)) {
                return double.NaN;
            }

            if(EndType == EndType.Open) {
                int pos = t < (Points - 1) ? (int)t : (Points - 1);
                if(t >= 0) {
                    double h = t - pos;
                    return segment_list[pos].Diff(h);
                }
                return segment_list[0].b;
            }
            else {
                double h = t - Math.Floor(t);
                int pos = ((int)(Math.Floor(t)) % Points + Points) % Points;
                return segment_list[pos].Diff(h);
            }
        }

        /// <summary>補間N次微分値</summary>
        public sealed override double Diff(double t, uint n) {
            if(Points <= 0 || double.IsNaN(t) || double.IsInfinity(t)) {
                return double.NaN;
            }

            if(EndType == EndType.Open) {
                if(n == 0)
                    return Value(t);
                if(n == 1)
                    return Diff(t);
                if(n > 3)
                    return 0;

                int pos = t < (Points - 1) ? (int)t : (Points - 1);
                if(t >= 0) {
                    double h = t - pos;
                    return segment_list[pos].Diff(h, n);
                }
                return 0;
            }
            else {
                double h = t - Math.Floor(t);
                int pos = ((int)(Math.Floor(t)) % Points + Points) % Points;
                return segment_list[pos].Diff(h, n);
            }
        }

        /// <summary>制御点における傾き</summary>
        protected abstract double Grad(int index);

        /// <summary>区間更新</summary>
        protected void ReflashSegment(int index) {
            if(Points <= 1) {
                segment_list[0] = CubicSegment.Constant(v[0]);
                return;
            }

            if(EndType == EndType.Close) {
                index = (index % Points + Points) % Points;
            }

            if(index < 0 || index >= Points) {
                return;
            }

            if(EndType == EndType.Open) {
                if(index == 0) {
                    segment_list[0] = CubicSegment.LeftEnd(v[0], v[1], Grad(1));
                }
                else if(index >= Points - 2) {
                    segment_list[Points - 2] = CubicSegment.RightEnd(v[Points - 2], v[Points - 1], Grad(Points - 2));

                    double g = segment_list[Points - 2].b + 2 * segment_list[Points - 2].c;
                    segment_list[Points - 1] = CubicSegment.Linear(v[Points - 1], v[Points - 1] + g);
                }
                else {
                    segment_list[index] = CubicSegment.Interval(v[index], v[index + 1], Grad(index), Grad(index + 1));
                }
            }
            else {
                if(index == Points - 1) {
                    segment_list[Points - 1] = CubicSegment.Interval(v[Points - 1], v[0], Grad(Points - 1), Grad(0));
                }
                else {
                    segment_list[index] = CubicSegment.Interval(v[index], v[index + 1], Grad(index), Grad(index + 1));
                }
            }
        }

        /// <summary>等しいか判定</summary>
        public static bool operator ==(CubicSpline s1, CubicSpline s2) {
            if((Spline)s1 != (Spline)s2) {
                return false;
            }

            for(int i = 0, Points = s1.segment_list.Count; i < Points; i++) {
                if(s1.segment_list[i] != s2.segment_list[i]) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>異なるか判定</summary>
        public static bool operator !=(CubicSpline s1, CubicSpline s2) {
            return !(s1 == s2);
        }

        /// <summary>等しいか判定</summary>
        public override bool Equals(object obj) {
            return obj is CubicSpline ? (CubicSpline)obj == this : false;
        }

        /// <summary>ハッシュ値</summary>
        public override int GetHashCode() {
            return Points > 0 ? segment_list[0].GetHashCode() : 0;
        }
    }
}
