using CoreAPI.Domain.Dto;
using System.Linq;

namespace CoreAPI.Data.Repository.Interface
{
    public interface IBillingItemRepository
    {
        IQueryable<BillingItem> GetAllBillingItemsFromDb();
    }
}
