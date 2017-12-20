using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyService.Controllers
{
    [Route("api/[controller]")]
    public class CurrenciesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Currency" };
        }    
    }
}
