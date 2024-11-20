using Wasit.Integration.Services.Abstraction;

namespace Wasit.Integration.Services.Implementation
{
    public class EPaymentService : IEPaymentService
    {
        public async Task<bool> Pay()
        {
            return await Task.FromResult(true);
        }
    }
}
