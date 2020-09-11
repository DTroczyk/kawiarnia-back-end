using Api.BLL.Entity;
using Api.DAL.EF;
using Api.IntegrationTests.TestAttributes;
using Api.Services.Services;
using Api.ViewModels.ViewModel;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Api.IntegrationTests.ServiceTests
{
    class HistoryServiceTests : BaseTest
    {
        public HistoryServiceTests() : base()
        {
           
        }

        [Test, Isolated]
        public void CountHistoryItems_ValidUser_ShouldReturnItemsCount()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new HistoryService(context);
            var username = "Test";

            var historyEntities = context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.IsPaymentCompleted == true)
                .Where(o => o.ClientId == username)
                .ToList();
            int expectedResult = 0;
            foreach (Order order in historyEntities)
            {
                expectedResult += order.Items.Count();
            }

            int itemsCount = service.CountHistoryItems(username).Result;

            Assert.That(itemsCount, Is.EqualTo(expectedResult));
        }

        [Test, Isolated]
        public void CountHistoryItems_InvalidUser_ShouldReturnZero()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new HistoryService(context);

            int itemsCount = service.CountHistoryItems("Te").Result;

            Assert.That(itemsCount, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void GetHistory_ValidUser_ShouldReturnItem()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new HistoryService(context);
            var username = "Test";

            var historyItem = context.Orders
                .Where(o => o.ClientId == username)
                .FirstOrDefault(o => o.IsPaymentCompleted == true);
            var expectedHistoryItemId = historyItem.Id;

            var order = service.GetHistory(expectedHistoryItemId, username).Result;

            Assert.That(order.id, Is.EqualTo(expectedHistoryItemId));
            Assert.That(order, Is.TypeOf<HistoryVm>());
        }

        [Test, Isolated]
        public void GetHistory_InvalidUser_ShouldReturnNull()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new HistoryService(context);
            
            var item = service.GetHistory(1, "Te").Result;

            Assert.IsNull(item);
        }

        [Test, Isolated]
        public void GetHistory_InvalidId_ShouldReturnNull()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new HistoryService(context);

            var item = service.GetHistory(100, "Test").Result;

            Assert.IsNull(item);
        }

        [Test, Isolated]
        public void GetHistories_ValidUser_ShouldReturnItems()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new HistoryService(context);
            var username = "Test";

            var order = service.GetHistories(username).Result;

            Assert.IsNotNull(order);
        }

        [Test, Isolated]
        public void GetHistories_InvalidUser_ShouldReturnEmpty()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new HistoryService(context);

            var item = service.GetHistories("Te").Result;

            Assert.IsEmpty(item);
        }
    }
}
