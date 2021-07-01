using System;

namespace System
{
    public static class DateTimeExtension
    {
        /// <summary>
        ///  Returns date formated YYYY.MM.dd.mm.ss
        /// </summary>
        /// <returns> a date string formated YYYY.MM.dd.mm.ss</returns>
        public static string ToPath(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy.MM.dd.mm.ss");
        }
    }
}
