using System.Net;

namespace WebApplication7_petPals.ApiStatusCode
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessages { get; set; }
        public T Result { get; set; }

        public ApiResponse( HttpStatusCode statusCode, bool Issuccess, string errormessages, T result) {
            StatusCode = statusCode;
            IsSuccess = Issuccess;
            ErrorMessages = errormessages;
            Result = result;
        }

    }
}
