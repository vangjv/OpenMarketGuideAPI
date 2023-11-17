using OMG.Domain.Market.Models;

namespace OMG.Infrastructure.CosmosDbData.Interfaces
{
    public interface IMarketRepository : IRepository<Market>
    {
        Task<IEnumerable<Market>> GetMarketsAsyncByUser(string userId);
    }
}
