﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatG02.APIs.Errors;
using TalabatG02.Repository.Data;

namespace TalabatG02.APIs.Controllers
{
    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet("Not Found")]
        public ActionResult GetNotFoundRequest()
        {
            var product = context.products.Find(100);
            if(product is null)
                return NotFound(new ApiErrorResponse(404));

            return Ok(product);
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var product = context.products.Find(100);
            var productToReturn = product.ToString();

            return Ok(productToReturn);
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
