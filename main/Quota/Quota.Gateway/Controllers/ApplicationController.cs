using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationActor.Interfaces;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors;
using System.Fabric;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quota.Gateway.Controllers
{
    [Route("Gateway/[controller]")]
    public class ApplicationController : Controller
    {     
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var actor = GetActor("1");
            var response = await actor.GetHelloWorldAsync();
            return Ok(response);
        }

        private IApplicationActor GetActor(string userId)
        {
            return ActorProxy.Create<IApplicationActor>(new ActorId(userId), new Uri("fabric:/Quota/ApplicationActorService"));
        }
    }
}
