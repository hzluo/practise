using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.SelfHost;
using Newtonsoft.Json;

namespace test
{
    public class JsonClient
    {
        private readonly string hostAddress;
        private readonly HttpSelfHostConfiguration config;
        private readonly string mediaType;

        public JsonClient(HttpSelfHostConfiguration config, string mediaType = "application/json")
        {
            hostAddress = config.BaseAddress.ToString();
            this.config = config;
            this.mediaType = mediaType;
        }

        public HttpResponseMessage Get(string resourceName)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(String.Format("{0}/{1}", hostAddress, resourceName)),
                Method = HttpMethod.Get,
            };
            return Get(request);
        }

        public HttpResponseMessage GetByLink(string link)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(link),
                Method = HttpMethod.Get,
            };
            return Get(request);
        }

        public HttpResponseMessage GetById(string id, string resourceName)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(String.Format("{0}/{1}/{2}", hostAddress, resourceName, id)),
                Method = HttpMethod.Get,
            };
            return Get(request);
        }

        public HttpResponseMessage PostAsJsonByLink<T>(string link, T body)
        {
            var request = new HttpRequestMessage
            {
                Content = new ObjectContent<T>(body, config.Formatters.JsonFormatter, mediaType),
                RequestUri = new Uri(link),
                Method = HttpMethod.Post
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            var response = new HttpClient().SendAsync(request).Result;
            return response;
        }

        public HttpResponseMessage PostJsonByLink(string link, string json)
        {
            var request = new HttpRequestMessage
            {
                Content = new StringContent(json),
                RequestUri = new Uri(string.Format("{0}/{1}",hostAddress, link)),
                Method = HttpMethod.Post
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            var response = new HttpClient().SendAsync(request).Result;
            return response;
        }

        public HttpResponseMessage Put(string link, string data)
        {
//            var json = JsonConvert.SerializeObject(data);
            var request = new HttpRequestMessage
            {
                Content = new StringContent(data),
                RequestUri = new Uri(string.Format("{0}/{1}", hostAddress, link)),
                Method = HttpMethod.Put
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            var response = new HttpClient().SendAsync(request).Result;
            return response;
        }

        private HttpResponseMessage Get(HttpRequestMessage request)
        {
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            var response = new HttpClient().SendAsync(request).Result;
            return response;
        }
    }
}