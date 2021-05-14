using System.Linq;

namespace TheMonarchs.Core.Utilities
{
    public static class DataHelper
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputYears"></param>
        /// <returns></returns>
        public static (int? startYear, int? endYear)? ExtractStartEndYear(string inputYears)
        {
            int? startYear = null;
            int? endYear = null;

            if (string.IsNullOrWhiteSpace(inputYears))
                return null;

            var yearParts = inputYears.Split('-');

            int parsedYear;

            if (int.TryParse(yearParts[0], out parsedYear))
            {
                startYear = parsedYear;
            }

            if (yearParts.Length > 1 && int.TryParse(yearParts[1], out parsedYear))
            {
                endYear = parsedYear;
            }

            return (startYear, endYear);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static (string firstName, string lastName)? ExtractFirstLastName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return null;

            var nameParts = fullName.Split(' ');

            if (nameParts.Length > 1)
                return (nameParts[0], string.Join(' ', nameParts.Skip(1)));

            return (nameParts[0], null);
        }
    }
}
