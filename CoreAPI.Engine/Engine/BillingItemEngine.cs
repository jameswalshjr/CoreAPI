using CoreAPI.Data.Repository.Interface;
using CoreAPI.Domain.Dto;
using System.Linq;


namespace CoreAPI.Engine.BillingItemEngine
{
    public class BillingItemEngine
    {
        private IBillingItemRepository itemRepo;

        public BillingItemEngine(IBillingItemRepository repo)
        {
            itemRepo = repo;
        }

        public IQueryable<BillingItem> GetAllBillingItems()
        {
            var billingItems = itemRepo.GetAllBillingItemsFromDb();
            if(billingItems.Any())
            {
                return billingItems;
            }
            else
            {
                return Enumerable.Empty<BillingItem>().AsQueryable();
            }
        }
    }
}
