using ExamTask.Models;
using ExamTask.Utils;

namespace ExamTask.TestConfig
{
    internal static class TestConfigManager
    {
        public static readonly TestsConfig? testConfig = FileReader.ReadJsonData<TestsConfig>(@"Resources\testConfig.json");
    }
}
