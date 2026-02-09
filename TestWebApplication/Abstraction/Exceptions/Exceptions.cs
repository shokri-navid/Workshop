namespace TestWebApplication.Abstraction.Exceptions
{

    public abstract class BaseException : Exception
    {
        public string Code { get; protected set; }
        public List<string> Details { get; protected set; }

        public BaseException(string code, string message, List<string>? details = null) : base(message)
        {
            Code = code;
            Details = details ?? new List<string>();
        }
    }


    public abstract class DomainException : BaseException
    {
        public DomainException(string message) : base("DomainError_", message)
        {
        }
    }


    public class BusinessException : BaseException
    {
        public BusinessException(string message, List<string>? details = null) : base("BusinessError_", message,
            details)
        {
        }
    }

    public class ItemNotFoundException : BusinessException
    {
        public ItemNotFoundException(string message, string entityName)
            : base(string.Format(message, entityName))
        {
            Code += "404";
        }
    }

    public class PermissionDeniedException : BusinessException
    {
        public PermissionDeniedException(string message)
            : base(message)
        {
            Code += "403";
        }
    }

    public class InvalidParameterException : BusinessException
    {
        public InvalidParameterException(string message)
            : base(message)
        {
            Code += "400";
        }
    }

    public class InvalidOperationException : BusinessException
    {
        public InvalidOperationException(string message, params string[] details)
            : base(message, details.ToList())
        {
            Code += "400";
        }
    }

    public class ServiceAuthenticationException : BusinessException
    {
        public ServiceAuthenticationException(string message)
            : base(message)
        {
            Code += "401";
        }
    }

    public class ServerErrorException : BusinessException
    {
        public ServerErrorException(string message, params string[] details)
            : base(message, details.ToList())
        {
            Code += "500";
        }
    }

    public class CustomBusinessException : BusinessException
    {
        public CustomBusinessException( List<string>? details = null) : base("this is a custom exception", details)
        {
        }
    }
}
