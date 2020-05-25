using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
namespace UI.Utilities
{

    class PNumberTString
    {
        private static string[] yakan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static string[] sadgan = new string[10] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };
        private static string getnum3(int num3)
        {
            string s = "";
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yakan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            };
            return s;
        }
        public string num2str(string snum)
        {
            string stotal = "";
            if (snum == "") return "صفر";
            if (snum == "0")
            {
                return yakan[0];
            }
            else
            {
                snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
                int L = snum.Length / 3 - 1;
                for (int i = 0; i <= L; i++)
                {
                    int b = int.Parse(snum.Substring(i * 3, 3));
                    if (b != 0)
                        stotal = stotal + getnum3(b) + " " + basex[L - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            return stotal;
        }
    }
    public static class DateTimeFarsi
    {
        private static PersianCalendar pr = new PersianCalendar();
        private enum month
        {
            فروردین,
            اردیبهشت,
            خرداد,
            تیر,
            مرداد,
            شهریور,
            مهر,
            آبان,
            آذر,
            دی,
            بهمن,
            اسفند
        }

        public static string PesrianMonth(this DateTime now)
        {
            var m = pr.GetMonth(now);
            var mString = (month)m;
            return ((month)m).ToString();
        }
        public static string PesrianDay(this DateTime now)
        {
            string dString = "";
            switch (pr.GetDayOfWeek(now))
            {
                case DayOfWeek.Sunday:
                    dString = "یک شنبه";

                    break;
                case DayOfWeek.Monday:
                    dString = "دو شنبه";
                    break;
                case DayOfWeek.Tuesday:
                    dString = "سه شنبه";

                    break;
                case DayOfWeek.Wednesday:
                    dString = "چهار شنبه";

                    break;
                case DayOfWeek.Thursday:
                    dString = "پنج شنبه";

                    break;
                case DayOfWeek.Friday:
                    dString = "جمعه";

                    break;
                case DayOfWeek.Saturday:
                    dString = "شنبه";

                    break;
            }

            return dString;
        }
        public static string PesrianYear(this DateTime now)
        {
            return pr.GetYear(now).ToString();
        }
        public static string PersionDayAgo(this DateTime now)
        {
            TimeSpan days = DateTime.Now.Subtract(now);
            PNumberTString pnum = new PNumberTString();
            return pnum.num2str(days.Days.ToString());
            

        }

    }

    // class DateTimeFarsi
    //{

    //    string _day;
    //    string _year;
    //    string _month;
    //    bool _IsActiveFarsiDate = false;

    //    private enum month
    //    {
    //        فروردین,
    //        اردیبهشت,
    //        خرداد,
    //        تیر,
    //        مرداد,
    //        شهریور,
    //        مهر,
    //        آبان,
    //        آذر,
    //        دی,
    //        بهمن,
    //        اسفند
    //    }

    //    public DateTimeFarsi DateNow()
    //    {

    //        PersianCalendar pr = new PersianCalendar();
    //        var now = DateTime.Now;
    //        var m = pr.GetMonth(now);
    //        var mString = (month)m;
    //        string dString = "";
    //        switch (pr.GetDayOfWeek(now))
    //        {
    //            case DayOfWeek.Sunday:
    //                dString = "یک شنبه";

    //                break;
    //            case DayOfWeek.Monday:
    //                dString = "دو شنبه";
    //                break;
    //            case DayOfWeek.Tuesday:
    //                dString = "سه شنبه";

    //                break;
    //            case DayOfWeek.Wednesday:
    //                dString = "چهار شنبه";

    //                break;
    //            case DayOfWeek.Thursday:
    //                dString = "پنج شنبه";

    //                break;
    //            case DayOfWeek.Friday:
    //                dString = "جمعه";

    //                break;
    //            case DayOfWeek.Saturday:
    //                dString = "شنبه";

    //                break;
    //        }
    //        _IsActiveFarsiDate = true;
    //        return new DateTimeFarsi()
    //        {
    //            _day = dString,
    //            _year = pr.GetYear(now).ToString(),
    //            _month = mString.ToString()
    //        };
    //    }
    //}
}
