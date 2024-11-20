using System.ComponentModel.DataAnnotations;

namespace TalabatG02.APIs.Dtos
{
    public class CustomerBaskerDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodsId { get; set; }
        public decimal ShippingCost { get; set; }


    }
}
