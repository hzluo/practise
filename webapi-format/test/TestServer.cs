using System;
using System.Web.Http.SelfHost;
using webapi_format;

namespace test
{
    public class TestServer : IDisposable
    {
        private HttpSelfHostServer httpSelfHostServer;

        public TestServer()
        {
            HttpSelfHostServer.OpenAsync().Wait();
        }

        private HttpSelfHostServer HttpSelfHostServer
        {
            get
            {
                if (httpSelfHostServer == null)
                {
                    var configuration = new HttpSelfHostConfiguration("http://localhost:9091");
                    RegisterRoutes(configuration);
                    httpSelfHostServer = new HttpSelfHostServer(configuration);
                }
                return httpSelfHostServer;
            }
        }

        public void Dispose()
        {
            HttpSelfHostServer.CloseAsync().Wait();
        }

        private void RegisterRoutes(HttpSelfHostConfiguration configuration)
        {
            WebApiConfig.Register(configuration);
        }

        public JsonClient CreateJsonClient()
        {
            return new JsonClient((HttpSelfHostConfiguration) HttpSelfHostServer.Configuration);
        }
    }
}