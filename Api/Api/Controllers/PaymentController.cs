using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.ViewModels.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
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
        public async Task<ActionResult> StartPayment(OrderItemsVm itemsVm)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);
            try
            {
                if (await _paymentService.Payment(itemsVm,username))
                {
                    await _paymentService.Success(username);
                    return Ok(new { status = 200, message = "Payment complete" });
                }
                await _paymentService.Cancel(username);
                return StatusCode(406, new { status = 406, message = "Something went wrong." });
            }
            catch (Exception e)
            {
                await _paymentService.Cancel(username);
                return StatusCode(406, new { status = 406, message = "Something went wrong." });
            }
        }

        [HttpPost]
        [Route("onDelivery")]
        public async Task<ActionResult> OnDelivery(OrderItemsVm itemsVm)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);
            try
            {
                if (await _paymentService.PaymentOnDelivery(itemsVm, username))
                {
                    await _paymentService.Success(username);
                    return Ok(new { status = 200, message = "Order complete" });
                }
                await _paymentService.Cancel(username);
                return StatusCode(406, new { status = 406, message = "Something went wrong." });
            }
            catch (Exception e)
            {
                await _paymentService.Cancel(username);
                return StatusCode(406, new { status = 406, message = e.Message });
            }
        }
    }
}
