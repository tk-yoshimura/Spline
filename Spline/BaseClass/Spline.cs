using System;
using System.Collections.Generic;
using System.Linq;

namespace Spline {

    /// <summary>スプライン基本クラス</summary>
    public abstract class Spline {

        /// <summary>制御点</summary>
        protected List<double> v;

        /// <summary>コンストラクタ</summary>
        public Spline(EndType type = EndType.Open) {
            this.EndType = type;
            this.v = new List<double>();
        }

        /// <summary>終端タイプ</summary>
        public EndType EndType { get; private set; }

        /// <summary>制御点数</summary>
        public int Points => v.Count;

        /// <summary>制御点を設定</summary>
        public void Set(params double[] v) {
            Set(v, v.Length);
        }

        /// <summary>制御点を設定</summary>
        public abstract void Set(double[] v, int points);

        /// <summary>制御点を挿入</summary>
        public abstract void Insert(int index, double new_v);

        /// <summary>制御点を除去</summary>
        public abstract void Remove(int index);

        /// <summary>初期化</summary>
        public virtual void Initialize() {
            v.Clear();
        }

        /// <summary>補間値</summary>
        public abstract double Value(double t);

        /// <summary>補間微分値</summary>
        public abstract double Diff(double t);

        /// <summary>補間N次微分値</summary>
        public abstract double Diff(double t, uint n);

        /// <summary>制御点番号が有効であるか判定</summary>
        public bool IsInRange(int index) {
            return (index >= 0) || (index < Points);
        }

        /// <summary>制御点の値を取得</summary>
        public double GetPoint(int index) {
            return v[index];
        }

        /// <summary>制御点の値を設定</summary>
        public abstract void SetPoint(int index, double set_v);

        /// <summary>等しいか判定</summary>
        public static bool operator ==(Spline s1, Spline s2) {
            if (ReferenceEquals(s1, s2)) {
                return true;
            }
            if (s1 is null || s2 is null) {
                return false;
            }
            if (s1.EndType != s2.EndType) {
                return false;
            }

            return s1.v.SequenceEqual(s2.v);
        }

        /// <summary>異なるか判定</summary>
        public static bool operator !=(Spline s1, Spline s2) {
            return !(s1 == s2);
        }

        /// <summary>等しいか判定</summary>
        public override bool Equals(object obj) {
            return (obj is not null) && obj is Spline spline && spline == this;
        }

        /// <summary>ハッシュ値</summary>
        public override int GetHashCode() {
            return Points > 0 ? v[0].GetHashCode() : 0;
        }

        /// <summary>コンストラクタ ジェネリッククラス用</summary>
        public static SplineType Construct<SplineType>(EndType type = EndType.Open) {
            return (SplineType)typeof(SplineType).GetConstructor(new Type[] { typeof(EndType) }).Invoke(new object[] { type });
        }
    }
}
