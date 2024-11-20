namespace Wasit.Core.Models.DTO
{
    public class DTO<T>
    {
        public virtual T Id { get; set; }
    }

    public class DTO : DTO<string>
    {

    }
}
