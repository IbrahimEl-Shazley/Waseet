using System.Linq.Expressions;

namespace Wasit.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<bool> MailExistsRegister(string email);
        Task<bool> PhoneExistsBeforeRegister(string phone);
        Task<bool> MailExistsEdit(string email, string userId);
        Task<bool> PhoneExistsBeforEdit(string phone, string userId);
        Task<bool> IdentityNumberExists(string IdentityNumber);
        Task<bool> IdentityNumberExists(string IdentityNumber, string userId);
        Task<bool> CommercialNumberExists(string CommercialNumber);
        Task<bool> CommercialNumberExists(string CommercialNumber, string userId);
        IQueryable<T> GetUser<T>(Expression<Func<T, bool>> predicate, bool withTracking = true) where T : class;
        Task<T> UserFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : class; //params Expression<Func<T, object>>[] includes
        void UpdateUser<T>(T entity) where T : class;
        Task AddEntityAsync<T>(T entity) where T : class;



    }
}
