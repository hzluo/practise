using System.Collections.Generic;
using System.Web.Http;

namespace webapi_format.Controllers
{
    public class OfficesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]OfficeRequest request)
        {
            Fake.Value = (request);
            var office = new Office();
            request.HomeCountry.Write(v => office.HomeCountry = v);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class Office
    {
        public string HomeCountry { get; set; }
    }

    public class Fake
    {
        public static OfficeRequest Value { get; set; }
    }
}