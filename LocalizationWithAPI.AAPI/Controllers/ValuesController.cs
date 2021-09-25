using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocalizationWithAPI.AAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ValuesController(IConfiguration config)
        {
            _config = config;
        }
        public string Get()
        {
            //burada işlemler yapılır iken multi language apisine istenir ise model veya string değerler yollanabilir.
            //ben multi language apisi içerisinde tanımlı olan string değeri döndürüyorum.
            using (var client = new HttpClient())
            {
                var urlName = _config.GetSection("MultiLanguageAPIUrl").GetSection("URL").Value;
                client.BaseAddress = new Uri(urlName);
                //culture query string olarak alınıyor.varsayılan olarak türkçe gönderiliyor.
                //istenir ise gerekli ayarlar yapılarak route içerisine culture tanımlanabilir. localhost:44385/tr-TR/api/home gibi.
                //localhost:44385/api/home?culture=en-US ile culture değiştirilir.
                var culture = HttpContext.Request.Query["culture"];
                if (!string.IsNullOrEmpty(culture))
                {
                    urlName += "?culture=" + culture;
                }
                var messge = client.GetAsync(urlName).Result;
                return messge.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
