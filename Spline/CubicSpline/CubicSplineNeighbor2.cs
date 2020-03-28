using System;

namespace Spline {

    /// <summary>傾きを制御点とその近傍2点から算出する3次スプライン基本クラス</summary>
    public abstract class CubicSplineNeighbor2 : CubicSpline {

        /// <summary>コンストラクタ</summary>
        public CubicSplineNeighbor2(EndType type = EndType.Open) : base(type) {
        }

        /// <summary>制御点を挿入</summary>
        public sealed override void Insert(int index, double new_v) {
            if(index < 0 || index > Points) {
                throw new ArgumentException();
            }

            v.Insert(index, new_v);
            segment_list.Insert(index, new CubicSegment());

            if(Points > 4) {
                for(int i = index - 2; i <= index + 1; i++) {
                    ReflashSegment(i);
                }
            }
            else {
                for(int i = 0; i < Points; i++) {
                    ReflashSegment(i);
                }
            }
        }

        /// <summary>制御点を除去</summary>
        public sealed override void Remove(int index) {
            if(index < 0 || index >= Points) {
                throw new ArgumentException();
            }

            v.RemoveAt(index);
            segment_list.RemoveAt(index);

            if(Points > 3) {
                for(int i = index - 2; i <= index; i++) {
                    ReflashSegment(i);
                }
            }
            else {
                for(int i = 0; i < Points; i++) {
                    ReflashSegment(i);
                }
            }
        }

        /// <summary>制御点を設定</summary>
        public sealed override void SetPoint(int index, double set_v) {
            v[index] = set_v;

            if(Points > 4) {
                for(int i = index - 2; i <= index + 1; i++) {
                    ReflashSegment(i);
                }
            }
            else {
                for(int i = 0; i < Points; i++) {
                    ReflashSegment(i);
                }
            }
        }

        /// <summary>制御点における傾き</summary>
        protected abstract double Grad(double vm1, double v0, double vp1);

        /// <summary>制御点における傾き</summary>
        protected sealed override double Grad(int index) {
            if(index < 0 || index >= Points) {
                throw new ArgumentException();
            }

            if(Points <= 1) {
                return 0;
            }

            if(EndType == EndType.Open) {
                if(Points <= 2) {
                    return v[1] - v[0];
                }

                if(index == 0) {
                    return Grad(2 * v[0] - v[1], v[0], v[1]);
                }
                if(index == Points - 1) {
                    return Grad(v[Points - 2], v[Points - 1], 2 * v[Points - 1] - v[Points - 2]);
                }
            }
            else {
                if(Points <= 2) {
                    return 0;
                }

                if(index == 0) {
                    return Grad(v[Points - 1], v[0], v[1]);
                }
                if(index == Points - 1) {
                    return Grad(v[Points - 2], v[Points - 1], v[0]);
                }
            }
            return Grad(v[index - 1], v[index], v[index + 1]);
        }
    }
}
