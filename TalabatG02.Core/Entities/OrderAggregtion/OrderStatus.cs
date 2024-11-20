using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TalabatG02.Core.Entities.OrderAggregtion
{
    public enum OrderStatus
    {
        //DB  
        [EnumMember(Value = "Pending")]
        Pending, //0
        [EnumMember(Value = "Payment Recived")]
        PaymentRecived, //1
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed //2
    }
}
