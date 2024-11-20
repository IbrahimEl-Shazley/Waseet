namespace Wasit.Integration.Services.Abstraction
{
    public interface IEPaymentService
    {
        Task<bool> Pay();
    }
}
