using Api.DAL.EF;
using Api.IntegrationTests.TestAttributes;
using Api.Services.Services;
using Api.ViewModels.ViewModel;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace Api.IntegrationTests.ServiceTests
{
    class OrderServiceTests : BaseTest
    {
        public OrderServiceTests() : base()
        {
        }

        [Test, Isolated]
        public void AddOrderItem_CorrectData_ShouldReturnOrderVm()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new OrderService(context);
            OrderVm orderVm = new OrderVm()
            {
                coffeeName = "Latte",
                milkCount = 3,
                espressoCount = 3,
                isContainChocolate = false,
                price = 7.5
            };
            string username = "Test";

            var newOrderItem = service.AddOrderItem(orderVm, username);
            var orderFromDb = context.Orders
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Coffee)
                .Where(o => o.IsPaymentCompleted == false)
                .FirstOrDefault(o => o.ClientId == username);
            var orderItemFromDb = orderFromDb.Items
                .Where(o => o.Coffee.Name == "Latte")
                .Where(o => o.MilkCount == 3)
                .Where(o => o.EspressoCount == 3)
                .Where(o => o.IsContainChocolate == false)
                .FirstOrDefault(o => o.Price == 7.5);

            Assert.IsNotNull(orderItemFromDb);
            Assert.IsNotNull(newOrderItem);
            Assert.That(orderVm.coffeeName, Is.EqualTo(orderItemFromDb.Coffee.Name));
            Assert.That(orderVm.milkCount, Is.EqualTo(orderItemFromDb.MilkCount));
            Assert.That(orderVm.espressoCount, Is.EqualTo(orderItemFromDb.EspressoCount));
            Assert.That(orderVm.isContainChocolate, Is.EqualTo(orderItemFromDb.IsContainChocolate));
            Assert.That(orderVm.price, Is.EqualTo(orderItemFromDb.Price));
        }

        [Test, Isolated]
        public void AddOrderItem_IncorrectPrice_ShouldReturnOrderVmWithCorrectPrice()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new OrderService(context);
            OrderVm orderVm = new OrderVm()
            {
                coffeeName = "Latte",
                milkCount = 3,
                espressoCount = 3,
                isContainChocolate = false,
                price = 1
            };
            string username = "Test";

            var newOrderItem = service.AddOrderItem(orderVm, username);
            var orderFromDb = context.Orders
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Coffee)
                .Where(o => o.IsPaymentCompleted == false)
                .FirstOrDefault(o => o.ClientId == username);
            var orderItemFromDb = orderFromDb.Items
                .Where(o => o.Coffee.Name == "Latte")
                .Where(o => o.MilkCount == 3)
                .Where(o => o.EspressoCount == 3)
                .Where(o => o.IsContainChocolate == false)
                .FirstOrDefault(o => o.Price == 7.5);

            Assert.IsNotNull(orderItemFromDb);
            Assert.IsNotNull(newOrderItem);
            Assert.That(orderVm.coffeeName, Is.EqualTo(orderItemFromDb.Coffee.Name));
            Assert.That(orderVm.milkCount, Is.EqualTo(orderItemFromDb.MilkCount));
            Assert.That(orderVm.espressoCount, Is.EqualTo(orderItemFromDb.EspressoCount));
            Assert.That(orderVm.isContainChocolate, Is.EqualTo(orderItemFromDb.IsContainChocolate));
            Assert.That(7.5, Is.EqualTo(orderItemFromDb.Price));
        }

        [Test, Isolated]
        public void AddOrderItem_OrderVmIsNull_ShouldThrowException()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new OrderService(context);
            OrderVm orderVm = null;
            string username = "Test";
            Exception exception = null;

            try
            {
                var newOrderItem = service.AddOrderItem(orderVm, username);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("Order item is null"));
        }

        [Test, Isolated]
        public void DeleteOrderItem_CorrectData_ShouldReturnOrderVm()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new OrderService(context);
            var username = "Test";

            var orderItem = service.AddOrderItem(new OrderVm() { coffeeName = "YourOwn" }, username);
            var newItem = context.OrderItems.FirstOrDefault(oi => oi.Coffee.Name == "YourOwn");
            Assert.IsNotNull(newItem);
            var deletedItem = service.DeleteOrderItem(newItem.Id, username);
            var result = context.OrderItems.
                Any(oi => oi.Id == newItem.Id);

            Assert.IsFalse(result);
        }

        [Test, Isolated]
        public void DeleteOrderItem_OrderVmIsNull_ShouldThrowException()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new OrderService(context);
            string username = "Test";
            Exception exception = null;

            try
            {
                var newOrderItem = service.DeleteOrderItem(0, username);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("Order item is null"));
        }

        [Test, Isolated]
        public void DeleteOrderItem_IncorrectUsername_ShouldThrowException()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new OrderService(context);
            string username = "Te";
            Exception exception = null;

            try
            {
                var newOrderItem = service.DeleteOrderItem(7, username);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("Method Not Allowed"));
        }
    }
}
