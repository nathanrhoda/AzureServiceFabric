using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotationService.Model;

namespace QuotationService.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public string Generate(QuoteRequest request)
        {            
            if (request == null)
            {
                return "Failure";
            }

            if(!request.IsValid())
            {
                return "Failure";                
            }

            return "Success";
        }
    }
}
