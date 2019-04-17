using Microsoft.AspNetCore.Antiforgery;
using MyERP.Controllers;

namespace MyERP.Web.Host.Controllers
{
    public class AntiForgeryController : MyERPControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
