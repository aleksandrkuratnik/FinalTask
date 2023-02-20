using System;

namespace ExamTask.Models
{
    internal class ApiMethods
    {
        public string GetTokenUrl { get; init; }
        public string GetTestsUrl { get; init; }
        public string PostNewTestUrl { get; init; }
        public string AddLogsToTestUrl { get; init; }
        public string AddScreenshotToTestUrl { get; init; }
    }
}
