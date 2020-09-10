using Api.Configuration;
using Api.DAL.EF;
using AutoMapper;

namespace Api.IntegrationTests.ServiceTests
{
    public class BaseTest
    {
        protected readonly ConnectionStringDto _testcs;
        private static bool _isInitialized = false;

        public BaseTest()
        {
            _testcs = new ConnectionStringDto();
            _testcs.ConnectionString = "Server=BASTION-1603\\UCZELNIA;Initial Catalog=Kawiarnia;User ID=Coffe;Password=coffe;Connection Timeout=30;";
            if (!_isInitialized)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<AutoMapperProfile>();
                });
                _isInitialized = true;
            }
        }
    }
}
