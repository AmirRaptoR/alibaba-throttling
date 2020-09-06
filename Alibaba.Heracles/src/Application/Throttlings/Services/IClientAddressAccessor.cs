namespace Alibaba.Heracles.Application.Throttlings.Services
{
    // this service is for abstracting user address
    // created without realizing that ip will be sent with request
    // leave it plz. It is harmless
    public interface IClientAddressAccessor
    {
        string ClientAddress { get; }
    }
}