using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Wasit.Core.ExtensionsMethods
{
    public static class ContextExtensions
    {
        public static IQueryable<TEntity> FindQuery<TContext, TEntity>(this DbSet<TEntity> set, params object[] keyValues) where TContext : DbContext where TEntity : class
        {
            var context = ((IInfrastructure<IServiceProvider>)set).GetService<TContext>();

            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();

            var entries = context.ChangeTracker.Entries<TEntity>();
            //Edit to find with the full entity object
            var i = 0;
            if (Convert.GetTypeCode(keyValues[0]) == TypeCode.Object)//is object
            {
                var newKeyValues = new object[key.Properties.Count];
                var entity = keyValues[0];
                i = 0;
                foreach (var property in key.Properties)
                {
                    newKeyValues[i] = entity.GetType().GetProperty(property.Name).GetValue(entity);
                    i++;
                }
                keyValues = newKeyValues;
            }

            i = 0;
            foreach (var property in key.Properties)
            {
                var keyValue = keyValues[i];
                entries = entries.Where(e => e.Property(property.Name).CurrentValue == keyValue);
                i++;
            }

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = set.AsQueryable();
            i = 0;
            foreach (var property in key.Properties)
            {
                var propertyName = key.Properties[i].Name;
                Type clrType = key.Properties[i].ClrType;
                var keyValue = TypeDescriptor.GetConverter(key.Properties[i].ClrType).ConvertFromInvariantString(Convert.ToString(keyValues[i]));

                query = query.Where((Expression<Func<TEntity, bool>>)Expression.Lambda(
                            Expression.Equal(Expression.Property(parameter, propertyName), Expression.Constant(keyValue)),
                            parameter)
                        );
                i++;
            }
            return query;
        }
    }
}
