using Xunit;

namespace HospitalTest.Setup
{
    public class BaseIntegrationTest: IClassFixture<TestDatabaseFactory>
    {
        protected TestDatabaseFactory Factory { get; }

        public BaseIntegrationTest(TestDatabaseFactory factory)
        {
            Factory = factory;
        }
    }
}