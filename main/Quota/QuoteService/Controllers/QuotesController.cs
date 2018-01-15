using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Data;
using QuoteService.Model;
using QuoteService.Repository;
using System;
using System.Collections.Generic;

namespace QuoteService.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private readonly IReliableStateManager _stateManager;
        public IQuoteRepository Repository;

        public QuotesController(IReliableStateManager stateManager)
        {
            this._stateManager = stateManager;
            Repository = new QuoteRepository(_stateManager);
        }

        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            return Repository.Get().Result;
        }

        [HttpGet("{id}")]
        public Quote Get(Guid id)
        {
            return Repository.Get(id).Result;
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
