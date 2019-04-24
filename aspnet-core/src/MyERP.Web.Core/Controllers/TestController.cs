using Abp.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace MyERP.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController : MyERPControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var currentCulture = CultureInfo.CurrentUICulture;
            var sources = LocalizationManager.GetAllSources().OrderBy(s => s.Name).ToArray();

            foreach (var source in sources)
            {
                var stringValues = source.GetAllStrings(currentCulture).OrderBy(s => s.Name).ToList();
                var stringDictionary = stringValues
                    .ToDictionary(_ => _.Name, _ => _.Value);
            }

            return Content("111");
        }
    }
}
