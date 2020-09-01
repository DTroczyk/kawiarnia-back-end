using Api.Configuration;
using AutoMapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.UnitTests.ControllerTests
{
    class BucketsControllerTests
    {
        public BucketsControllerTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }

        [Test]
        public void GetBucketItems()
        {

        }
    }
}
