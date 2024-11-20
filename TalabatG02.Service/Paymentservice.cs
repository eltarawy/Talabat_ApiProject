using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG02.Core;
using TalabatG02.Core.Entities;
using TalabatG02.Core.Entities.OrderAggregtion;
using TalabatG02.Core.Repositories;
using TalabatG02.Core.Services;
using TalabatG02.Core.Specification.Order_spec;
using Product = TalabatG02.Core.Entities.Product;

namespace TalabatG02.Service
{
    public class Paymentservice : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        public Paymentservice(IConfiguration configuration, IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreatOrUpdatepaymentIntent(string basketid)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:Secretkey"];

            var basket = await basketRepository.GetBasketAsync(basketid);

            if (basket is null) return null;

            var shippingPrice = 0m;
            if(basket.DeliveryMethodsId.HasValue)
            {
                var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodsId.Value);
                shippingPrice = deliveryMethod.Cost;
                basket.ShippingCost = deliveryMethod.Cost;
            }

            if(basket?.Items?.Count > 0)
            {
                foreach(var item in basket.Items)
                {
                    var product =  await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long) basket.Items.Sum(item => item.Quantity * 100) + (long) shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id; 
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Quantity * 100) + (long)shippingPrice * 100
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);

            }

            await basketRepository.UpdateBasketAsync(basket);
            return basket;

        }

        public async Task<Order> UpbatePaymentToSucceedOrFaild(string IntentId, bool isSucced)
        {
            var spec = new OrderWithPaymentSpecification(IntentId);
            var order = await unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if (isSucced)
                order.Status = OrderStatus.PaymentRecived;
            else
                order.Status = OrderStatus.PaymentFailed;

            unitOfWork.Repository<Order>().Update(order);

            await unitOfWork.Complete();

            return order;
        }
    }
}
