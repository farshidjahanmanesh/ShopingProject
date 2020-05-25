using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Utilities
{
    public static class FRPrice
    {
        public static string ToRial(this int money)
        {
            var cultureInfo = new CultureInfo("fa-Ir");
            cultureInfo.NumberFormat.CurrencyPositivePattern = 3;
            cultureInfo.NumberFormat.CurrencyNegativePattern = 3;
            return string.Format(cultureInfo, "{0:C0}", money);
        }
        public static string ToRial(this float money)
        {
            var cultureInfo = new CultureInfo("fa-Ir");
            cultureInfo.NumberFormat.CurrencyPositivePattern = 3;
            cultureInfo.NumberFormat.CurrencyNegativePattern = 3;
            return string.Format(cultureInfo, "{0:C0}", money);
        }

        public static string ToRial(this double money)
        {
            var cultureInfo = new CultureInfo("fa-Ir");
            cultureInfo.NumberFormat.CurrencyPositivePattern = 3;
            cultureInfo.NumberFormat.CurrencyNegativePattern = 3;
            return string.Format(cultureInfo, "{0:C0}", money);
        }

        public static string ToRial(this decimal money)
        {
            var cultureInfo = new CultureInfo("fa-Ir");
            cultureInfo.NumberFormat.CurrencyPositivePattern = 3;
            cultureInfo.NumberFormat.CurrencyNegativePattern = 3;
            return string.Format(cultureInfo, "{0:C0}", money);
        }
    }
}
