using Microsoft.AspNetCore.Mvc;
using QuotationService.Model;
using System.Collections.Generic;

namespace QuotationService.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private readonly IQuotationService service;

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
        public string Generate([FromBody] QuoteRequest request)
        {
            if (request == null)
            {
                return "Failure";
            }

            if (!request.IsValid())
            {
                return "Failure";
            }


            return "Success";
        }
    }
}
