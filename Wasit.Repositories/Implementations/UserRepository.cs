using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Wasit.Context;
using Wasit.Repositories.Interfaces;

namespace Wasit.Repositories.Implementations
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> MailExistsEdit(string email, string userId)
         => await _context.Users.AnyAsync(u => (u.Email == email) && u.Id != userId);

        public async Task<bool> MailExistsRegister(string email)
          => await _context.Users.AnyAsync(u => u.Email == email);

        public async Task<bool> PhoneExistsBeforEdit(string phone, string userId)
         => await _context.Users.AnyAsync(u => u.PhoneNumber == phone && u.Id != userId);

        public async Task<bool> PhoneExistsBeforeRegister(string phone)
            => await _context.Users.AnyAsync(u => u.PhoneNumber == phone);
        
        public async Task<bool> IdentityNumberExists(string IdentityNumber)
            => await _context.Users.AnyAsync(u => u.IDNumber == IdentityNumber);

        public async Task<bool> IdentityNumberExists(string IdentityNumber, string userId)
            => await _context.Users.AnyAsync(u => u.IDNumber == IdentityNumber && u.Id != userId);

        public async Task<bool> CommercialNumberExists(string commercialNumber)
            => await _context.Users.AnyAsync(u => u.CommercialNo == commercialNumber);

        public async Task<bool> CommercialNumberExists(string commercialNumber, string userId)
            => await _context.Users.AnyAsync(u => u.CommercialNo == commercialNumber && u.Id != userId);

        public IQueryable<T> GetUser<T>(Expression<Func<T, bool>> predicate, bool withTracking = true) where T : class
        {
            var query = _context.Set<T>().AsQueryable();
            if (withTracking)
                return query.Where(PreparePredicate(predicate));
            else
                return query.Where(PreparePredicate(predicate)).AsNoTracking();
        }   
        
        public Task<T> UserFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : class
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    var splitted = include.ToString().Replace("(", "").Replace(")", "").Split("Select");
                    if (splitted.Length > 1)
                    {
                        string pathInclude = string.Empty;
                        var i = 0;
                        foreach (var prop in splitted)
                        {
                            var subSplitted = prop.Split('.');
                            for (int j = 1; j < subSplitted.Length; j++)
                            {
                                pathInclude += $"{subSplitted[j]}";
                                if (j < subSplitted.Length - 1)
                                    pathInclude += ".";
                            }
                            if (i < splitted.Length - 1)
                                pathInclude += ".";
                            i++;
                        }
                        pathInclude = pathInclude.Replace("..", ".");
                        query = query.Include(pathInclude);
                    }
                    else
                    {
                        query = query.Include(include);
                    }
                }
            }

            if (withTracking)
                return query.FirstOrDefaultAsync(PreparePredicate(predicate));
            else
                return query.FirstOrDefaultAsync(PreparePredicate(predicate));
        }

        public void UpdateUser<T>(T entity) where T : class
        {
            _context.Update<T>(entity);
        }

        public async Task AddEntityAsync<T>(T entity) where T : class
        {
            await _context.AddAsync<T>(entity);
        }

    }
}
