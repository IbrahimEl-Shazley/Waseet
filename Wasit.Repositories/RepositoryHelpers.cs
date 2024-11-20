namespace Wasit.Repositories
{
    public static class RepositoryHelpers
    {
        public static void AddPreDefibedColumns<T>(T entity)
        {
            if (typeof(T).GetProperty("CreatedBy") != null)
                typeof(T).GetProperty("CreatedBy").SetValue(entity, 0);
            if (typeof(T).GetProperty("CreatedOn") != null)
                typeof(T).GetProperty("CreatedOn").SetValue(entity, DateTime.UtcNow);
        }

        public static void DeleteFlag<T>(T entity)
        {
            if (typeof(T).GetProperty("IsDeleted") != null)
                typeof(T).GetProperty("IsDeleted").SetValue(entity, true);
            if (typeof(T).GetProperty("DeletedBy") != null)
                typeof(T).GetProperty("DeletedBy").SetValue(entity, 0);
            if (typeof(T).GetProperty("DeletedOn") != null)
                typeof(T).GetProperty("DeletedOn").SetValue(entity, DateTime.UtcNow);
        }
    }
}
