using System.IO;
using System.Net.Http.Formatting;
using Xunit;
using webapi_format.Controllers;

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

        [Fact]
        public void TestSerialization()
        {
            var str = "{\"HomeCountry\": \"1\"}";
            var rep = jsonClient.Put("api/offices/1", str);
            var realRequestion = Fake.Value;
            Assert.Equal("1", realRequestion.HomeCountry.Value);

            str = "{}";
            jsonClient.Put("api/offices/1", str);
            realRequestion = Fake.Value;
            Assert.Equal(false, realRequestion.HomeCountry.HasNewValue);
        }

        T Deserialize<T>(MediaTypeFormatter formatter, string str) where T : class
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return formatter.ReadFromStreamAsync(typeof(T), stream, null, null).Result as T;
        }
    }
}