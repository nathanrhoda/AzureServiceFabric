using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Threading.Tasks;

namespace Mailer.Interfaces
{
    public interface IMailerService: IService
    {
        Task<string> SendMailAsync(string name);
    }
}
