using AutoMapper.QueryableExtensions;
using CoreAPI.Data.Repository.Interface;
using CoreAPI.Data.Resource;
using CoreAPI.Domain.Dto;
using System.Linq;

namespace CoreAPI.Data.Repository
{
    public class BillingItemRepository : IBillingItemRepository
    {
        private DevSandBoxContext dbContext;

        public BillingItemRepository(DevSandBoxContext context)
        {
            this.dbContext = context;
        }

        public IQueryable<BillingItem> GetAllBillingItemsFromDb()
        {
            var billingItem =
                from item in dbContext.BillingItems
                select item;

            return billingItem.ProjectTo<BillingItem>();
        }
    }
}
