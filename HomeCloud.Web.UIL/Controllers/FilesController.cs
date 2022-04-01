using HomeCloud.FSWatcher;
using HomeCloud.Shared.Dtos;
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
        public IActionResult SaveFile([FromForm] FileClientTransferDto fileClient)
        {
            if (fileClient is null) return BadRequest(nameof(fileClient));
            return Ok();
        }
    }
}
