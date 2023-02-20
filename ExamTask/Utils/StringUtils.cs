using System;

namespace ExamTask.Utils
{
    internal static class StringUtils
    {
        public static string GetTrimString(string strForTrim, string separator)
        {
            string upgradedString = strForTrim.Split(separator).Last();
            return upgradedString;
        }
    }
}
