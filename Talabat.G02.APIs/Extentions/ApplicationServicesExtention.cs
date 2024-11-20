using Microsoft.AspNetCore.Mvc;
using TalabatG02.APIs.Errors;
using TalabatG02.APIs.Helpers;
using TalabatG02.Core;
using TalabatG02.Core.Repositories;
using TalabatG02.Core.Services;
using TalabatG02.Repository;
using TalabatG02.Service;

namespace TalabatG02.APIs.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices( this IServiceCollection Services)
        {
            //Services
            Services.AddScoped<IOrderService,OrderService>(); 
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IPaymentService, Paymentservice>();
            //Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfiles));                       
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                        .SelectMany(p => p.Value.Errors)
                                                        .Select(E => E.ErrorMessage).ToArray();
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            return Services;
        }
    }
}
