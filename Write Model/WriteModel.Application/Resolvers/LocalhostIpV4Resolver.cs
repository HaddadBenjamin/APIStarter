using System.IO;
using System.Net;
using WriteModel.Domain.Resolvers;

namespace APIStarter.Application.Resolvers
{
    public class LocalhostIPv4Resolver : ILocalhostIPv4Resolver
    {
        public string Resolve()
        {
            var webRequest = WebRequest.Create("http://checkip.dyndns.org/");
            using var webResponse = webRequest.GetResponse();
            using var streamReader = new StreamReader(webResponse.GetResponseStream());
            var IPv4Response = streamReader.ReadToEnd();
            var IPv4StartIndex = IPv4Response.IndexOf("Address: ") + 9;
            var IPv4EndIndex = IPv4Response.LastIndexOf("</body>");
            var IPv4 = IPv4Response.Substring(IPv4StartIndex, IPv4EndIndex - IPv4StartIndex);

            return IPv4;
        }
    }
}