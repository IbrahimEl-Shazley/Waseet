using Wasit.Integration.DTOs;

namespace Wasit.Integration.Services.Abstraction
{
    public interface ISMSService
    {
        public Task<bool> Send(SMSDTO dto);
    }
}
