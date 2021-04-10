using System;

namespace Spline {
    public class Utility {
        public static double[] Polyploidize(double[] array) {
            const int index_shift = 2;

            if (array == null) {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length < 1) {
                throw new ArgumentException(nameof(array));
            }

            int length = array.Length;

            if (length <= 1) {
                return (double[])array.Clone();
            }

            double[] ret_array = new double[length * 2 - 1];
            double[] shift_array = new double[length + index_shift * 2];

            for (int i = 0; i < length; i++) {
                ret_array[i * 2] = shift_array[i + index_shift] = array[i];
            }

            for (int i = 1; i <= index_shift; i++) {
                shift_array[index_shift - i] = array[0] + (array[0] - array[1]) * i;
                shift_array[length + index_shift + i - 1] = array[length - 1] + (array[length - 1] - array[length - 2]) * i;
            }

            for (int i = 1; i < length; i++) {
                ret_array[i * 2 - 1] = Resampling(shift_array[i + index_shift - 3], shift_array[i + index_shift - 2], shift_array[i + index_shift - 1],
                                                  shift_array[i + index_shift], shift_array[i + index_shift + 1], shift_array[i + index_shift + 2]);
            }

            return ret_array;
        }

        public static double Resampling(double sm3, double sm2, double sm1, double sp1, double sp2, double sp3) {
            double dm12 = sm1 - sm2, dp12 = sp1 - sp2;
            double dm23 = sm2 - sm3, dp23 = sp2 - sp3;

            double dm = 2 * dm12 - dm23, dp = 2 * dp12 - dp23, dd;

            if (dm > 0) {
                dm = Math.Max(0, Math.Min(dm12 * 2, dm));
            }
            else if (dm < 0) {
                dm = Math.Min(0, Math.Max(dm12 * 2, dm));
            }

            if (dp > 0) {
                dp = Math.Max(0, Math.Min(dp12 * 2, dp));
            }
            else if (dp < 0) {
                dp = Math.Min(0, Math.Max(dp12 * 2, dp));
            }

            if (dm > 0 && dp > 0) {
                dd = +Math.Sqrt(dm * dp);
            }
            else if (dm < 0 && dp < 0) {
                dd = -Math.Sqrt(dm * dp);
            }
            else {
                dd = 0;
            }

            return (sm1 + sp1 + dd) / 2;
        }
    }
}
