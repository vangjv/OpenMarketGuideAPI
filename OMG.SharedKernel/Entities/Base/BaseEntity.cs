using Newtonsoft.Json;
namespace OMG.SharedKernel.Entities.Base
{
    public abstract class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }
        public virtual int SchemaVersion { get; set; }
    }
}
