using OMG.SharedKernel.Entities.Base;

namespace OMG.Domain.Market.ValueObjects
{
    public class ThreeDModelInfo
    {
        public string Name { get; set; }
        public string PreviewFile { get; set; }
        public string ModelUri { get; set; }
        public double DefaultScale { get; set; }
    }
}
