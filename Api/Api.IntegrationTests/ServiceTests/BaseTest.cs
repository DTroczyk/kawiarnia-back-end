using Api.DAL.EF;

namespace Api.IntegrationTests.ServiceTests
{
    public class BaseTest
    {
        protected readonly ConnectionStringDto _testcs;

        public BaseTest()
        {
            _testcs = new ConnectionStringDto();
            _testcs.ConnectionString = "Server=BASTION-1603\\UCZELNIA;Initial Catalog=Kawiarnia;User ID=Coffe;Password=coffe;Connection Timeout=30;";
        }
    }
}
