using Newtonsoft.Json;

namespace ExamTask.Models
{
    internal class TestsModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        public string? EndTime { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        public static List<TestsModel> SortPostCollectionByDescending(List<TestsModel>? listPostModels)
        {
            List<TestsModel> listForSort = new();
            if (listPostModels != null)
                listForSort.AddRange(listPostModels);
            var sortedList = listForSort.OrderByDescending(p => p.StartTime);
            return new List<TestsModel>(sortedList);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as TestsModel);
        }

        public bool Equals(TestsModel? other)
        {
            return other is not null &&
                   Duration.ToLower() == other.Duration &&
                   Method.ToLower() == other.Method &&
                   Name.ToLower() == other.Name &&
                   StartTime.ToLower() == other.StartTime &&
                   Status.ToLower() == other.Status;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Duration, Method, Name, StartTime, EndTime, Status);
        }
    }
}
