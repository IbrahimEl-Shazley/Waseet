using AAITHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Helpers.Sql;

namespace Wasit.Core.Entities.Shared
{
    public abstract class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        public virtual string? CreatedById { get; set; }
        public virtual DateTime? CreatedOn { get; set; } = HelperDate.GetCurrentDate();

        public virtual string? UpdatedById { get; set; }
        public virtual DateTime? UpdatedOn { get; set; }

        public virtual bool IsDeleted { get; set; }
        public virtual string? DeletedById { get; set; }

        public virtual DateTime? DeletedOn { get; set; }


        public virtual T AddPredefinedColumns<T>(T entity, long? userId)
        {
            PredefinedCoulmnsHelper.Add(entity, userId);
            return entity;
        }

        public virtual T UpdatePredefinedColumns<T>(T entity, long? userId)
        {
            PredefinedCoulmnsHelper.Update(entity, userId);
            return entity;
        }
    }
}
