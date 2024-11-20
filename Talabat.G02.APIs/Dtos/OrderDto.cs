using System.ComponentModel.DataAnnotations;
using TalabatG02.Core.Entities.OrderAggregtion;

namespace TalabatG02.APIs.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
