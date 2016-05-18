using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Tools
{
    public class NumberTools
    {
        const string Alphabet = "AG8FOLE2WVTCPY5ZH3NIUDBXSMQK7946";
        public static string CouponCode(int number)
        {
            var b = new StringBuilder();
            for (var i = 0; i < 6; ++i)
            {
                b.Append(Alphabet[(int)number & ((1 << 5) - 1)]);
                number = number >> 5;
            }
            return b.ToString();
        }
        public static long CodeFromCoupon(string coupon)
        {
            long n = 0;
            for (var i = 0; i < 6; ++i)
                n = n | (((long)Alphabet.IndexOf(coupon[i])) << (5 * i));
            return n;
        }
    }
}
