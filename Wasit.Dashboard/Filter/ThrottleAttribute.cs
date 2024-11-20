using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using Nancy.Json;

namespace Wasit.Dashboard.Filter
{
    public class ThrottleAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }

        /// <summary>
        /// Gets and sets the Throttling seconds
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Gets and sets the Throttle message. 
        /// Default "You may only perform this action every {n} seconds."
        /// </summary>
        public string Message { get; set; }

        private static MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());

        public override void OnActionExecuting(ActionExecutingContext c)
        {
            if (string.IsNullOrWhiteSpace(Name))
                Name = c.ActionDescriptor.DisplayName;

            var ipAddress = c.HttpContext.Connection.RemoteIpAddress?.ToString();
            var key = string.Concat(Name, "-", ipAddress);

            if (!Cache.TryGetValue(key, out bool entry))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(Seconds));

                Cache.Set(key, true, cacheEntryOptions);
            }
            else
            {
               
                
                //if (string.IsNullOrEmpty(Message))
                //    Message = "You may only perform this action every {n} seconds.";
                //c.Result = new JsonResult(new { success = false, message = Message });
                //c.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString()) };
                //c.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;

            }
        }


    }
}
