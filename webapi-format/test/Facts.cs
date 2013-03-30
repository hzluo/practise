using Xunit;

namespace test
{
    public class Facts
    {
        private readonly JsonClient jsonClient;

        public Facts() 
        {
            jsonClient = new TestServer().CreateJsonClient();
        }

        [Fact]
        public void ShouldTest()
        {
           var rep = jsonClient.GetById("1", "api/offices").Content.ReadAsStringAsync().Result;

            Assert.Equal("value", rep);
        }
    }
}
