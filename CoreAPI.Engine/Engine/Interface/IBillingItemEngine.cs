using System.Linq;
using CoreAPI.Domain.Dto;

namespace CoreAPI.Engine.Engine.Interface
{
    public interface IBillingItemEngine
    {
        IQueryable<BillingItem> GetAllBillingItems();
    }
}
