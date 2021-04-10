using Algebra;
using System;

namespace Spline {

    /// <summary>多次元スプライン補間ジェネリッククラス</summary>
    public class SplineMultiDimension<SplineType> where SplineType : Spline {
        private readonly SplineType[] spline;

        /// <summary>コンストラクタ</summary>
        public SplineMultiDimension(int dimention, EndType type = EndType.Open) {
            if (dimention <= 0) {
                throw new ArgumentOutOfRangeException(nameof(dimention));
            }

            this.spline = new SplineType[dimention];
            for (int i = 0; i < spline.Length; i++) {
                this.spline[i] = Spline.Construct<SplineType>(type);
            }
            this.Dimention = dimention;
        }

        /// <summary>制御点数</summary>
        public int Points => spline[0].Points;

        /// <summary>次元数</summary>
        public int Dimention { get; private set; }

        /// <summary>制御点を設定</summary>
        public void Set(params Vector[] v) {
            if (v is null) {
                throw new ArgumentNullException(nameof(v));
            }

            Set(v, v.Length);
        }

        /// <summary>制御点を設定</summary>
        public void Set(Vector[] v, int points) {
            Initialize();

            if (v is null) {
                throw new ArgumentNullException(nameof(v));
            }

            for (int i = 0; i < points; i++) {
                if (v[i].Dim != Dimention) {
                    throw new ArgumentException("mismatch dimention", nameof(v));
                }
            }

            double[] s = new double[points];

            for (int i = 0, j; i < Dimention; i++) {
                for (j = 0; j < points; j++) {
                    s[j] = v[j][i];
                }

                this.spline[i].Set(s);
            }
        }

        /// <summary>制御点を挿入</summary>
        public void Insert(int index, Vector new_v) {
            if (index < 0 || index > Points || new_v.Dim != Dimention) {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            for (int i = 0; i < Dimention; i++) {
                this.spline[i].Insert(index, new_v[i]);
            }
        }

        /// <summary>制御点を除去</summary>
        public void Remove(int index) {
            if (index < 0 || index >= Points) {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            for (int i = 0; i < Dimention; i++) {
                this.spline[i].Remove(index);
            }
        }

        /// <summary>初期化</summary>
        public void Initialize() {
            for (int i = 0; i < spline.Length; i++) {
                spline[i].Initialize();
            }
        }

        /// <summary>補間値</summary>
        public Vector Value(double t) {
            double[] v = new double[Dimention];

            for (int i = 0; i < Dimention; i++) {
                v[i] = spline[i].Value(t);
            }

            return new Vector(v);
        }

        /// <summary>補間微分値</summary>
        public Vector Diff(double t) {
            double[] v = new double[Dimention];

            for (int i = 0; i < Dimention; i++) {
                v[i] = spline[i].Diff(t);
            }

            return new Vector(v);
        }

        /// <summary>補間N次微分値</summary>
        public Vector Diff(double t, uint n) {
            double[] v = new double[Dimention];

            for (int i = 0; i < Dimention; i++) {
                v[i] = spline[i].Diff(t, n);
            }

            return new Vector(v);
        }

        /// <summary>等しいか判定</summary>
        public static bool operator ==(SplineMultiDimension<SplineType> s1, SplineMultiDimension<SplineType> s2) {
            if (ReferenceEquals(s1, s2)) {
                return true;
            }
            if (s1 is null || s2 is null) {
                return false;
            }
            if (s1.Dimention != s2.Dimention) {
                return false;
            }
            for (int i = 0; i < s1.Dimention; i++) {
                if (s1.spline[i] != s2.spline[i]) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>異なるか判定</summary>
        public static bool operator !=(SplineMultiDimension<SplineType> s1, SplineMultiDimension<SplineType> s2) {
            return !(s1 == s2);
        }

        /// <summary>等しいか判定</summary>
        public override bool Equals(object obj) {
            return (!(obj is null)) && obj is SplineMultiDimension<SplineType> dimspline && dimspline == this;
        }

        /// <summary>ハッシュ値</summary>
        public override int GetHashCode() {
            return spline[0].GetHashCode();
        }
    }
}
