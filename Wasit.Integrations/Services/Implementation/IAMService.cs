using Wasit.Integration.DTOs;
using Wasit.Integration.Services.Abstraction;

namespace Wasit.Integration.Services.Implementation
{
    public class IAMService : IIAMService
    {
        public async Task<IAMUserDTO> GetUserInfoAsync(string identityId)
        {
            return await Task.FromResult(new IAMUserDTO());
        }
    }
}
