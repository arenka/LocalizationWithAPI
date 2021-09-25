using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationWithAPI.MultiLanguageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public string Get()
        {
            return _localizer["HELLO"];
        }
        //Parametre olarak gönderilmek istenir ise bu şekil kullanılabilinir.
        //[HttpGet("{name}")]
        //public string Get(string name)
        //{
        //    return _localizer[name];
        //}
    }

}
