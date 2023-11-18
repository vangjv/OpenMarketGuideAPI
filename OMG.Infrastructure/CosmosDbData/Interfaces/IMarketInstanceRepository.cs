using OMG.Domain.Market.Models;

namespace OMG.Infrastructure.CosmosDbData.Interfaces
{
    public interface IMarketInstanceRepository : IRepository<MarketInstance>
    {
        Task<IEnumerable<MarketInstance>> GetMarketInstancesByMarketIdAsync(string marketId);
    }
}
