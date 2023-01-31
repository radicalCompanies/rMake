using Microsoft.AspNetCore.Mvc;

namespace rLibrary.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PublishController : Controller
    {
        [HttpPost("Once")]
        public async Task<IActionResult> PublishFull() {
            throw new NotImplementedException();
        }
    }
}
