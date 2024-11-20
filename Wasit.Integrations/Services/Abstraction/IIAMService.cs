using Wasit.Integration.DTOs;

namespace Wasit.Integration.Services.Abstraction
{
    public interface IIAMService
    {
        public Task<IAMUserDTO> GetUserInfoAsync(string identityId);
    }
}
