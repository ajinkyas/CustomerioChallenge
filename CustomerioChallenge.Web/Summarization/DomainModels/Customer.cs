using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CustomerioChallenge.Summarization.DomainModels
{
    public class Customer
    {
        public int Id { get; set; }

        [JsonIgnore]
        public IDictionary<string, TimedValue> AttributesInfo { get; set; }
        public IDictionary<string, string> Attributes => AttributesInfo.ToDictionary(p => p.Key, p => p.Value.Value);

        [JsonIgnore]
        public IDictionary<string, HashSet<string>> OneTimeActivities { get; set; }
        public IDictionary<string, int> Events => OneTimeActivities.ToDictionary(p => p.Key, p => p.Value.Count);
        
        [JsonPropertyName("last_updated")]
        public long UpdatedTimestamp { get; internal set; }
    }

    public class TimedValue
    {
        public string Value { get; set; }
        public long LastUpdatedTimestamp { get; set; }
    }
}