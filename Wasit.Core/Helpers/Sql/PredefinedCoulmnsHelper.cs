namespace Wasit.Core.Helpers.Sql
{
    public static class PredefinedCoulmnsHelper
    {
        public static void Add<T>(T entity, long? userId)
        {
            if (typeof(T).GetProperty("CreatedById") != null)
                typeof(T).GetProperty("CreatedById").SetValue(entity, userId);
            if (typeof(T).GetProperty("CreatedOn") != null)
                typeof(T).GetProperty("CreatedOn").SetValue(entity, DateTime.UtcNow);
        }

        public static void Update<T>(T entity, long? userId)
        {
            if (typeof(T).GetProperty("UpdatedById") != null)
                typeof(T).GetProperty("UpdatedById").SetValue(entity, userId);
            if (typeof(T).GetProperty("UpdatedOn") != null)
                typeof(T).GetProperty("UpdatedOn").SetValue(entity, DateTime.UtcNow);
        }

        public static void SoftRemove<T>(T entity, long? userId)
        {
            if (typeof(T).GetProperty("IsDeleted") != null)
                typeof(T).GetProperty("IsDeleted").SetValue(entity, true);
            if (typeof(T).GetProperty("DeletedById") != null)
                typeof(T).GetProperty("DeletedById").SetValue(entity, userId);
            if (typeof(T).GetProperty("DeletedOn") != null)
                typeof(T).GetProperty("DeletedOn").SetValue(entity, DateTime.UtcNow);
        }
    }
}
