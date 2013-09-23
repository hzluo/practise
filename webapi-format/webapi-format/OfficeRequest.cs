using webapi_format.App_Start;

namespace webapi_format
{
    public class OfficeRequest
    {
        public OfficeRequest()
        {
            HomeCountry = new Optional<string>();
        }

        public Optional<string> HomeCountry { get; set; }
    }
}