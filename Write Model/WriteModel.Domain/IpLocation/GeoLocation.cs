using Newtonsoft.Json;

namespace WriteModel.Domain.IpLocation
{
    public class GeoLocation
    {
        public string Ip { get; set; }
        [JsonProperty("country_code")] public string CountryCode { get; set; }
        [JsonProperty("country_name")] public string CountryName { get; set; }
        [JsonProperty("region_code")] public string RegionCode { get; set; }
        [JsonProperty("region_name")] public string RegionName { get; set; }
        public string City { get; set; }
        [JsonProperty("zip_code")] public string ZipCode { get; set; }
        [JsonProperty("time_zone")] public string TimeZone { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [JsonProperty("metro_code")] public int MetroCode { get; set; }
    }
}
