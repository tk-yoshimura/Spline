using System;

namespace Spline {

    /// <summary>傾きを制御点とその近傍4点から算出する3次スプライン基本クラス</summary>
    public abstract class CubicSplineNeighbor4 : CubicSpline {

        /// <summary>コンストラクタ</summary>
        public CubicSplineNeighbor4(EndType type = EndType.Open) : base(type) {
        }

        /// <summary>制御点を挿入</summary>
        public sealed override void Insert(int index, double new_v) {
            if (index < 0 || index > Points) {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            v.Insert(index, new_v);
            segment_list.Insert(index, new CubicSegment());

            if (Points > 6) {
                for (int i = index - 3; i <= index + 2; i++) {
                    ReflashSegment(i);
                }
            }
            else {
                for (int i = 0; i < Points; i++) {
                    ReflashSegment(i);
                }
            }
        }

        /// <summary>制御点を除去</summary>
        public sealed override void Remove(int index) {
            if (index < 0 || index >= Points) {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            v.RemoveAt(index);
            segment_list.RemoveAt(index);

            if (Points > 5) {
                for (int i = index - 3; i <= index + 1; i++) {
                    ReflashSegment(i);
                }
            }
            else {
                for (int i = 0; i < Points; i++) {
                    ReflashSegment(i);
                }
            }
        }

        /// <summary>制御点を設定</summary>
        public sealed override void SetPoint(int index, double set_v) {
            v[index] = set_v;

            if (Points > 6) {
                for (int i = index - 3; i <= index + 2; i++) {
                    ReflashSegment(i);
                }
            }
            else {
                for (int i = 0; i < Points; i++) {
                    ReflashSegment(i);
                }
            }
        }

        /// <summary>制御点における傾き</summary>
        protected abstract double Grad(double vm2, double vm1, double v0, double vp1, double vp2);

        /// <summary>制御点における傾き</summary>
        protected sealed override double Grad(int index) {
            if (index < 0 || index >= Points) {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (Points <= 1) {
                return 0;
            }

            if (EndType == EndType.Open) {
                if (Points <= 2) {
                    return v[1] - v[0];
                }

                if (Points <= 3 && index == 1) {
                    return Grad(2 * v[0] - v[1], v[0], v[1], v[2], 2 * v[2] - v[1]);
                }

                if (index == 0) {
                    return Grad(3 * v[0] - 2 * v[1], 2 * v[0] - v[1], v[0], v[1], v[2]);
                }
                if (index == 1) {
                    return Grad(2 * v[0] - v[1], v[0], v[1], v[2], v[3]);
                }
                if (index == Points - 2) {
                    return Grad(v[Points - 4], v[Points - 3], v[Points - 2], v[Points - 1], 2 * v[Points - 1] - v[Points - 2]);
                }
                if (index == Points - 1) {
                    return Grad(v[Points - 3], v[Points - 2], v[Points - 1], 2 * v[Points - 1] - v[Points - 2], 3 * v[Points - 1] - 2 * v[Points - 2]);
                }
            }
            else {
                if (Points <= 2) {
                    return 0;
                }

                if (Points <= 3) {
                    return index switch {
                        0 => Grad(v[1], v[2], v[0], v[1], v[2]),
                        1 => Grad(v[2], v[0], v[1], v[2], v[0]),
                        _ => Grad(v[0], v[1], v[2], v[0], v[1]),
                    };
                }

                if (index == 0) {
                    return Grad(v[Points - 2], v[Points - 1], v[0], v[1], v[2]);
                }
                if (index == 1) {
                    return Grad(v[Points - 1], v[0], v[1], v[2], v[3]);
                }
                if (index == Points - 1) {
                    return Grad(v[Points - 3], v[Points - 2], v[Points - 1], v[0], v[1]);
                }
                if (index == Points - 2) {
                    return Grad(v[Points - 4], v[Points - 3], v[Points - 2], v[Points - 1], v[0]);
                }

            }
            return Grad(v[index - 2], v[index - 1], v[index], v[index + 1], v[index + 2]);
        }
    }
}
