using System.Net;

namespace Organizations.API.Services
{
    public class IpFilterService : IIpFilterService
    {

        private readonly List<string> _allowedIpAddresses;

        public IpFilterService(IConfiguration configuration)
        {
            _allowedIpAddresses = configuration.GetSection("IpFilter:AllowedIpAddresses").Get<List<string>>() ?? new List<string>();
        }

        public bool IsIpAddressAllowed(string ipAddress)
        {
            return _allowedIpAddresses.Any(allowedIp => IPAddress.TryParse(allowedIp, out var parsedIp) && parsedIp.Equals(IPAddress.Parse(ipAddress)));
        }
    }
}
