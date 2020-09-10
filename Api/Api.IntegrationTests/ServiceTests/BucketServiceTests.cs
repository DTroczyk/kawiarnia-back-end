using Api.Configuration;
using Api.DAL.EF;
using Api.IntegrationTests.TestAttributes;
using Api.Services.Services;
using AutoMapper;
using NUnit.Framework;
using System.Linq;

namespace Api.IntegrationTests.ServiceTests
{
    public class BucketServiceTests : BaseTest
    {
        public BucketServiceTests() : base()
        {
            
        }

        [Test, Isolated]
        public void GetBucketItems_ItemsInBucket_ShouldReturnListOfOrderItem()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new BucketService(context);

            var items = service.GetBucketItems("Test");
            items.Wait();
            var itemsCount = items.Result.Count();

            Assert.That(itemsCount, Is.EqualTo(2));
        }

        [Test, Isolated]
        public void GetBucketItems_EmptyBucket_ShouldReturnEmptyList()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new BucketService(context);

            var items = service.GetBucketItems("Te"); // User doesn't exist;
            items.Wait();
            var itemsCount = items.Result.Count();

            Assert.That(itemsCount, Is.EqualTo(0));
        }
    }
}
