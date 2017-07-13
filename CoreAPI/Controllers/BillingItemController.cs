using CoreAPI.Domain.Dto;
using CoreAPI.Engine.Engine.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;

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

        [HttpGet]
        [Route("api/V1/AllItems")]
        public IActionResult AllItems()
        {
            try
            {
                var items = engine.GetAllBillingItems();

                if (items.Any())
                {
                    return Ok(items);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.InnerException.Message);
                logger.LogError(ex.InnerException.Message);
            }
            

        }
    }
}
