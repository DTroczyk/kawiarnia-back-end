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

        private string getUserName()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            return claims[0].Value;
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
                return StatusCode(406, new { message = e.Message});
            }
        }

        //[HttpPut]
        //[Route("accept")]
        //public async Task<ActionResult<Session>> FinishPayment()
        //{
        //    var username = getUserName();

        //    var bucketEntity = await _context.Orders
        //        .Include(o => o.Items)
        //            .ThenInclude(c => c.Coffee)
        //        .Where(o => o.ClientId == username)
        //        .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

        //    var items = bucketEntity.Items.Where(i => i.PaymentStatus == (PaymentStatus)2);

        //    foreach (BLL.Entity.OrderItem item in items)
        //    {
        //        item.PaymentStatus = (PaymentStatus)3;
        //        _context.OrderItems.Update(item);
        //    }

        //    if (items.Count() == bucketEntity.Items.Count())
        //    {
        //        bucketEntity.IsPaymentCompleted = true;
        //        bucketEntity.OrderDate = DateTime.Now;
        //        _context.Orders.Update(bucketEntity);
        //        await _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        var newBucket = new BLL.Entity.Order();
        //        newBucket.ClientId = bucketEntity.ClientId;
        //        await _context.SaveChangesAsync();
        //    }



        //    return Ok();
        //}

        //[HttpPut]
        //[Route("denied")]
        //public async Task<ActionResult<Session>> CancelPayment()
        //{
        //    var username = getUserName();

        //    var bucketEntity = await _context.Orders
        //        .Include(o => o.Items)
        //            .ThenInclude(c => c.Coffee)
        //        .Where(o => o.ClientId == username)
        //        .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

        //    foreach (BLL.Entity.OrderItem item in bucketEntity.Items)
        //    {
        //        item.PaymentStatus = (PaymentStatus)1;
        //        _context.Update(item);
        //    }
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
    }
}
