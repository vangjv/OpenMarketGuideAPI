using OMG.Domain.Market.Models;
using OMG.Infrastructure.CosmosDbData.DatabaseModel;

namespace OMG.Infrastructure.CosmosDbData.Interfaces
{
    public interface IVendorRepository : IRepository<VendorDM>
    {
        Task<IEnumerable<VendorDM>> GetMyVendorsAsync(string userId);
    }
}
