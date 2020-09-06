using Alibaba.Heracles.Application.Throttlings.Services;
using Microsoft.AspNetCore.Http;

namespace Alibaba.Heracles.WebUI.Services
{
    public class HttpClientAddressAccessor : IClientAddressAccessor
    {
        public HttpClientAddressAccessor(IHttpContextAccessor httpContextAccessor)
        {
            // this is not working behind proxy server
            // should check x-forward header before using this shit
            ClientAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public string ClientAddress { get; }
    }
}