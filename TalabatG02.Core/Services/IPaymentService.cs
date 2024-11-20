using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG02.Core.Entities;
using TalabatG02.Core.Entities.OrderAggregtion;

namespace TalabatG02.Core.Services
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreatOrUpdatepaymentIntent(string basketid);
        Task<Order> UpbatePaymentToSucceedOrFaild(string IntentId, bool isSucced);

    }
}
