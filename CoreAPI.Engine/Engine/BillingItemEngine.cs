using CoreAPI.Data.Repository.Interface;
using CoreAPI.Domain.Dto;
using CoreAPI.Engine.Engine.Interface;
using System.Linq;


namespace CoreAPI.Engine.BillingItemEngine
{
    public class BillingItemEngine : IBillingItemEngine
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
