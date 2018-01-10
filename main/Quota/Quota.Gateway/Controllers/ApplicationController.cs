using ApplicationActor.Interfaces;
using Mailer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace Quota.Gateway.Controllers
{
    [Route("Gateway/[controller]")]
    public class ApplicationController : Controller
    {
        private static readonly Random rnd = new Random(DateTime.UtcNow.Second);
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var applicationActor = GetApplicationActor("1");
            var response = await applicationActor.GetHelloWorldAsync();
    
            var mailer = GetMailerService();
            string messge = await mailer.SendMailAsync(response);
            return Ok(messge);
        }

        private IApplicationActor GetApplicationActor(string userId)
        {
            return ActorProxy.Create<IApplicationActor>(new ActorId(userId), new Uri("fabric:/Quota/ApplicationActorService"));
        }

        private IMailerService GetMailerService()
        {
            long key = LongRandom();

            return ServiceProxy.Create<IMailerService>(
                   new Uri("fabric:/Quota/MailerService"),
                   new ServicePartitionKey(key));
        }

        private long LongRandom()
        {
            byte[] buf = new byte[8];
            rnd.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return longRand;
        }
    }
}
