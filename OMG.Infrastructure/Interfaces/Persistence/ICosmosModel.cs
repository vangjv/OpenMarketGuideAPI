using Newtonsoft.Json;

namespace OMG.Infrastructure.Interfaces.Persistence
{
    public interface ICosmosModel
    {
        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}
