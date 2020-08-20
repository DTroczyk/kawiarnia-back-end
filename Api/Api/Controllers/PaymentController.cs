using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.BLL.Entity;
using Api.ViewModels.ViewModel;
using Api.DAL.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using Api.Services.Interfaces;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;

        public PaymentController(IUserService userService, IPaymentService paymentService)
        {
            _userService = userService;
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult<Session>> StartPayment(IList<OrderVm> orderVms)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);
            try
            {
                var session = await _paymentService.StatrPayment(orderVms, username);
                return Ok(session);
            }
            catch (Exception e)
            {
                return StatusCode(406, new { message = e.Message, status = 406});
            }
        }

        [HttpPut]
        [Route("success")]
        public async Task<ActionResult<Session>> FinishPayment()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);

            await _paymentService.Success(username);

            return Ok();
        }

        [HttpPut]
        [Route("cancel")]
        public async Task<ActionResult<Session>> CancelPayment()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);

            await _paymentService.Cancel(username);

            return Ok();
        }
    }
}
