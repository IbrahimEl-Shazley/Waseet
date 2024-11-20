using System.Text.Json.Serialization;
using Wasit.Core.Helpers;

namespace Wasit.Core.Models.DTO
{
    public class PageDTO<T>
    {
        public int CurrentPage { get; set; }

        [JsonIgnore]
        public int PageSize { get; set; } = MyConstants.PageSize;
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasNextPage => CurrentPage < TotalPages;
        public List<T> Data { get; set; }
    }

    public class PageDTO : PageDTO<object>
    {

    }
}
