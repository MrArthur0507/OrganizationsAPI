namespace Organizations.API.Services
{
    public interface IIpFilterService
    {
        public bool IsIpAddressAllowed(string ipAddress);
    }
}
