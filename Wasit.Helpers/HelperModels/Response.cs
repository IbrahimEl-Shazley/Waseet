using Newtonsoft.Json;

namespace Wasit.Helpers.HelperModels
{
    public class Response<T>
    {
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public T Data { get; set; }

        public Response SuccessResult()
        {
            var result = new Response { Success = true, Data = new object() };
            return result;
        }

        public Response SuccessResult(object data, string successMessage)
        {
            var result = new Response { Success = true, Data = data, SuccessMessage = successMessage };
            return result;
        }

        public Response ErrorResult(string errorMessage, string errorCode)
        {
            var result = new Response { Success = false, Data = new object(), ErrorCode = errorCode, ErrorMessage = errorMessage };
            return result;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Response : Response<object>
    {

    }

}
