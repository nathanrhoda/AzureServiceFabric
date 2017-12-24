using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyService.Model
{
    public interface IFeedProvider
    {
        CurrencyFeed GetFeed();
    }
}
