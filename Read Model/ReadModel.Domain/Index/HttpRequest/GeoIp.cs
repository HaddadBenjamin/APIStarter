using Nest;

namespace ReadModel.Domain.Index.HttpRequest
{
    public class GeoIp
    {
        public string IPv4 { get; set; }
        public GeoLocation Location { get; set; }
    }
}