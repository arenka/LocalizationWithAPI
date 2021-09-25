using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocalizationWithAPI.BAPI.Controllers
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
            using (var client = new HttpClient())
            {
                var urlName = _config.GetSection("MultiLanguageAPIUrl").GetSection("URL").Value;
                client.BaseAddress = new Uri(urlName);
                var culture = HttpContext.Request.Query["culture"];
                if (!string.IsNullOrEmpty(culture))
                {
                    urlName += "?culture=" + culture;
                }
                else
                {
                    //eğer querystringde culture girilmemiş ise a apisinden default olarak türkçe gönderildiği için b apisinden statik olarak ingilizce gönderiliyor. 
                    urlName += "?culture=en-US";
                }
                var messge = client.GetAsync(urlName).Result;
                return messge.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
