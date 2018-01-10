using Mailer.Interfaces;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;

namespace MailerService
{
    internal sealed class MailerService : StatefulService, IMailerService
    {
        public MailerService(StatefulServiceContext context)
            : base(context)
        { }

        public async Task<string> SendMailAsync(string name)
        {
            return await Task.FromResult($"Hello {name}");
        }

        
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(context => this.CreateServiceRemotingListener(context))
            };

        }
    }
}
