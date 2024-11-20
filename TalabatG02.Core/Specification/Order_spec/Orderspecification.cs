using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG02.Core.Entities.OrderAggregtion;

namespace TalabatG02.Core.Specification.Order_spec
{
    public class Orderspecification : BaseSpecification<Order>
    {
        public Orderspecification(string email)
            :base(O =>O.BuyerEmail == email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDescending(O => O.OrderDate);
        }

        public Orderspecification(int id, string email)
           : base(O => O.BuyerEmail == email && O.Id == id)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        } 
    }
}
