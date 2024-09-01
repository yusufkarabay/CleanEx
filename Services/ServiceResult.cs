using CleanEx.Services.Dtos;
using System.Net;
using System.Text.Json.Serialization;

namespace CleanEx.Services
{
    public record NoContentDto;
    public class ServiceResult<T> where T : class
    {
        public T Data { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public ErrorDto Error { get; private set; }
        public string Message { get; private set; }
        public int Count { get; private set; }

        [JsonIgnore]
        public bool IsSuccessful { get; private set; }

        public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static ServiceResult<T> Success(T data, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T> { Data = data, StatusCode = statusCode, IsSuccessful = true, Message = message };
        }

        public static ServiceResult<T> Success(T data, string message, int count, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T> { Data = data, StatusCode = statusCode, IsSuccessful = true, Message = message, Count = count };
        }

        public static ServiceResult<T> Success(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T> { Data = null, StatusCode = statusCode, IsSuccessful = true };
        }

        public static ServiceResult<T> Fail(ErrorDto errorDto, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }

        public static ServiceResult<T> Fail(string errorMessage, bool isShow, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var errorDto = new ErrorDto(errorMessage, isShow);
            return new ServiceResult<T> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }

        public static ServiceResult<T> FailMessage(string errorMessage, bool isShow, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var errorDto = new ErrorDto(errorMessage, isShow);
            return new ServiceResult<T> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false, Message = errorMessage };
        }
    }

}




