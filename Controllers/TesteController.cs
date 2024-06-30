using Microsoft.AspNetCore.Mvc;

namespace apiFornecedor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {

        [HttpGet] public ActionResult PegarProdutos()
        {
            return Ok();
        }
    }
}
