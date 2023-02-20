using ExamTask.Utils;

namespace ExamTask.TestData
{
    internal static class TestDataManager
    {
        public static readonly TestData? testData = FileReader.ReadJsonData<TestData>(@"Resources\testData.json");
    }
}
