namespace TestWebApplication.Abstraction.ResponseDto;

public abstract class BaseResponseDto<T>
{
    public BaseResponseDto()
    {

    }

    public BaseResponseDto(T value)
    {
        Data = value;
        Succeeded = true;
        Error = null;
    }

    public BaseResponseDto(string errorCode, string errorMessage, List<string> details = null)
    {
        Succeeded = false;
        Error = new Error
        {
            Code = errorCode,
            Message = errorMessage,
            Details = details
        };
        Data = default;
    }

    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public Error? Error { get; set; }
}

public class Error
{
    public string Code { get; set; }
    public string Message { get; set; }
    public List<string> Details { get; set; }
}