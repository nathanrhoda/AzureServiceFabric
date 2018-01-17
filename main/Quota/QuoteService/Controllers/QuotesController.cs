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
        public string Post([FromBody] QuoteRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return "Invalid Request";
            }

            try
            {
                Quote quote = new Quote();
                quote.Name = request.Name;
                quote.Surname = request.Surname;
                quote.Email = request.Email;
                quote.ContactNumber = request.ContactNumber;

                foreach (var item in request.Items)
                {

                    var orderItem = new OrderItem();
                    orderItem.productGuid = item.productGuid;
                    orderItem.Quantity = item.Quantity;
                    quote.Items.Add(orderItem);
                }

                Repository.AddQuote(quote);

                return quote.Id.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        public string Delete(Guid id)
        {
            try
            {
                Repository.Delete(id);
                return "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("id")]
        public string Put(Guid id, [FromBody] QuoteRequest request)
        {
            try
            {
                Quote quote = new Quote();
                quote.Name = request.Name;
                quote.Surname = request.Surname;
                quote.Email = request.Email;
                quote.ContactNumber = request.ContactNumber;

                foreach (var item in request.Items)
                {

                    var orderItem = new OrderItem();
                    orderItem.productGuid = item.productGuid;
                    orderItem.Quantity = item.Quantity;
                    quote.Items.Add(orderItem);
                }

                Repository.Put(id, quote);
                return "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
