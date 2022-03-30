using HomeCloud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeCloud.Web.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public FilesController()
        {

        }

        [HttpPost]
        public IActionResult SaveFile(Change change, IFormFile file)
        {
            if (change is null) return BadRequest(nameof(change));
            return Ok();
        }
    }
}
