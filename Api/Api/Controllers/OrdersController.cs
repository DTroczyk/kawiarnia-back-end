using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.BLL.Entity;
using Api.ViewModels.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Api.Services.Interfaces;

namespace Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public OrdersController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        // GET: Orders
        [HttpGet]
        public async Task<ActionResult<OrderVm>> GetOrderItem(int orderId)
        {
            var orderVm = await _orderService.GetOrderItem(orderId);

            return Ok(orderVm);
        }

        // POST: Orders
        [HttpPost]
        public ActionResult<OrderVm> PostOrderItem(OrderVm orderVm)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);

            try
            {
                var userVm = _orderService.AddOrderItem(orderVm, username);
                return StatusCode(201, new { status = 201, user = userVm });
            }
            catch(Exception e)
            {
                return StatusCode(406, new { message = e.Message, status = 406 });
            }

        }

        // DELETE: Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderItem>> DeleteOrderItem(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);

            try
            {
                var orderVm = _orderService.DeleteOrderItem(id, username);
                return Ok(new { status = 200, order = orderVm });
            }
            catch (Exception e)
            {
                if (e.Message == "Method Not Allowed")
                {
                    return StatusCode(405, new { message = e.Message, status = 405 });
                }
                else
                {
                    return StatusCode(406, new { message = e.Message, status = 406 });
                }
            }
        }
    }
}
