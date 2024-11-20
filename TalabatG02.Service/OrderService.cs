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

namespace TalabatG02.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPaymentService paymentService;

        //private readonly IGenericRepository<Product> productRepo;
        //private readonly IGenericRepository<DeliveryMethod> dmRepository;
        //private readonly IGenericRepository<Order> orderRepository;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.paymentService = paymentService;
            //this.productRepo = productRepo;
            //dmRepository = DmRepository;
            //this.orderRepository = orderRepository;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmiail, string basketId, int deliveryMethodId, Address shippingAddress)
        {

            var basket = await basketRepository.GetBasketAsync(basketId);


            var orderItems = new List<OrderItem>();

            if(basket?.Items?.Count >0) 
            {
                foreach(var item in basket.Items)
                {
                    var Product = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var productItemOrdered = new ProductOrderItem(Product.Id, Product.Name, Product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, Product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }
            }

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var spec = new OrderWithPaymentSpecification(basket.PaymentIntentId);
            var exsistOrder = await unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if(exsistOrder is null)
            {
                unitOfWork.Repository<Order>().Delete(exsistOrder);

                await paymentService.CreatOrUpdatepaymentIntent(basket.Id);
            }

            var Order = new Order(BuyerEmiail, shippingAddress,basket.PaymentIntentId, deliveryMethod, orderItems, subTotal);

            await unitOfWork.Repository<Order>().Add(Order);

            var result = await unitOfWork.Complete();
            if (result <= 0) return null;

            return Order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new Orderspecification(buyerEmail);
            var orders = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return orders;
        }

        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new Orderspecification(orderId, buyerEmail);
            var Order = await unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            return Order;   
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

            return deliveryMethods; 
        }
    }
}
