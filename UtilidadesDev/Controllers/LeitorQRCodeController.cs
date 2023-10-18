using Microsoft.AspNetCore.Mvc;

namespace UtilidadesDev.Controllers
{
    public class LeitorQRCodeController : Controller
    {
        [Route("/LeitorQRCode")]
        public IActionResult LeitorQRCode()
        {
            return View();
        }
    }
}
