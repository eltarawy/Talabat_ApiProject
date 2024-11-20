using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatG02.APIs.Dtos;
using TalabatG02.APIs.Errors;
using TalabatG02.Core.Entities;
using TalabatG02.Core.Repositories;

namespace TalabatG02.APIs.Controllers
{
    public class BasketsController : ApiBaseController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper) 
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            return basket is null ? new CustomerBasket(id) : basket;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBaskerDto>> UpdateBasket(CustomerBaskerDto basket)
        {
            var mappedBasket = mapper.Map<CustomerBaskerDto,CustomerBasket>(basket);

            var CreatOrUpdateBasket = await basketRepository.UpdateBasketAsync(mappedBasket);
            if (CreatOrUpdateBasket is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(basket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket (string id)
        {
              return await basketRepository.DeleteBasketAsync(id);
        }

    }
}
