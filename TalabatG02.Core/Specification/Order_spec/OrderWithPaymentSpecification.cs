using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = TalabatG02.Core.Entities.OrderAggregtion.Order;

namespace TalabatG02.Core.Specification.Order_spec
{
    public class OrderWithPaymentSpecification : BaseSpecification<Order>
    {
        public OrderWithPaymentSpecification(string IntentId)
            :base(o => o.PaymentIntentId == IntentId) 
        {
            
        }
    }
}
