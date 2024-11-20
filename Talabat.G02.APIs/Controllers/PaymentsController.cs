using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using TalabatG02.APIs.Dtos;
using TalabatG02.APIs.Errors;
using TalabatG02.Core.Entities;
using TalabatG02.Core.Services;

namespace TalabatG02.APIs.Controllers
{
    
    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private const string _whSecret = "whsec_5f24de30e4be5a053daab055cc58381337d481f762786738441af627426a4ac1";



        public PaymentsController(IPaymentService paymentService, IMapper mapper,ILogger<PaymentsController> logger)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;
            this.logger = logger;
        }



        [ProducesResponseType(typeof(CustomerBaskerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        [HttpPost("{basketId}")]//post: /api/payments?id=basketId  
        public async Task<ActionResult<CustomerBaskerDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await paymentService.CreatOrUpdatepaymentIntent(basketId);

            if (basket is null) return BadRequest(new ApiErrorResponse(400, "A problem with your Basket"));

            var mappedbasket = mapper.Map<CustomerBasket, CustomerBaskerDto>(basket);

            return Ok(mappedbasket);
        }
        

        [HttpPost("webhook")]
        public async Task<IActionResult> Stripwebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
           
            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], _whSecret);

            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;

            switch(stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    await paymentService.UpbatePaymentToSucceedOrFaild(paymentIntent.Id, true);
                    logger.LogInformation("payment is succed ya lolo", paymentIntent.Id);
                    break;
                case Events.PaymentIntentPaymentFailed:
                    await paymentService.UpbatePaymentToSucceedOrFaild(paymentIntent.Id, false);
                    logger.LogInformation("payment is Faild ya lolo :(", paymentIntent.Id);
                    break;
            }

            return Ok();
           

        }




    }
}
