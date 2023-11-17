using OMG.Domain.Market.ValueObjects;
using OMG.SharedKernel.Entities.Base;

namespace OMG.Domain.Market.Models
{
    public class ThreeDModelCollection:BaseEntity
    {
        public string Owner { get; set; }
        public List<ThreeDModelInfo> Models { get; set; }
    }
}
