using CoreAPI.Domain.Dto;
using CoreAPI.Engine.Engine.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CoreAPI.Controllers
{
    public class BillingItemController : Controller
    {
        private ILogger<BillingItemController> logger;
        private IBillingItemEngine engine;
        
        public BillingItemController(ILogger<BillingItemController> _logger, IBillingItemEngine _engine)
        {
            this.logger = _logger;
            this.engine = _engine;
        }

        
    }
}
