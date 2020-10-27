namespace ReadModel.Domain.Index.HttpRequest
{
    public class HttpRequestRequest
    {
        public string Method { get; set; }
        public string Uri { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
        public string Os { get; set; }
        public string Browser { get; set; }
        public string Device { get; set; }
    }
}