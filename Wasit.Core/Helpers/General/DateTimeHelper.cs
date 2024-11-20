using AAITHelper;
using Humanizer;
using System.Globalization;
using Wasit.Core.Enums;

namespace Wasit.Core.Helpers.General
{
    public static class DateTimeHelper
    {
        public static CultureInfo GetCulture(Language lang)
        {
            return lang == Language.Ar ? new CultureInfo("ar-EG") : new CultureInfo("en-US");
        }


        public static string GetTimeSpan(DateTime datetime, CultureInfo culture)
        {
            return datetime.Humanize(true, HelperDate.GetCurrentDate(3), culture);
        }


        public static List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = [];
            for (DateTime date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1))
            {
                dates.Add(date);
            }
            return dates;
        }
    }
}
