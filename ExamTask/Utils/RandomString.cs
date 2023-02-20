using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask.Utils
{
    internal static class RandomString
    {
        private const string StringForRandomLetters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbn";
        private const int StringLength = 10;

        public static string GetGeneratedRandomString()
        {
            Random random = new();
            return new string(Enumerable.Repeat(StringForRandomLetters, StringLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
