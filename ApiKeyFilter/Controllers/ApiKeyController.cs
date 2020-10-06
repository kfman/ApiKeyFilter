using Microsoft.AspNetCore.Mvc;

namespace ApiKeyFilter.Controllers {
    
    [ApiController]
    [Route("api/accessControl/[controller]")]
    public class ApiKeyController : ControllerBase {
        
        [HttpGet]
        public IActionResult Get() {
            return Ok("Hello from ApiKeyController");
        }
    }
}
