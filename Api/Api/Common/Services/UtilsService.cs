using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Api.Common.Services
{
    public class UtilsService
    {
        public static string GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GetWeekInYear(DateTime time)
        {
            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            return myCal.GetWeekOfYear(time, myCWR, myFirstDOW);
        }

        public static string ColumnAdress(int col)
        {
            if (col <= 26)
            {
                return Convert.ToChar(col + 64).ToString();
            }
            int div = col / 26;
            int mod = col % 26;
            if (mod == 0) { mod = 26; div--; }
            return ColumnAdress(div) + ColumnAdress(mod);
        }

        public static string ConvertRomanNumber(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("Value must be between 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ConvertRomanNumber(number - 1000);
            if (number >= 900) return "CM" + ConvertRomanNumber(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ConvertRomanNumber(number - 500);
            if (number >= 400) return "CD" + ConvertRomanNumber(number - 400);
            if (number >= 100) return "C" + ConvertRomanNumber(number - 100);
            if (number >= 90) return "XC" + ConvertRomanNumber(number - 90);
            if (number >= 50) return "L" + ConvertRomanNumber(number - 50);
            if (number >= 40) return "XL" + ConvertRomanNumber(number - 40);
            if (number >= 10) return "X" + ConvertRomanNumber(number - 10);
            if (number >= 9) return "IX" + ConvertRomanNumber(number - 9);
            if (number >= 5) return "V" + ConvertRomanNumber(number - 5);
            if (number >= 4) return "IV" + ConvertRomanNumber(number - 4);
            if (number >= 1) return "I" + ConvertRomanNumber(number - 1);
            throw new ArgumentOutOfRangeException("Value must be between 1 and 3999");
        }

        public static int GetWeekOrderInYear(DateTime time)
        {
            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            return myCal.GetWeekOfYear(time, myCWR, myFirstDOW);
        }

        public static int GetQuarter(int month)
        {
            int quarter = 1;
            if (month >= 1 && month <= 3)
                quarter = 1;
            if (month >= 4 && month <= 6)
                quarter = 2;
            if (month >= 7 && month <= 9)
                quarter = 3;
            if (month >= 10 && month <= 12)
                quarter = 4;
            return quarter;
        }

        public static DateTime ConvertDateDDMMYYYYStart(string datetime)
        {
            DateTime res = new DateTime(1990, 1, 1);
            string[] td1 = datetime.Split(' ');
            string[] td = td1[0].Split('-');
            try
            {
                res = new DateTime(int.Parse(td[0]), int.Parse(td[1]), int.Parse(td[2]), 0, 0, 0);
            }
            catch
            {
                res = new DateTime(1990, 1, 1);
            }


            return res;
        }

        public static DateTime ConvertDateDDMMYYYYEnd(string datetime)
        {
            DateTime res = new DateTime(1990, 1, 1);
            string[] td1 = datetime.Split(' ');
            string[] td = td1[0].Split('-');
            try
            {
                res = new DateTime(int.Parse(td[0]), int.Parse(td[1]), int.Parse(td[2]), 23, 59, 59);
            }
            catch
            {
                res = new DateTime(1990, 1, 1);
            }


            return res;
        }

        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }
        public static bool checkVideoFile(string extension)
        {
            bool res = false;
            IList<string> AllowedVideo = new List<string> { ".3gp", ".3g2", ".asf", ".avi", ".f4v", ".flv", ".ismv", ".m4v", ".mkv", ".mov", ".mp4", ".mpeg", ".ogv", ".wmv", ".webm" };
            if (AllowedVideo.Contains(extension))
            {
                res = true;
            }
            return res;

        }
        public static bool checkImageFile(string extension)
        {
            bool res = false;
            IList<string> AllowedImage = new List<string> { ".bmp", ".exr", ".ico", ".jpg", ".jpeg", ".gif", ".pbm", ".pcx", ".pgm", ".png", ".ppm", ".psd", ".tif", ".tiff", ".tga", ".wbmp", ".heic" };
            if (AllowedImage.Contains(extension))
            {
                res = true;
            }
            return res;

        }

        public static DateTime ConvertStringToDate(string strDate)
        {
            DateTime dateTime = new DateTime(1890, 1, 1);
            try
            {
                dateTime = DateTime.FromOADate(Double.Parse(strDate));
            }
            catch
            {
                try
                {
                    //dateTime = DateTime.Parse(strDate);
                    dateTime = DateTime.ParseExact(strDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    try
                    {
                        string[] strT = strDate.Split(' ');
                        if (strT.Length > 1)
                        {
                            string[] str1 = strT[0].Split('/');
                            string[] str2 = strT[1].Split(':');
                            if (str1.Length == 3)
                            {
                                dateTime = new DateTime(int.Parse(str1[2]), int.Parse(str1[1]), int.Parse(str1[0]), int.Parse(str2[0]), int.Parse(str2[1]), int.Parse(str2[2]));
                            }

                        }
                        else
                        {
                            string[] str = strDate.Split('/');
                            if (str.Length == 3)
                            {
                                dateTime = new DateTime(int.Parse(str[2]), int.Parse(str[1]), int.Parse(str[0]));
                            }
                        }
                    }
                    catch
                    {
                        string[] str = strDate.Split('/');
                        if (str.Length == 3)
                        {
                            dateTime = new DateTime(int.Parse(str[2]), int.Parse(str[1]), int.Parse(str[0]));
                        }
                    }
                }
            }


            return dateTime;
        }


        public static object TrimStringPropertyTypeObject(object obj)
        {
            var stringProperties = obj.GetType().GetProperties()
                          .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(obj, null);
                stringProperty.SetValue(obj, currentValue.Trim(), null);
            }

            return obj;
        }
    }
}
